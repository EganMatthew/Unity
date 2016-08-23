using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject target;
    public float rotateSpeed = 5.0f;
    public float ZoomSpeed = 10f;
    public float maxZoomDistance = 90f;
    public float minZoomDistance = 15f;
    private Vector3 offset;
    public float cameraDistance = 10;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - target.transform.position;
        transform.LookAt(target.transform);
    }

    // Update is called once per frame
    // guarneteed to run after all items have been processed in Update
    void LateUpdate()
    {

        // Place the camera in a position relative to the target
        transform.position = target.transform.position + offset;

        // If the right mouse button is pressed
        if (Input.GetMouseButton(1))
        {
            // Allow for rotation on the X axis (left and right)
            transform.RotateAround(target.transform.position, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed);
            // TODO: Camera rotates 180 degrees on the y axis when at the limits of the RotateAround
            // TODO: Limit the camera angle
            // Allow for rotation on the Y axis (up and down)
            transform.RotateAround(target.transform.position, transform.right * -1, Input.GetAxis("Mouse Y") * rotateSpeed);
            // Caclulate a new camera position relative to our target
            offset = transform.position - target.transform.position;
        }

        //Acquire the camera's current field of view value
        float fov = Camera.main.fieldOfView;
        //Adjust the field of view determined by the mouse scroll wheel
        //Multiple by -1 to invert scroll direction
        fov += Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed * -1;
        //Limit how close or far we can zoom
        fov = Mathf.Clamp(fov, minZoomDistance, maxZoomDistance);
        //Set the new field of view for the camera
        Camera.main.fieldOfView = fov;


        // Now that the camera has been moved, point it towards the target
        transform.LookAt(target.transform);
    }
}
