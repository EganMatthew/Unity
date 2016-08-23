using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float speed = 5.0f;
    private Vector3 offset;
    public float cameraDistance = 10;
    public float cameraAngle = 45;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
        transform.LookAt(player.transform);
    }

    // Update is called once per frame
    // guarneteed to run after all items have been processed in Update
    void LateUpdate()
    {

        //TODO: Fix problems with offset
        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(player.transform.position, Vector3.up, Input.GetAxis("Mouse X") * speed);
            transform.LookAt(player.transform);
            offset = transform.position - player.transform.position;
        }


        //Place the camera in a position relative to the target
        transform.position = player.transform.position + offset;

        //Now that the camera has been moved, point it towards the target
        transform.LookAt(player.transform);
    }
}











/*  ===STABLE BALL ROLLING===
    transform.position = player.transform.position + offset;
    Quaternion rotation = Quaternion.Euler(45, 0, 0);
    transform.rotation = rotation;
*/
