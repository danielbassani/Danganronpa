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

    // Update is called once per frame
    void Update()
    {
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
    }
}
