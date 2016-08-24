using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject target;
    public float rotateSpeed;
    public float zoomSpeed;
    public float maxZoomDistance;
    public float minZoomDistance = 15f;
    private Vector3 offset;
    //private Vector3 newCameraPosition;

    // Use this for initialization
    void Start()
    {
        //newCameraPosition = Camera.main.transform.position;
        rotateSpeed = 5.0f;
        zoomSpeed = 1f;
        maxZoomDistance = 90f;
        minZoomDistance = 15f;

        offset = transform.position - target.transform.position;
        transform.LookAt(target.transform);
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
        // Move the camera relative to the target
        transform.position = target.transform.position + offset;
    }

    void yRotationHandler()
    {
        // TODO: Limit the camera angle

        // If the right mouse button is pressed
        if (Input.GetMouseButton(1))
        {
            // Allow for rotation on the Y axis (up and down)
            transform.RotateAround(target.transform.position, transform.right * -1, Input.GetAxis("Mouse Y") * rotateSpeed);

            // Caclulate the new camera direction/position relative to our target
            offset = transform.position - target.transform.position;
        }
    }

    void xRotationHandler()
    {
        // If the right mouse button is pressed
        if (Input.GetMouseButton(1))
        {
            // Allow for rotation on the X axis (left and right)
            transform.RotateAround(target.transform.position, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed);

            // Caclulate the new camera direction/position relative to our target
            offset = transform.position - target.transform.position;
        }
    }

    void zoomHandler()
    {
        //TODO: Smooth zoom
        //TODO: limit zoom depths

        // Get any potential data caused by the scroll wheel
        float scrollEventData = Input.GetAxis("Mouse ScrollWheel");

        // If scrollwheel data exists
        if (scrollEventData != 0)
        {
            Vector3 slope = offset * Mathf.Sign(scrollEventData) * -1;
            slope.Normalize();
            Vector3 newCameraPosition = Camera.main.transform.position;
            //zoomSpeed = 50;
            newCameraPosition += (slope * zoomSpeed);
            Camera.main.transform.position = newCameraPosition;
            //Vector3.Lerp(Camera.main.transform.position, newCameraPosition, Time.deltaTime * 2);
            offset = transform.position - target.transform.position;
        }

        //Debug.Log(Camera.main.transform.position);
        //Debug.Log(newCameraPosition);
        //Vector3.Lerp(Camera.main.transform.position, newCameraPosition, Time.deltaTime * 2);
    }
}
