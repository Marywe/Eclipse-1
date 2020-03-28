using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Clase encargada de las especificaciones de los personajes, es decir, sus caracteristicas.
//Todos los componentes necesarios para sus estadisticas dentro del juego.
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

    /// <summary>
    /// Maquina de Estados para los personajes, en este caso para los estados en los que pueden encontrarse
    /// </summary>
    public enum PlayerState { idle, dash, skill, damaged }
    public float cdbasicAttack;

    [Header("Skills")]
    public float dashSpeed;
    protected float dashTime;
    public float startDash;
    protected Vector3 dashVector;
    protected bool dashing = false;
    public float cdDash;


    protected float skillTime;
    public float startSkill;
    public float cdSkill;
    protected bool skilling = false;
    public bool onShield = false;

    /// <summary>
    /// Corrutina para la mecánica de daño sobre los personajes, cambiando el estado de vulnerabilidad de la linea.
    /// </summary>
    /// <returns></returns>
    protected IEnumerator CorVulnerabilidad()
    {
        vulnerable = false;
        yield return new WaitForSeconds(tiempoVul);
        vulnerable = true;
    }
     
    /// <summary>
    /// Funcion para rotar los sprites de los personajes en relacion a la camara
    /// </summary>
    


    protected virtual void Rotar()
    {
        sprites.transform.rotation = Quaternion.LookRotation(transform.position - cam.position);
    }

    /// <summary>
    /// Daño hacia el jugador.
    /// Realiza la funcion de la camara de shake,
    /// Aplica el daño correspondiente e
    /// Inicia el cooldown con la corrutina.
    /// </summary>
    public void Danado()
    {
        CameraShake.ShakeOnce(0.4f, 0.4f);
        line_sc.RecibirDano();
        StartCoroutine(CorVulnerabilidad());
    }

}
