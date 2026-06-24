using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementControl : MonoBehaviour
{

    private GrabObject grabObject; //using script instance to see if the character is in grab mode
    private CharacterController2D controller; //using script instance to move the character
    public float horizontalMove;
    float runSpeed = 40f;
    private bool jump;  //used to determine if the character should jump
    private bool flip;  //used to determine if the character should flip


    private void Start()
    {
        if (grabObject == false)
        {
            grabObject = GetComponentInChildren<GrabObject>();
        }

        if (controller == false)
        {
            controller = GetComponent<CharacterController2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //calculates the horizontal move (1 or -1) * runSpeed 
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump") && !grabObject.IsGrabbing)
        {
            jump = true;
        }

        //if the character is grabbing it should not flip
        if (grabObject.IsGrabbing)
            flip = false;
        else
            flip = true;
    }

    void FixedUpdate()
    {
        //Move character
        controller.Move(horizontalMove * Time.fixedDeltaTime,false,jump,flip);
        jump = false;
    }

}
