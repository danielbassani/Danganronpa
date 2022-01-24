using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Player script for movement. Horizontal and vertical movement is done during the regular update, while physics-based jumping is done in fixed update
 */
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 14f;
        }else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 10f;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
