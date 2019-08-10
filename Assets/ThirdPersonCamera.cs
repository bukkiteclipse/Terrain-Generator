using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

    private const float Y_ANGLE_MIN = -89f;
    private const float Y_ANGLE_MAX = 89f;

    public Vector3 lookAtVector;


    private float distance = 50f;
    private float currentXAngle = 0f;
    private float currentYAngle = 12f;
    private float rotationSpeedX = 4f;
    private float rotationSpeedY = 4f;

    bool automaticRotationMode = true;
    bool userInputRotationMode = false;
    bool fastUserInputZoomMode = false;

    private float maxDistance = 120f;
    private float minDistance = 3f;
    private float userInputZoomSpeed = 1f;
    private float fastUserInputSpeed = 10f;

    private void Start()
    {
        lookAtVector = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        CheckForUserMousButtonInput();
        if (!userInputRotationMode)
        {
            RotateAutomatic();
        }
        else
        {
            HandleUserRotation();
        }
        HandleInputZoom();
    }

    private void LateUpdate()
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentYAngle, currentXAngle, 0);
        transform.position = lookAtVector + rotation * direction;
        transform.LookAt(lookAtVector);
    }

    private void RotateAutomatic()
    {
        if (automaticRotationMode)
        {
            currentXAngle += Time.deltaTime * -rotationSpeedX;
        }
    }

    private void HandleUserRotation()
    {
        currentXAngle += Input.GetAxis("Mouse X") * rotationSpeedX;
        currentYAngle -= Input.GetAxis("Mouse Y") * rotationSpeedY;

        currentYAngle = Mathf.Clamp(currentYAngle, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    void HandleInputZoom()
    {
        if (fastUserInputZoomMode)
        {
            distance += Input.GetAxis("Mouse Y") * -fastUserInputSpeed;
        }
        else
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                    distance -= userInputZoomSpeed;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                    distance += userInputZoomSpeed;
            }
        }

        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    private void CheckForUserMousButtonInput()
    {
        if (Input.GetMouseButton(1))
        {
            userInputRotationMode = true;
        }
        else
        {
            userInputRotationMode = false;
        }

        if (Input.GetMouseButton(2))
        {
            fastUserInputZoomMode = true;
        }
        else
        {
            fastUserInputZoomMode = false;
        }
    }

    public void ChangeAutomaticRotationMode(bool state)
    {
        automaticRotationMode = state;
    }
}
