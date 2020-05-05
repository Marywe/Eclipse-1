using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Enemigo herenciado, como los demas, caracteristicas generales y específicas
public class Scorpio : Enemigos
{
    private bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        objetivo1 = Controlador.instance.objetivo1;
        objetivo2 = Controlador.instance.objetivo2;
        shield = transform.GetChild(1).gameObject;
        maxHealth = 5;
        currentHealth = maxHealth;
        agent = gameObject.GetComponent<NavMeshAgent>();
        animE = (Animator)gameObject.GetComponentInChildren(typeof(Animator));

        sr = gameObject.GetComponentInChildren<SpriteRenderer>();

        particleSpawn = transform.GetChild(2).GetComponent<ParticleSystem>();
        Spawn();
    }

    protected void Spawn()
    {
        sr.enabled = false;
        agent.isStopped = true;
        particleSpawn.Play();
        StartCoroutine(corEnable());
    }

    IEnumerator corEnable()
    {
        
        yield return new WaitForSeconds(2.5f);
        agent.isStopped = false;
        sr.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        //base.MirarObjetivo(cam);

        #region Seguimiento
        //Con esto podemos modificar 
        Vector3 vectorMov1 = new Vector3(objetivo1.position.x - this.transform.position.x, objetivo1.position.y - this.transform.position.y, objetivo1.position.z - this.transform.position.z);
        Vector3 vectorMov2 = new Vector3(objetivo2.position.x - this.transform.position.x, objetivo2.position.y - this.transform.position.y, objetivo2.position.z - this.transform.position.z); ;

        float distancia1 = Vector3.Distance(objetivo1.position, transform.position);
        float distancia2 = Vector3.Distance(objetivo2.position, transform.position);

        if (distancia1 <= radioVision && distancia1 < distancia2 && distancia1 > agent.stoppingDistance && (mov != Vector3.zero))
        {
            SetSpeedValue(1);
            mov = vectorMov1;
            agent.SetDestination(objetivo1.position);
            
        }
        else if (distancia1 <= agent.stoppingDistance && distancia1 < distancia2)
        {
            SetSpeedValue(0);
            if (puedeDisparar && !attacking)
                Atacar(objetivo1, 1);
        }

        else if (distancia2 <= radioVision && distancia2 < distancia1 && distancia2 > agent.stoppingDistance)
        {
            SetSpeedValue(1);
            mov = vectorMov2;
            agent.SetDestination(objetivo2.position);
            
        }
        else if (distancia2 <= agent.stoppingDistance && distancia2 < distancia1)
        {
            SetSpeedValue(0);
            if (puedeDisparar && !attacking)
                Atacar(objetivo2, 2);
        }

        //Animaciones
        if ((radioVision < distancia2 && radioVision < distancia1) || agent.stoppingDistance >= distancia1 || agent.stoppingDistance >= distancia2)
        {
            SetSpeedValue(0);            
        }

            if (mov.x > 0) SetDirectionValue(1);
            else if (mov.x < 0) SetDirectionValue(-1);
        

        #endregion    
        #region Morirse
        if (currentHealth <= 0)
        {
            animE.SetTrigger("Die");
            this.GetComponent<Collider>().enabled = false;
            this.enabled = false;
        }
        #endregion

    }

    //Damos tiempo para actualizar el estado de enemigo.
    private void LateUpdate()
    {
        if (damaged && currentHealth > 0)
        {
            //Animasao
            animE.SetTrigger("TakeDmg");

            agent.isStopped = true;
            Invoke("Damaged", 0.15f);
        }
    }

    /// <summary>
    /// Ataque del enemigo hacia el jugador, estado de ataque, 
    /// animación y corrutinas.
    /// </summary>
    private void Atacar(Transform target, int n)
    {
        attacking = true;      
        animE.SetTrigger("Attack");
        puedeDisparar = false;
        StartCoroutine(corAttack());
        StartCoroutine(corAnimAtacar(target, n));
        Invoke("corDisparo", 3);
    }

    /// <summary>
    /// Corrutinas de ataque, en funcion de que personaje le ataque.
    /// Y cooldown del mismo.
    /// </summary>
    /// <param name="t"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    private IEnumerator corAnimAtacar(Transform t, int n)
    {
        yield return new WaitForSeconds(0.15f);
        if ((t.position-transform.position).magnitude <= agent.stoppingDistance)
        {
            if (n == 1) t.GetComponent<Azul>().RecibirGolpe(this.transform);
            else if (n==2) t.GetComponent<Rosa>().RecibirGolpe(this.transform);
        }
    }
    private IEnumerator corAttack()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(1);
        attacking = false;
        agent.isStopped = false;
    }
    private void corDisparo()
    {
        puedeDisparar = true;
    }
    /// <summary>
    /// Control de velocidad y direccion
    /// </summary>
    /// <param name="speed"></param>
    private void SetSpeedValue(float speed)
    {
        if (speed > 0)
            animE.SetFloat("Speed", 1);

        if (speed <= 0)
            animE.SetFloat("Speed", 0);
    }
    private void SetDirectionValue(float dir)
    {
        if (dir > 0)
            animE.SetFloat("Direction", 1);
        if (dir < 0)
            animE.SetFloat("Direction", -1);
    }
    private void Damaged()
    {
        agent.isStopped = false;
        damaged = false;
    }
}
