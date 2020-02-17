﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rosa : Jugador
{
    CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    private Animator anim;

    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
       
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

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);

        if (xAxis == 0 && zAxis == 0)
            SetSpeedValue(0);
        else
            SetSpeedValue(1);
        SetDirectionValue(Input.GetAxis("Horizontal"));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemigos" && vulnerable == true)
        {
            base.OnTriggerEnter(other);
            Vector3 dir = ((this.transform.position - other.transform.position).normalized * distKnockback * Time.deltaTime);
            characterController.Move(dir);
            Debug.Log("knockback");
        }
    }

    protected override void Rotar()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.position);
    }

    private void SetSpeedValue(float speed)
    {
        anim.SetFloat("Speed", speed);
    }
    private void SetDirectionValue(float dir)
    {
        anim.SetFloat("Direction", dir);
    }
}
