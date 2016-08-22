using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CameraController : NetworkBehaviour
{

    public GameObject player;
    public float speed = 5.0f;
    private Vector3 offset;

    // Use this for initialization
    void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
    // guarneteed to run after all items have been processed in Update
	void LateUpdate () {

        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(player.transform.position, Vector3.up, Input.GetAxis("Mouse X") * speed);
        }

        Vector3 offset2 = transform.position - player.transform.position;
        float x = player.transform.position.x + offset2.x;
        float y = player.transform.position.y;
        float z = player.transform.position.z + offset2.z;
        Vector3 newPos = new Vector3(x, y, z);
        transform.position = newPos;
        transform.LookAt(player.transform);
    }
}











/*  ===STABLE BALL ROLLING===
    transform.position = player.transform.position + offset;
    Quaternion rotation = Quaternion.Euler(45, 0, 0);
    transform.rotation = rotation;
*/