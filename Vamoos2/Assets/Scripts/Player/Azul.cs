﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Controla movimiento
/// </summary>
public class Azul : Jugador
{
    private Vector3 moveDirection = Vector3.zero;
    public PlayerState playerState;


    void Start()
    {
        playerState = PlayerState.idle;
        anim.SetFloat("Direction", 1);
        dashTime = startDash;
        characterController = GetComponent<CharacterController>();
        sprites = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        this.Movimiento();
        base.Rotar();
        Rotar();

        #region Dash
        dashVector = new Vector3(moveDirection.x, 0, moveDirection.z).normalized;
        if (dashVector == Vector3.zero) dashVector = Vector3.right * anim.GetFloat("Direction");
        if (Input.GetKeyDown(KeyCode.L) && playerState==PlayerState.idle && !dashing)
        {
            playerState = PlayerState.dash;
            StartCoroutine(corDash());
        }

        Dash();
        #endregion
    }

    private void Movimiento()
    {
        xAxis = Input.GetAxis("HorizontalArrow");
        zAxis = Input.GetAxis("VerticalArrow");

        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(xAxis, 0.0f, zAxis);
            moveDirection *= speed;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        if (playerState == PlayerState.idle || playerState == PlayerState.skill) characterController.Move(moveDirection * Time.deltaTime);
        else characterController.Move(new Vector3(0, moveDirection.y , 0 )* Time.deltaTime);

        if (xAxis == 0 && zAxis == 0)
            SetSpeedValue(0);
        else
            SetSpeedValue(1);

        if(xAxis!=0)
        SetDirectionValue(xAxis);
    }
    protected void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Enemigos" || other.gameObject.tag == "Bullet") && vulnerable == true)
        {
            RecibirGolpe(other.transform);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "Enemigos" || other.gameObject.tag == "Bullet") && vulnerable == true)
        {
            RecibirGolpe(other.transform);
        }
    }
    public void RecibirGolpe(Transform other)
    {
        anim.SetTrigger("TakeDmg");
        playerState = PlayerState.damaged;
        Invoke("NoHacerNadaMientrasTeDan", 0.3f);
        Danado();
        Vector3 dir = ((this.transform.position - other.transform.position).normalized * distKnockback * Time.deltaTime);
        characterController.Move(dir);
    }
    protected override void Rotar()
    {
        Vector3 look;
        look.x = transform.position.x - cam.position.x;
        look.y = 0;
        look.z = transform.position.z - cam.position.z;
        transform.rotation = Quaternion.LookRotation(look);

    }

    private void SetSpeedValue(float speed)
    {
        if (speed > 0)
            anim.SetFloat("Speed", 1);

        if (speed <= 0)
            anim.SetFloat("Speed", 0);
    }
    private void SetDirectionValue(float dir)
    {
        if (dir > 0)
            anim.SetFloat("Direction", 1);
        if (dir < 0)
            anim.SetFloat("Direction", -1);
    }

    private void Dash() 
    {
        if (dashTime <= 0 && playerState == PlayerState.dash)
        {
            playerState = PlayerState.idle;
        }
        else if (!dashing)
            dashTime = startDash;
        else if (dashTime > 0 && playerState == PlayerState.dash)
        {
            characterController.Move(dashVector * Time.deltaTime * dashSpeed);
            dashTime -= Time.deltaTime;
        }

    }

    IEnumerator corDash()
    {
        dashing = true;
        yield return new WaitForSeconds(cdDash);
        dashing = false;
    }

    public IEnumerator corrSkill()
    {
        skilling = true;
        yield return new WaitForSeconds(cdSkill);
        skilling = false;
    }
    private void NoHacerNadaMientrasTeDan()
    {
        playerState = PlayerState.idle;
    }

    public void HabilidadGirar(Collider[] enemiesHit, LayerMask enemyLayer, float radius)
    {
        if (skillTime <= 0 && playerState == PlayerState.skill)
        {
            playerState=PlayerState.idle;
        }
        else if (!skilling) skillTime = startSkill;
        else if (skillTime > 0 && playerState == PlayerState.skill)
        {
            enemiesHit = Physics.OverlapSphere(this.transform.position, radius, enemyLayer);

            foreach (Collider enemy in enemiesHit)
            {
                enemy.GetComponent<Enemigos>().TakeDamage(0.1f);
            }
            skillTime -= Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Intangible"))
        {
            speed -= other.gameObject.GetComponent<EscudoHabilidad>().speed;
            dano -= other.gameObject.GetComponent<EscudoHabilidad>().dano;
        }
    }
}
