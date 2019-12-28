using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rosa : Jugador
{
    CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private Animator anim;
    private bool moving = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = (Animator)gameObject.GetComponent(typeof(Animator)); //No me acordaba de que esta era la forma buena así que el resto se queda como está que pa algo es un juego indie
    }

    void Update()
    {
        Movimiento();
        base.Rotar();
    }


    private void Movimiento()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(xAxis, 0.0f, zAxis);
            moveDirection *= speed;          
        }

        if(xAxis ==0 && zAxis==0) moving = false;
        else moving = true;

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
        anim.SetBool("Walking", moving);
    }
    
}
