using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    private int count;

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
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
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
}
