using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float run = 7f;
    [SerializeField] private float jump = 2f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float maxRunCoef = 1.5f;
    private float realMaxSpeed;
    private float speedX;
    private float speedZ;
    private float speedY;
    private Rigidbody rb;
    private float inputX;
    private float inputY;
    private bool inGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");        
        Movement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Movement()
    {
        Vector3 CameraForward = this.transform.forward;
        Vector3 CameraRight = this.transform.right;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            realMaxSpeed = maxSpeed * maxRunCoef;
        }
        else
        {
            realMaxSpeed = maxSpeed;
        }

        if (inputX != 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.AddForce(CameraRight * inputX * run);
        }
        else if (inputX != 0)
        {
            rb.AddForce(CameraRight * inputX * speed);
        }
        if (inputY != 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.AddForce(CameraForward * inputY * run);
        }
        else if (inputY != 0)
        {
            rb.AddForce(CameraForward * inputY * speed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, Vector3.down, 1.9f))
        {
            rb.AddForce(new Vector3(0, jump, 0), ForceMode.Impulse);
        }


        if (rb.velocity.x >= realMaxSpeed)
        {
            speedY = rb.velocity.y;
            speedZ = rb.velocity.z;
            rb.velocity = new Vector3(realMaxSpeed, speedY, speedZ);
        }
        else if (rb.velocity.x <= -1 * realMaxSpeed)
        {
            speedY = rb.velocity.y;
            speedZ = rb.velocity.z;
            rb.velocity = new Vector3(-1 * realMaxSpeed, speedY, speedZ);
        }

        if (rb.velocity.z >= realMaxSpeed)
        {
            speedY = rb.velocity.y;
            speedX = rb.velocity.x;
            rb.velocity = new Vector3(speedX, speedY, realMaxSpeed);
        }
        else if (rb.velocity.z <= -1 * realMaxSpeed)
        {
            speedY = rb.velocity.y;
            speedX = rb.velocity.x;
            rb.velocity = new Vector3(speedX, speedY, -1 * realMaxSpeed);
        }
    }

    private void Jump()
    {
        RaycastHit hit;
        Ray ray = new Ray (this.transform.position, new Vector3 (0,-1,0));
        if (Physics.Raycast(ray, out hit, 1f))
        {
            rb.AddForce(0, jump, 0, ForceMode.Impulse);
        }
    }
}
