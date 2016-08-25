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

    // Use this for initialization
    void Start()
    {
        rotateSpeed = 5.0f;
        zoomSpeed = 1f;
        maxZoomDistance = 90f;
        minZoomDistance = 15f;

        transform.LookAt(target.transform);
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    // guarneteed to run after all items have been processed in Update
    void LateUpdate()
    {
        // Enable the camera to move with the target
        //positionHandler();

        // Enable camera zoom on our target
        zoomHandler();

        // Enable camera rotation on the X axis
        //xRotationHandler();

        // Enable camera rotation on the Y axis
        //yRotationHandler();
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

    float distance = 0;
    bool zooming = false;

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
            Vector3 newCameraPosition = transform.position;
            newCameraPosition += slope;
            distance = Vector3.Distance(newCameraPosition, transform.position);
            zooming = true;
            //transform.position = newCameraPosition;
            //offset = transform.position - target.transform.position;
        }

        if (zooming)
        {
            Vector3 newCameraPosition2 = transform.position;
            Vector3 slope2 = offset * Mathf.Sign(scrollEventData) * -1;
            slope2.Normalize();
            newCameraPosition2 += slope2;

            Debug.Log(slope2);

            if (Vector3.Distance(transform.position, newCameraPosition2) - distance > .001)
            {
                transform.position = Vector3.Lerp(transform.position, newCameraPosition2, Time.deltaTime * .1f);
                //transform.Translate(target.transform.position * Time.deltaTime);
                //Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * 5);
                offset = transform.position - target.transform.position;
                return;
            }
            zooming = false;
        }
    }
}
