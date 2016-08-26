using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CameraController : MonoBehaviour
{

    //Used by all camera fucntions
    public GameObject target;   // The object the camera is tracking
    private Vector3 cameraTargetLine;     // Describes the line between the camera and the target

    // Rotation Variables
    public float rotateSpeed;

    // Zooming Variables
    private float distanceToZoom;
    private Vector3 slopeOfZoomLine;    // Same as the cameraTargetLine, but normalized and includes zoom direction
    public float zoomSpeed;             // Speed at which a request to zoom will be fulfiled
    public float zoomSensitivity;       // Determines the ammount of distance each scroll request produces
    public float maxZoomDistance;
    public float minZoomDistance;

    // Use this for initialization
    void Start()
    {
        rotateSpeed = 5.0f;             // A constant
        zoomSpeed = 2.5f;               // A constant
        maxZoomDistance = 30f;          // A constant
        minZoomDistance = 5f;           // A constant
        distanceToZoom = 0;             // A dynamic variable that keeps track of distances that need to be zoomed
        zoomSensitivity = 1;            // A constant. A value of 0 produces no movement during scroll requests
        slopeOfZoomLine = new Vector3(0, 0, 0); // A dynamic variable

        transform.LookAt(target.transform);
        cameraTargetLine = transform.position - target.transform.position;
    }

    // Update is called once per frame
    // guarneteed to run after all items have been processed in Update
    void LateUpdate()
    {
        // Enable the camera to move with the target
        positionHandler();

        // Enable camera zoom on our target
        zoomHandler();

        // Enable camera rotation on the X axis
        xRotationHandler();

        // Enable camera rotation on the Y axis
        yRotationHandler();
    }

    void positionHandler()
    {
        // NOTE: The object this camera is tracking should be moving smoothly, else the camera will seem discrete
        // Move the camera relative to the target
        transform.position = target.transform.position + cameraTargetLine;
    }

    void yRotationHandler()
    {
        // TODO: Implement smooth rotation
        // TODO: Limit the camera angle
        // TODO: Add restrictions to rotations and world objects. TBD.

        // If the right mouse button is pressed
        if (Input.GetMouseButton(1))
        {
            // Allow for rotation on the Y axis (up and down)
            transform.RotateAround(target.transform.position, transform.right * -1, Input.GetAxis("Mouse Y") * rotateSpeed);

            // Caclulate the new camera direction/position relative to our target
            cameraTargetLine = transform.position - target.transform.position;
        }
    }

    void xRotationHandler()
    {
        // TODO: Implement smooth rotation
        // TODO: Add restrictions to rotations and world objects. TBD.

        // If the right mouse button is pressed
        if (Input.GetMouseButton(1))
        {
            // Allow for rotation on the X axis (left and right)
            transform.RotateAround(target.transform.position, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed);

            // Caclulate the new camera direction/position relative to our target
            cameraTargetLine = transform.position - target.transform.position;
        }
    }

    void zoomHandler()
    {
        // TODO: Add zoom sensitivity metric, this will determine how far a single scroll goes

        // Get any potential data caused by the scroll wheel
        float scrollEventData = Input.GetAxis("Mouse ScrollWheel");

        // If the scrollwheel is being triggered, initialize zoom data
        if (scrollEventData != 0)
        {
            // Calculate the vector between the target and camera, scrollEventData dictates direction
            slopeOfZoomLine = cameraTargetLine * Mathf.Sign(scrollEventData) * -1;

            // Normalize the direction vector to be used in movement calcuations
            slopeOfZoomLine.Normalize();

            // Calcuate the distance the camera should move
            distanceToZoom += Vector3.Distance(transform.position + slopeOfZoomLine, transform.position);
        }

        // Note: Negative values of scrollEventData indicate requests to zoom out
        // If the distance between the camera and tracking target exceeds our maximum,
        // And if we are detecting requests to zoom out more
        if (cameraTargetLine.magnitude > maxZoomDistance && scrollEventData <= 0)
        {
            // Deny the request to zoom
            return;
        }

        // Note: Positive values of scrollEventData indicate requests to zoom in
        // If the distance between the camera and tracking target exceeds our minimum,
        // And if we are detecting requests to zoom in more
        if (cameraTargetLine.magnitude < minZoomDistance && scrollEventData >= 0)
        {
            // Deny the request to zoom
            return;
        }

        // Calculate the point in space where the camera should go
        Vector3 desiredPosition = transform.position + (slopeOfZoomLine * distanceToZoom);

        // Move the camera to that position over time
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * zoomSpeed);

        // Caclulate the new camera direction/position relative to our target
        cameraTargetLine = transform.position - target.transform.position;

        // Calculate the new distance away the camera is from our desired position
        distanceToZoom = Vector3.Distance(transform.position, desiredPosition);
    }
}
