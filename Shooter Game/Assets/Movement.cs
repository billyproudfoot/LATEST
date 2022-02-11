using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    Vector3 velocity;
    public Transform CheckOnGround;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float movementSpeed = 0.8f;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        try
        {
        isGrounded = Physics.CheckSphere(CheckOnGround.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.Mouse0))
        {
            movementSpeed = 1.6f;
        }
        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");
        Vector3 move = transform.right * xDirection * movementSpeed + transform.forward * zDirection * movementSpeed;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        movementSpeed = 1f;
        }
        catch
        {
        }
    }
}
