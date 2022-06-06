using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchHandler : MonoBehaviour
{

    private TouchController touchController;
    private Vector2 moveStart, moveEnd, rotateStart, rotateEnd;
    private bool isLooking;
    private float rotateSpeed = 0.5f;
    private float rotX = 0f, rotY = 0f;


    public void Awake()
    {
        touchController = new TouchController();
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
        rotX = Camera.main.transform.eulerAngles.x;
        rotY = Camera.main.transform.eulerAngles.y;

        // subscribe
        touchController.Touch.PrimaryTouchContact.performed += _ => MoveAroundStart();
        touchController.Touch.PrimaryTouchContact.canceled += _ => MoveAroundEnd();

        // subscribe
        touchController.Touch.PrimaryTouchHold.performed += _ => LookAroundStart();
        touchController.Touch.PrimaryTouchHold.canceled += _ => LookAroundEnd();
    }

    public void Update()
    {
        if (isLooking) { LookAround(); }
    }

    private void MoveAroundStart()
    {
        moveStart = touchController.Touch.PrimaryFingerPosition.ReadValue<Vector2>();

        // Coordinates in World
        // Camera.main.ScreenToViewportPoint(touchController.Touch.PrimaryFingerPosition.ReadValue<Vector2>())
    }

    private void MoveAroundEnd()
    {
        if (isLooking) { return; }

        moveEnd = touchController.Touch.PrimaryFingerPosition.ReadValue<Vector2>();

        Vector2 distance = (moveStart - moveEnd) / 10f;

        Camera.main.transform.position = new Vector3(
            Camera.main.transform.position.x + distance.x,
            Camera.main.transform.position.y,
            Camera.main.transform.position.z + distance.y
        );
    }

    private void LookAroundStart()
    {
        rotateStart = touchController.Touch.PrimaryFingerPosition.ReadValue<Vector2>();
        isLooking = true;
    }

    private void LookAroundEnd()
    {
        isLooking = false;
    }

    private void LookAround()
    {
        rotateEnd = touchController.Touch.PrimaryFingerPosition.ReadValue<Vector2>();

        float deltaX = rotateStart.x - rotateEnd.x;
        float deltaY = rotateStart.y- rotateEnd.y;

        rotX -= deltaY * Time.deltaTime * rotateSpeed * -1;
        rotY += deltaX * Time.deltaTime * rotateSpeed * -1;
        // max angler
        rotX = Mathf.Clamp(rotX, 0f, 90f);
        
        Camera.main.transform.eulerAngles = new Vector3(rotX, rotY, 0f);
    }

}
