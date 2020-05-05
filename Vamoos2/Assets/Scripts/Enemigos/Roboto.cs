using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Otro tipo de enemigo, control general gracias al script Enemigos(herencia).
public class Roboto : Enemigos
{
	public float distanciaAtaque;
	private bool attacking;
	public Vector3 attackSz;
	public Vector3 attackPos;

    AudioManager audioManager;
    public AudioSource audioSource;

	// Start is called before the first frame update
	void Start()
	{
		objetivo1 = Controlador.instance.objetivo1;
		objetivo2 = Controlador.instance.objetivo2;
		shield = transform.GetChild(1).gameObject;
		attacking = false;
		maxHealth = 6;
		currentHealth = maxHealth;
		agent = gameObject.GetComponent<NavMeshAgent>();
		animE = (Animator)gameObject.GetComponentInChildren(typeof(Animator));
		sr = gameObject.GetComponentInChildren<SpriteRenderer>();

		//audioSource = audioManager.GetComponent<AudioSource>();
	
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
        //ReRobot(); 

		if (!sr.flipX)
		{
			attackPos = transform.position + (Vector3.right);
			
		}
		else
		{
			attackPos = (transform.position + (-Vector3.right));

		}



        //base.MirarObjetivo(cam);
        #region Seguimiento
        //Con esto podemos modificar 
        Vector3 vectorMov1 = new Vector3(objetivo1.position.x - this.transform.position.x, objetivo1.position.y - this.transform.position.y, objetivo1.position.z - this.transform.position.z);
		Vector3 vectorMov2 = new Vector3(objetivo2.position.x - this.transform.position.x, objetivo2.position.y - this.transform.position.y, objetivo2.position.z - this.transform.position.z); ;

		float distancia1 = Vector3.Distance(objetivo1.position, transform.position);
		float distancia2 = Vector3.Distance(objetivo2.position, transform.position);

		if (distancia1 <= radioVision && distancia1 < distancia2)
		{
			mov = vectorMov1;
			agent.SetDestination(objetivo1.position);

		}

		else if (distancia2 <= radioVision && distancia2 < distancia1)
		{
			mov = vectorMov2;
			agent.SetDestination(objetivo2.position);

		}
        else
        {
            mov = Vector3.zero;
        }
		#endregion
		#region Anims

		if(mov==Vector3.zero) animE.SetBool("Moving", false);

		if ((radioVision < distancia1 && radioVision < distancia2) || agent.stoppingDistance >= distancia1 || agent.stoppingDistance >= distancia2)
		{
			animE.SetBool("Moving", false);

		}

		else if ((radioVision > distancia1 || radioVision > distancia2) && mov.x > 0)
		{
			animE.SetBool("Moving", true);
			sr.flipX = false;
		}
		else if ((radioVision > distancia1 || radioVision > distancia2) && mov.x < 0)
		{
			animE.SetBool("Moving", true);
			sr.flipX = true; //izq
		}
		#endregion
		#region Ataque
		if (distancia1 <= distanciaAtaque && !attacking && puedeDisparar)
		{
			Atacar();
		}
		else if (distancia2 <= distanciaAtaque && !attacking && puedeDisparar)
		{
			Atacar();
		}
		#endregion
		#region Morirse
		if (currentHealth <= 0)
		{
			animE.SetTrigger("Die");
			this.GetComponent<Collider>().enabled = false;
			this.enabled = false;

            ReRobotDeath();
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
			Invoke("Damaged", 0.5f);

            //ReRobotHit();
        }
    }

    /// <summary>
    /// Ataque del enemigo hacia el jugador, estado de ataque, animación y corrutina de cooldown.
    /// </summary>
	void Atacar()
	{
		attacking = true;
		animE.SetTrigger("Atacar");
		StartCoroutine(corAttack());
        ReRobotAtaque();
    }

    /// <summary>
    /// Corrutina de Ataque, cooldown y cambio de estado de ataque.
    /// </summary>
    /// <returns></returns>
	private IEnumerator corAttack()
	{
		agent.isStopped = true;
		animE.SetBool("Moving", false);
		yield return new WaitForSeconds(1.6f);
		Collider[] hit = Physics.OverlapBox(attackPos, attackSz, transform.rotation);
		
		foreach(Collider colPJ in hit)
		{
			if (colPJ.gameObject.GetComponent<Azul>() != null) colPJ.gameObject.GetComponent<Azul>().RecibirGolpe(transform);
			else if(colPJ.gameObject.GetComponent<Rosa>() != null) colPJ.gameObject.GetComponent<Rosa>().RecibirGolpe(transform);
		}
		agent.isStopped = false;
		yield return new WaitForSeconds(3);
		attacking = false;
		

	}
    
	private void Damaged()
	{
		agent.isStopped = false;
		damaged = false;
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(attackPos, attackSz);
	}


    #region Robot Musica
    public void ReRobot()
    {
        audioManager.FuenteSFX.clip = audioManager.MovimientoR;
        audioManager.FuenteSFX.loop = true;
        audioManager.FuenteSFX.Play();
    }

    public void ReRobotHit()
    {
        audioManager.FuenteSFX.clip = audioManager.HitR;
        audioManager.FuenteSFX.loop = true;
        audioManager.FuenteSFX.Play();
    }

    public void ReRobotDeath()
    {
        audioManager.FuenteSFX.clip = audioManager.MuerteR;
        audioManager.FuenteSFX.loop = false;
        audioManager.FuenteSFX.Play();
    }

    public void ReRobotAtaque()
    {
        audioManager.FuenteSFX.clip = audioManager.AtaqueR;
        audioManager.FuenteSFX.loop = true;
        audioManager.FuenteSFX.Play();
    }

    #endregion



}