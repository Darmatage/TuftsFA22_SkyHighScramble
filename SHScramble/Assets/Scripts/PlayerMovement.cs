using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject Camera;
    private float speed = 7f;
    public float defaultSpeed = 7f;
    public float sprintSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask whatIsGround;


    Vector3 velocity;
    bool grounded;


    void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);

        if(!grounded)
            StopBobbing();

        if(grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        if(grounded)
        {
            if(Input.GetKey(KeyCode.W))
                StartBobbing();
            if(Input.GetKey(KeyCode.S))
                StartBobbing();
            if(Input.GetKey(KeyCode.D))
                StartBobbing();
            if(Input.GetKey(KeyCode.A))
                StartBobbing();
            if(Input.GetKeyUp(KeyCode.W))
                StopBobbing();
            if(Input.GetKeyUp(KeyCode.S))
                StopBobbing();
            if(Input.GetKeyUp(KeyCode.D))
                StopBobbing();
            if(Input.GetKeyUp(KeyCode.A))
                StopBobbing();

            if(Input.GetButtonUp("Sprint"))
                speed = defaultSpeed;
            else if(Input.GetButton("Sprint") == true)
                speed = sprintSpeed;
        }

        controller.Move(move * speed *Time.deltaTime);

        if(Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    void StartBobbing(){ Camera.GetComponent<Animator>().Play("HeadBobbing"); }
    void StopBobbing() { Camera.GetComponent<Animator>().Play("New State");}
}
