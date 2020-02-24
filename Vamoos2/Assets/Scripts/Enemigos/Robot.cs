using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : Enemigos
{
	Animator animE;
	public float distanciaAtaque;
	SpriteRenderer sr;
	private bool attacking;

	// Start is called before the first frame update
	void Start()
	{
		shield = transform.GetChild(1).gameObject;
		attacking = false;
		maxHealth = 6;
		currentHealth = maxHealth;
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.angularSpeed = 0;
		animE = (Animator)gameObject.GetComponentInChildren(typeof(Animator));
		sr = gameObject.GetComponentInChildren<SpriteRenderer>();
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

		if (distancia1 <= radioVision && distancia1 < distancia2)
		{
			mov = vectorMov1;
			agent.SetDestination(objetivo1.position);

		}

		if (distancia2 <= radioVision && distancia2 < distancia1)
		{
			mov = vectorMov2;
			agent.SetDestination(objetivo2.position);

		}
		#endregion
		#region Anims
		if (mov.z == 0)
		{
			animE.SetBool("Moving", false);
			
		}

		else if (mov.x > 0)
		{
			animE.SetBool("Moving", true);
			sr.flipX = false;
		}
		else if (mov.x < 0)
		{
			animE.SetBool("Moving", true);
			sr.flipX = true;
		}



		#endregion
		#region Ataque
		if (distancia1 <= distanciaAtaque && attacking == false)
		{
			Atacar();
		}
		else if (distancia2 <= distanciaAtaque && attacking == false)
		{
			Atacar();
		}
        #endregion

    }

    void Atacar()
	{		
		attacking = true;
		animE.SetTrigger("Atacar");
		StartCoroutine(corAttack());
	}

	private IEnumerator corAttack()
	{
		agent.isStopped = true;
		yield return new WaitForSeconds(3);
		attacking = false;
		agent.isStopped = false;

	}
}
