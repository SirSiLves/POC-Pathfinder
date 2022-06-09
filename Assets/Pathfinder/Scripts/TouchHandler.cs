using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TouchHandler : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float minMoveDistance = -1000f, maxMoveDistance = 1000f, moveSpeed = 10f, rotateSpeed = 0.5f;
    [SerializeField] private GameObject canvas;

    [SerializeField] private EventSystem eventSystem;


    private TouchController touchController;
    private GraphicRaycaster raycaster;
    private PointerEventData m_PointerEventData;
    private Vector3 touchStart, rotateStart;
    private float rotX = 0f, rotY = 0f;
    private float groundY = 100f;
    private bool isMoving, isLooking;


    public void Awake()
    {
        touchController = new TouchController();
        //Fetch the Raycaster from the GameObject (the Canvas)
        raycaster = canvas.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        eventSystem = GetComponent<EventSystem>();
    }

    public void OnEnable()
    {
        touchController.Enable();
    }

    public void OnDisable()
    {
        touchController.Disable();
    }

    public void Start()
    {
        // subscribe
        touchController.Touch.PrimaryTouchContact.performed += _ => MoveAroundStart();
        touchController.Touch.PrimaryTouchContact.canceled += _ => MoveAroundEnd();

        // subscribe
        touchController.Touch.SecondayTouchContact.performed += _ => LookAroundStart();
        touchController.Touch.SecondayTouchContact.canceled += _ => LookAroundEnd();
    }

    public void Update()
    {
        // TODO is click outside, don't do anything
        if (isOutside()) { return; }

        if (isLooking) { LookAround(); return; } // don't move if true
        if (isMoving) { MoveAround(); }
    }

    private bool isOutside()
    {
        Vector2 touchPosition = touchController.Touch.PrimaryFingerPosition.ReadValue<Vector2>();
        Debug.Log(cam.ScreenToWorldPoint(touchPosition));


        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(eventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = touchPosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        raycaster.Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit " + result.gameObject.name);
        }



        return true;
    }

    private void MoveAroundStart()
    {
        touchStart = GetWorldPosition(groundY);
        isMoving = true;
    }

    private void MoveAroundEnd()
    {
        isMoving = false;
    }

    private void MoveAround()
    {
        Vector3 direction = touchStart - GetWorldPosition(groundY);

        Vector3 targetPosition = new Vector3(
            cam.transform.position.x + direction.x,
            cam.transform.position.y + direction.y,
            cam.transform.position.z + direction.z
        );


        Vector3 maxTargetPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minMoveDistance, maxMoveDistance),
            targetPosition.y,
            Mathf.Clamp(targetPosition.z, minMoveDistance, maxMoveDistance)
        );

        cam.transform.position = Vector3.Lerp(Camera.main.transform.position, maxTargetPosition, Time.deltaTime * moveSpeed);
    }

    private Vector3 GetWorldPosition(float y)
    {
        Vector2 touchPosition = touchController.Touch.PrimaryFingerPosition.ReadValue<Vector2>();
        // camera has to be on orthographic
        Ray mousePos = cam.ScreenPointToRay(
            new Vector3(
                touchPosition.x,
                touchPosition.y,
                touchStart.z
            )
        );

        Plane ground = new Plane(Vector3.down, new Vector3(0, y, 0));
        float distance;
        ground.Raycast(mousePos, out distance);

        return mousePos.GetPoint(distance);
    }

    private void LookAroundStart()
    {
        isMoving = false;

        rotX = cam.transform.eulerAngles.x;
        rotY = cam.transform.eulerAngles.y;
        rotateStart = touchController.Touch.SecondaryFingerPosition.ReadValue<Vector2>();

        isLooking = true;
    }

    private void LookAroundEnd()
    {
        isLooking = false;
    }

    private void LookAround()
    {
        Vector3 rotateEnd = touchController.Touch.SecondaryFingerPosition.ReadValue<Vector2>();

        float deltaX = rotateStart.x - rotateEnd.x;
        float deltaY = rotateStart.y - rotateEnd.y;

        rotX -= deltaY * Time.deltaTime * rotateSpeed * -1;
        rotY += deltaX * Time.deltaTime * rotateSpeed * -1;
        rotX = Mathf.Clamp(rotX, 0f, 90f); // max angler

        cam.transform.eulerAngles = new Vector3(rotX, rotY, 0f);
    }

}