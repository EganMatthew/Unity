using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;

    //called on the first frame that the script is active
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText ();
        winText.text = "";
    }

    //called before rendering a frame.
    //this is where most of our game code will go.
    void Update ()
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
            SetCountText ();
            if(count >= 8)
            {
                winText.text = "You Win!";
            }
        }
    }

    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString();
    }
}
