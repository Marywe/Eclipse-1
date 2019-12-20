using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Azul : Jugador
{
    CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("HorizontalArrow"), 0.0f, Input.GetAxis("VerticalArrow"));
            moveDirection *= speed;
        }

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);

        base.Rotar();
    }

}
