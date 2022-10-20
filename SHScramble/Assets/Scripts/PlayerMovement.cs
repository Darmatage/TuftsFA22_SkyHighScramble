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
    public StaminaBar staminabar;

    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask whatIsGround;


    Vector3 velocity;
    public bool grounded;

    void Start()
    {
        speed = defaultSpeed;
    }


    void Update()
    {
        float stamina = staminabar.stamina;
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

            if(Input.GetButtonUp("Sprint") || (stamina <= 1))
                speed = defaultSpeed;
            else if((Input.GetButton("Sprint") == true) && (stamina > 0) && !staminabar.tired)
            {
                speed = sprintSpeed;
            }
        }

        controller.Move(move * speed *Time.deltaTime);

        // if(Input.GetButtonDown("Jump") && grounded)
        // {
        //     velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        // }


        velocity.y += gravity * 3 * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    void StartBobbing(){ Camera.GetComponent<Animator>().Play("CamBob2"); }
    void StopBobbing() { Camera.GetComponent<Animator>().Play("New State");}
}
