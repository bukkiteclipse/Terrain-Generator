using UnityEngine;
using System.Collections;

public class CameraRotator : MonoBehaviour {

    public float cameraDistanceFromPivotPoint = 36;
    public float rotationSpeed;
    Camera mainCamera;
    public MeshGenerator meshGenerator;

    Vector3 pivotPoint;
    Vector3 crossProductVectorForUpRotation;
    Vector3 upUnitVector = new Vector3(0, 1, 0);

    bool automaticRotation = true;
    bool inputRotationNow = false;
    public float inputRotationSpeed = 10f;
    public float inputZoomSpeed = 4f;
    public float minimumZoomDistance = 3f;
    public float maximumZoomDistance = 120f;

    private void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
        //meshGenerator = GetComponentInParent<MeshGenerator>();
        //meshGenerator = GetComponent<MeshGenerator>();

        //pivotPoint = transform.position;

        setUpPivotPoint();
    }
    void setUpPivotPoint()
    {
        pivotPoint = new Vector3(0, 0, 0);
        transform.position = pivotPoint;
        //cameraDistanceFromPivotPoint = Mathf.Sqrt(meshGenerator.xSize * meshGenerator.xSize + meshGenerator.zSize * meshGenerator.zSize);
        cameraDistanceFromPivotPoint = 70;
        //cameraDistanceFromPivotPoint *= 0.55f;

        mainCamera.transform.LookAt(pivotPoint);

        Vector3 newCameraPosition = CalculateCameraPositionDueToDistance();
        mainCamera.transform.position = newCameraPosition;
    }

    // Update is called once per frame
    void Update () {
        //transform.position = pivotPoint;
        mainCamera.transform.position = CalculateCameraPositionDueToDistance();
        CheckForRotationInput();
        if (!inputRotationNow)
        {
            HandleAutomaticRotation();
        } else
        {
            HandleInputRotation();
        }
        HandleInputZoom();
	}


    Vector3 CalculateCameraPositionDueToDistance()
    {
        mainCamera.transform.LookAt(pivotPoint);
        Vector3 directionVector = mainCamera.transform.position - pivotPoint;
        directionVector.Normalize();
        Vector3 projectedDirectionalVector = new Vector3(directionVector.x, 0, directionVector.z);
        projectedDirectionalVector.Normalize();
        crossProductVectorForUpRotation = Vector3.Cross(upUnitVector, projectedDirectionalVector);
        crossProductVectorForUpRotation.Normalize();

        Vector3 newPositionVector = pivotPoint + cameraDistanceFromPivotPoint * directionVector;
        return newPositionVector;
    }

    void HandleInputZoom()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            /*
            if (camera.fieldOfView > 7)
            {
                camera.fieldOfView--;
            } else
            {
                camera.fieldOfView = 7;
            } */
            if(cameraDistanceFromPivotPoint > minimumZoomDistance) {
                cameraDistanceFromPivotPoint -= inputZoomSpeed;
            } else
            {
                cameraDistanceFromPivotPoint = minimumZoomDistance;
            }
            
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            //camera.fieldOfView++;
            if (cameraDistanceFromPivotPoint < maximumZoomDistance)
            {
                cameraDistanceFromPivotPoint += inputZoomSpeed;
            }
            else
            {
                cameraDistanceFromPivotPoint = maximumZoomDistance;
            }
        }
    }

    void HandleAutomaticRotation()
    {
        if (automaticRotation)
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
    }
    void HandleInputRotation()
    {
        float deltaX = inputRotationSpeed * Input.GetAxis("Mouse X");
        //float deltaY = inputRotationSpeed * Input.GetAxis("Mouse Y") * 0.4f;
        float deltaY = inputRotationSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(0, deltaX, 0);
        transform.Rotate(crossProductVectorForUpRotation.x * deltaY, crossProductVectorForUpRotation.y * deltaY, crossProductVectorForUpRotation.z * deltaY);
        //mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + deltaY, mainCamera.transform.position.z);
    }

    void CheckForRotationInput()
    {
        if (Input.GetMouseButton(1))
        {
            inputRotationNow = true;
        }
        else
        {
            inputRotationNow = false;
        }
    }

    public void ChangeAutomaticRotationState(bool state)
    {
        automaticRotation = state;
    }
}
