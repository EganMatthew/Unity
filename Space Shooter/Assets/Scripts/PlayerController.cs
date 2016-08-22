using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;

    private float nextFire;
    public float fireRate;

    void Start()
    {
        rb = GetComponent<Rigidbody> ();
        nextFire = 0.0f;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource> ().Play();
        }
    }

    void FixedUpdate ()
    {

        Vector3 movement = new Vector3(0.0f, 0.0f, 0.0f);

        float moveHorizontalKeyboard = Input.GetAxis("Horizontal");
        float moveVerticalKeyboard = Input.GetAxis("Vertical");

        float moveHorizontalMouse = Input.GetAxis("Mouse X");
        float moveVerticalMouse = Input.GetAxis("Mouse Y");

        if (moveHorizontalKeyboard != 0 || moveVerticalKeyboard != 0)
        {
            movement = new Vector3(moveHorizontalKeyboard, 0.0f, moveVerticalKeyboard);
        }
        else if(moveHorizontalMouse != 0 || moveVerticalMouse != 0)
        {
            movement = new Vector3(moveHorizontalMouse, 0.0f, moveVerticalMouse);
        }


        //rb.AddForce(movement * speed);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

}
