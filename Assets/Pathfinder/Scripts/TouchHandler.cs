using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchHandler : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float minMoveDistance = -100f, maxMoveDistance = 100f, moveSpeed = 10f, rotateSpeed = 0.5f;
    [SerializeField] private GameObject canvas;

    private Vector3 touchStart, rotateStart;
    private float rotX = 0f, rotY = 0f, groundY = 100f;
    private bool isMoving, isLooking;
    private GraphicRaycaster raycaster;


    public void Start()
    {
        raycaster = canvas.GetComponent<GraphicRaycaster>();
    }

    public void Update()
    {
        if (isTouchOnCanvas()) { return; }

        // use legacy input system, cause of unity remote
        switch (Input.touchCount)
        {
            case 1:
                MoveAround();
                break;
            case 2:
                LookAround();
                break;
            default:
                Reset(); 
                break;
        }
    }

    private bool isTouchOnCanvas()
    {
        //Set up the new Pointer Event
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        pointerData.position = Input.mousePosition;
        raycaster.Raycast(pointerData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit " + result.gameObject.name);
            return true;
        }

        return false;
    }

    private void Reset()
    {
        if (isMoving) { MoveAroundEnd(); }
        if (isLooking) { LookAroundEnd(); }
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
        if (!isMoving)
        {
            MoveAroundStart();
        }
        else
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

            cam.transform.position = Vector3.Lerp(cam.transform.position, maxTargetPosition, Time.deltaTime * moveSpeed);
        }
    }

    private Vector3 GetWorldPosition(float y)
    {
        Vector2 touchPosition = Input.GetTouch(0).position;
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
        rotateStart = Input.GetTouch(1).position;

        isLooking = true;
    }

    private void LookAroundEnd()
    {
        isLooking = false;
    }

    private void LookAround()
    {
        if (!isLooking)
        {
            LookAroundStart();
        }
        else
        {
            Vector3 rotateEnd = Input.GetTouch(1).position;

            float deltaX = rotateStart.x - rotateEnd.x;
            float deltaY = rotateStart.y - rotateEnd.y;

            rotX -= deltaY * Time.deltaTime * rotateSpeed * -1;
            rotY += deltaX * Time.deltaTime * rotateSpeed * -1;
            rotX = Mathf.Clamp(rotX, 0f, 90f); // max angler

            cam.transform.eulerAngles = new Vector3(rotX, rotY, 0f);
        }
    }

}