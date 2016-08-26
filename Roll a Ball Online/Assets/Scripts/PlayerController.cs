using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

// Ball chasing mouse movement
    //Ball is capable of moving even when the mouse is still
// Mouse adding force to ball movement
    //The mouse must be moving for movement, the mouse is the ball

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    private int count;
    public Camera playerCamera;

    // Called on the first frame that the script is active
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
    }

    // Called before rendering a frame.
    // This is where most of our game code will go.
    void Update()
    {
        //transform.Translate(new Vector3(10, 10, 10) * Time.deltaTime * .1f);
    }

    // Called just before performing any physics calculations.
    // This is where our physics code will go.
    void FixedUpdate ()
    {
        //movePlayer_Keyboard();
        movePlayer_Mouse2();
        //movePlayer_Mouse1();
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

    void movePlayer_Mouse1()
    {
        // TODO: Allow for mouse detection in empty space (or detect the mouse cursor by some other means entirely)
        
        /*****
        * The player will constantly try to move to the on screen mouse position
        *****/

        // Set up variables for calculating mouse position on the screen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; // hit.point is the position of your mouse

        // If the mouse cursor is located somewhere on the screen
        if (Physics.Raycast(ray, out hit, 100))
        {
            // Save the vector that represents the mouse location on the screen
            Vector3 totalForce = hit.point;

            // Set the y component to our player position to prepare for subtraction, we want 0 force in the y direction
            totalForce.y = transform.position.y;

            // Subtract the mouse position vector from our player position vector to determine the direction of our force
            totalForce = totalForce - transform.position;

            // Normalize our force so we can multiply by our speed
            totalForce.Normalize();

            // Create the motion via our force
            rb.AddForce(totalForce * speed);
        }
    }


    void movePlayer_Mouse2()
    {
        // TODO: Hide mouse cursor in this mode? Maybe not, windowed mode would be weird

        /*****
        * The player will only move when the mouse is moved
        *****/

        // Poll any avaiable mouse data
        float forceY = Input.GetAxis("Mouse Y");
        float forceX = Input.GetAxis("Mouse X");

        // Generate a force vector based on the mouse movement data
        Vector3 force = new Vector3(forceX, 0, forceY);

        // Normalize the force vector to be used as a direction
        force.Normalize();

        // Create the motion via our force * speed
        rb.AddForce(force * speed);
    }

    void movePlayer_Keyboard()
    {
        /*****
        * The player will move when keys on the keyboard are pressed
        *****/

        // Obtain data from key presses
        float horizontalKeyData = Input.GetAxis("Horizontal");
        float VerticalKeyData = Input.GetAxis("Vertical");

        // Force to the applied on our player
        Vector3 horizontalForce = new Vector3();
        Vector3 verticalForce = new Vector3();

        // Camera's reletaive position
        Vector3 cameraTargetLine = transform.position - playerCamera.transform.position;

        // If we have horizontal data, we are strafing (left or right)
        if (horizontalKeyData != 0)
        {
            // Create a force perpendicular to the cameraTargetLine
            horizontalForce = new Vector3(cameraTargetLine.z, 0.0f, cameraTargetLine.x * -1);

            // Ensure the direction of the force is in the desired direction
            horizontalForce.x *= Mathf.Sign(horizontalKeyData);
            horizontalForce.z *= Mathf.Sign(horizontalKeyData);
        }

        // If we have vertical data, we are moving forward or backward
        if (VerticalKeyData != 0)
        {
            // Create a force parallel to the cameraTargetLine
            verticalForce = new Vector3(cameraTargetLine.x, 0.0f, cameraTargetLine.z);
            // Ensure the direction of the force is in the desired direction
            verticalForce.x *= Mathf.Sign(VerticalKeyData);
            verticalForce.z *= Mathf.Sign(VerticalKeyData);
        }

        // Sum the forces from our inputs and normalize it
        Vector3 totalForce = horizontalForce + verticalForce;
        totalForce.Normalize();

        // Apply the force to our player
        rb.AddForce(totalForce * speed);
    }

}
