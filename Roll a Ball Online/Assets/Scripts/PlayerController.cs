using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    private int count;
    public Camera playerCamera;

    //called on the first frame that the script is active
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
    }

    //called before rendering a frame.
    //this is where most of our game code will go.
    void Update()
    {

    }
    //called just before performing any physics calculations.
    //this is where our physics code will go.
    void FixedUpdate ()
    {
        movePlayer();
    }

    void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
        if (other.gameObject.CompareTag ("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
        }
    }

    void movePlayer()
    {
        // Obtain data from key presses
        float horizontalKeyData = Input.GetAxis("Horizontal");
        float VerticalKeyData = Input.GetAxis("Vertical");

        // Force to the applied on our player
        Vector3 horizontalMovement = new Vector3();
        Vector3 verticalMovement = new Vector3();

        // Camera's reletaive position
        Vector3 offset = transform.position - playerCamera.transform.position;

        // If we have horizontal data, we are strafing (left or right)
        if (horizontalKeyData != 0)
        {
            // Create a force perpendicular to the camera face
            horizontalMovement = new Vector3(offset.z, 0.0f, offset.x * -1);

            // Ensure the direction of the force is in the desired direction
            horizontalMovement.x *= Mathf.Sign(horizontalKeyData);
            horizontalMovement.z *= Mathf.Sign(horizontalKeyData);
        }

        // If we have vertical data, we are moving forward or backward
        if (VerticalKeyData != 0)
        {
            // Create a force parallel to the camera face
            verticalMovement = new Vector3(offset.x, 0.0f, offset.z);
            // Ensure the direction of the force is in the desired direction
            verticalMovement.x *= Mathf.Sign(VerticalKeyData);
            verticalMovement.z *= Mathf.Sign(VerticalKeyData);
        }

        // Sum the forces from our inputs and normalize it
        Vector3 movement = horizontalMovement + verticalMovement;
        movement.Normalize();

        // Apply the force to our player
        rb.AddForce(movement * speed);
    }

}
