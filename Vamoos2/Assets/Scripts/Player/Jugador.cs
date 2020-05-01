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
    public bool tp = false;
    [SerializeField]
    float tiempoVul = 2f;
    protected GameObject sprites;
    public float dano = 1;

    [Header("Componentes")]
    [SerializeField]
    protected Transform cam = null;
    public CharacterController characterController;
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
        CameraShake.ShakeOnce(0.5f, 0.5f);
        line_sc.RecibirDano();
        StartCoroutine(CorVulnerabilidad());
    }

    protected Vector3 GetOffset()
    {
        //Variable del offset al entrar en la sala
        int sala = (int)Controlador.instance.dondeEstas;
        Vector3 offsetEntrada = Vector3.zero;
        switch (sala)
        {
            case 0: //DONE
                offsetEntrada = new Vector3(-2f, 0, 0.6f);
                break;
            case 1: //DONE
                offsetEntrada = new Vector3(2f, 0, -2f);
                break;
            case 2: //DONE
                offsetEntrada = new Vector3(0, 0, 3f);
                break;
            case 3: //DONE
                offsetEntrada = new Vector3(2f, 0, 2f);
                break;
            case 4: //Sala principal
                break;
            case 5: //Sala pasillo
                offsetEntrada = new Vector3(2f, 0, 2f);
                break;
            case 6: //Sala pasillo
                offsetEntrada = new Vector3(2f, 0, 2f);
                break;
            case 7: //Sala pasillo
                offsetEntrada = new Vector3(2f, 0, 2f);
                break;
            case 8: //Sala pasillo
                offsetEntrada = new Vector3(2f, 0, 2f);
                break;
            case 9: //DONE
                offsetEntrada = new Vector3(0, 0, 2f);
                break;
            case 10: //Sala pasillo
                offsetEntrada = new Vector3(2f, 0, 2f);
                break;
            case 11: //Sala pasillo
                offsetEntrada = new Vector3(2f, 0, 2f);
                break;
            case 12: //Sala pasillo
                offsetEntrada = new Vector3(2f, 0, 2f);
                break;
        }

        return offsetEntrada;
    }

}
