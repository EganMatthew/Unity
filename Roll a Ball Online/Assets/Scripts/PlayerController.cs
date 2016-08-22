using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public Camera playerCamera;

    private Rigidbody rb;
    private int count;

    //called on the first frame that the script is active
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText ();
        winText.text = "";
		countText.text = "Count: " + count.ToString();
    }

    //called before rendering a frame.
    //this is where most of our game code will go.
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (playerCamera.enabled == false)
        {
            playerCamera.enabled = true;
        }
    }
    //called just before performing any physics calculations.
    //this is where our physics code will go.
    void FixedUpdate ()
    {
		if (!isLocalPlayer)
        {
            return;
        }
		
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }
	
	public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
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
