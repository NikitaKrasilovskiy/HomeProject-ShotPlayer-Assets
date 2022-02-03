using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    public float speed = 4f;
    public float jumpSpeed = 0.8f;
    public float gravity = 20f;
    private Vector3 moveDir = Vector3.zero;
    private CharacterController controller;
    private float maxSpeed;
    private float realHeight;
    private float crounch;
    private float run;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        maxSpeed = speed;
        realHeight = controller.height;
        crounch = speed / 2;
        run = speed * 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = 1f;
            speed = crounch;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = run;
            controller.height = realHeight;
        }
        else
        {
            speed = maxSpeed;
            controller.height = realHeight;
        }

        if (controller.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            moveDir.y = jumpSpeed;
        }
      

        moveDir.y -= gravity * Time.deltaTime;

        controller.Move(moveDir * Time.deltaTime);
    }
}
