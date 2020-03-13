using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 6.0f;
    protected float gravity = 10;
    [SerializeField]
    protected int distKnockback;

    protected bool vulnerable = true;
    
    [SerializeField]
    float tiempoVul = 2f;
    protected GameObject sprites;
    public float dano = 1;

    [Header("Componentes")]
    [SerializeField]
    protected Transform cam = null;
    protected CharacterController characterController;
    public Animator anim;

    protected float xAxis;
    protected float zAxis;
    public Linea line_sc;

    public enum PlayerState { idle, dash, skill, damaged }

    [Header("Dash")]
    public float dashSpeed;
    protected float dashTime;
    public float startDash;
    protected Vector3 dashVector;
    protected bool dashing = false;
    public float cdDash;

    protected IEnumerator CorVulnerabilidad()
    {
        vulnerable = false;
        yield return new WaitForSeconds(tiempoVul);
        vulnerable = true;
    }

    protected virtual void Rotar()
    {
        sprites.transform.rotation = Quaternion.LookRotation(transform.position - cam.position);
    }

    public void Danado()
    {
        CameraShake.ShakeOnce(0.4f, 0.4f);
        line_sc.RecibirDano();
        StartCoroutine(CorVulnerabilidad());
    }

}
