using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controla movimiento del personaje Araxiel
public class Rosa : Jugador
{   
    private Vector3 moveDirection = Vector3.zero;
    public PlayerState playerState; 

    public GameObject escudoTemp;
    Vector3 posicionEscudoSuelo;

    public GameObject azul;

    public Image skill;
    void Start()
    {
        particulaRecibirDano = transform.GetChild(2).GetComponent<ParticleSystem>();

        playerState = PlayerState.idle;
        anim.SetFloat("Direction", 1);
        dashTime = startDash;
        characterController = GetComponent<CharacterController>();
        sprites = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        Movimiento();
        base.Rotar();
        Rotar();

        #region Dash
        dashVector = new Vector3(moveDirection.x, 0, moveDirection.z).normalized;
        if (dashVector == Vector3.zero) dashVector = Vector3.right * anim.GetFloat("Direction");
        if (!dashing) dash.color = new Color(255, 255, 255);
        if (Input.GetButtonDown("DASH1") && playerState == PlayerState.idle && !dashing)
        {
            dash.color = new Color(0, 0, 0);
            anim.SetTrigger("Dashing");
            playerState = PlayerState.dash;
            StartCoroutine(corDash());
        }
        Dash();
        #endregion

        #region Skill Escudo
        if (!skilling) skill.color = new Color(255, 255, 255);
        if (Input.GetButtonDown("HAB1") && playerState == PlayerState.idle && !skilling)
        {
            skill.color = new Color(0, 0, 0);
            posicionEscudoSuelo = transform.position;
            playerState = PlayerState.skill;
            StartCoroutine(corrSkill());
            anim.SetTrigger("Skill");
        }
        HabilidadEscudo();
        #endregion

        if (!escudoTemp.activeSelf && EscudoHabilidad.temporal)
        {
            escudoTemp.GetComponent<EscudoHabilidad>().Disabled();
        }
    
    }

    #region Movimiento
    /// <summary>
    /// Movimiento del personaje, obtención de su posicion y modificación de su estado
    /// </summary>
    private void Movimiento()
    {
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");

        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(xAxis, 0.0f, zAxis);
            moveDirection *= speed;          
        }

        moveDirection.y -= gravity * Time.deltaTime;
        
        if(playerState==PlayerState.idle || playerState==PlayerState.skill)characterController.Move(moveDirection * Time.deltaTime);
        else characterController.Move(new Vector3(0, moveDirection.y, 0) * Time.deltaTime);

        if (xAxis == 0 && zAxis == 0)
            SetSpeedValue(0);
        else
            SetSpeedValue(1);

        if (xAxis != 0)
            SetDirectionValue(xAxis);
    }
    #endregion

    #region Habilidades

    /// <summary>
    /// Habilidad del personaje.
    /// Cambio de estado, y realiazacion de la habiliad, desplazamiento de posicion.
    /// </summary>
    private void HabilidadEscudo()
    {
        if (skillTime <= 0 && playerState == PlayerState.skill)
        {


            onShield = false;
            playerState = PlayerState.idle;
            escudoTemp.SetActive(false);
        }
        else if (!skilling)
        {
            escudoTemp.SetActive(false);
            escudoTemp.transform.position = transform.position;
            skillTime = startSkill;
        }
        else if (skillTime > 0 && playerState == PlayerState.skill)
        {
            escudoTemp.transform.position = posicionEscudoSuelo;
            Invoke("ActivarEscudo", 0.6f);
            skillTime -= Time.deltaTime;
        }
    }

    void ActivarEscudo()
    {
        escudoTemp.SetActive(true);
    }

    /// <summary>
    /// Habilidad del personaje.
    /// Cambio de estado, y realiazacion de la habiliad, desplazamiento de posicion.
    /// </summary>
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

    #endregion

    #region Colisiones
    //Colisiones con objetos
    protected void OnTriggerEnter(Collider other)
    {
        if (((other.gameObject.tag == "Enemigos" && other.GetComponent<Mariposa>()!=null)|| other.gameObject.tag == "Bullet") && vulnerable == true)
        {
            RecibirGolpe(other.transform);
        }

        if (other.gameObject.tag == "Salas" && !tp) //Para que no se tp los dos según entre cada uno y solo lo haga 1
        {
            Debug.Log("ghj");
            Vector3 offsetEntrada = base.GetOffset();
            //TP
            Azul a = azul.GetComponent<Azul>();
            a.tp = true;
            a.characterController.enabled = false;
            azul.transform.position = this.transform.position + offsetEntrada; //variable según la sala en la q estas
            a.characterController.enabled = true;
            StartCoroutine(TP());
        }


    }
    public IEnumerator TP()
    {
        yield return new WaitForSeconds(0.1f);
        azul.GetComponent<Azul>().tp = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (((other.gameObject.tag == "Enemigos" && other.GetComponent<Mariposa>() != null )|| other.gameObject.tag == "Bullet") && vulnerable == true)
        {
            RecibirGolpe(other.transform);
        }
    }
    
    /*private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Intangible"))
        {
            DevolverStats(other.gameObject);
        }
    }*/
    #endregion

    #region Variables de Velocidad y Direccion del jugador
    public void SetSpeedValue(float speed)
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
    #endregion

    #region Cooldowns habilidades Personaje y Cambio de Estado a Idle
    IEnumerator corDash()
    {
        dashing = true;
        yield return new WaitForSeconds(cdDash);
        dashing = false;
    }

    IEnumerator corrSkill()
    {
        skilling = true;
        yield return new WaitForSeconds(cdSkill);
        skilling = false;
    }
    private void NoHacerNadaMientrasTeDan()
    {
        playerState = PlayerState.idle;
    }
    #endregion

    /// <summary>
    /// Personaje dañado, cambio de animacion, de estado.
    /// </summary>
    public void RecibirGolpe(Transform other)
    {
        if (vulnerable && playerState != PlayerState.dash)
        {
            particulaRecibirDano.Play();
            anim.SetTrigger("TakeDmg");
            playerState = PlayerState.damaged;
            Invoke("NoHacerNadaMientrasTeDan", 0.3f);
            Danado();
            Vector3 dir = ((this.transform.position - other.transform.position).normalized * distKnockback * Time.deltaTime);
            characterController.Move(new Vector3(dir.x, 0, dir.z));
        }
    }
    protected override void Rotar()
    {
        Vector3 look;
        look.x = transform.position.x - cam.position.x;
        look.y = 0;
        look.z = transform.position.z - cam.position.z;
        transform.rotation = Quaternion.LookRotation(look);
    }
    
    /*void DevolverStats(GameObject other)
    {
        speed -= other.GetComponent<EscudoHabilidad>().speed;
        dano -= other.GetComponent<EscudoHabilidad>().dano;
        onShield = false;
    }*/
}
