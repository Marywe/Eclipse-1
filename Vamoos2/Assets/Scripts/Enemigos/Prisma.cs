using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Prisma : Enemigos
{
    Animator animE;

    [SerializeField]
    GameObject disparo;
    Vector3 vD;


    // Start is called before the first frame update
    void Start()
    {
        shield = transform.GetChild(1).gameObject;
        maxHealth = 5;
        currentHealth = maxHealth;
        agent = gameObject.GetComponent<NavMeshAgent>();
        animE = (Animator)gameObject.GetComponentInChildren(typeof(Animator));
    }

    // Update is called once per frame
    void Update()
    {
        //base.MirarObjetivo(cam);  

        #region Seguimiento
        //Con esto podemos modificar 
        Vector3 vectorMov1 = new Vector3(objetivo1.position.x - this.transform.position.x, objetivo1.position.y - this.transform.position.y, objetivo1.position.z - this.transform.position.z);   
        float distancia1 = Vector3.Distance(objetivo1.position, transform.position);
        Vector3 vectorMov2 = new Vector3(objetivo2.position.x - this.transform.position.x, objetivo2.position.y - this.transform.position.y, objetivo2.position.z - this.transform.position.z);
        float distancia2 = Vector3.Distance(objetivo2.position, transform.position);

        #region PJ1
        if (distancia1 <= radioVision && distancia1 < distancia2 && distancia1 > agent.stoppingDistance)
        {
            SetSpeedValue(1);
            mov = vectorMov1;
            agent.SetDestination((objetivo1.position) - Vector3.forward - Vector3.right * 2);
        }

        else if (distancia1 <= agent.stoppingDistance + 5 && distancia1 < distancia2)
        {
            SetSpeedValue(0);
            if (puedeDisparar)
                Shoot(objetivo1);
        }

        #endregion

        #region PJ2

        else if (distancia2 <= radioVision && distancia2 < distancia1 && distancia2 > agent.stoppingDistance)
        {
            SetSpeedValue(1);
            mov = vectorMov2;
            agent.SetDestination(objetivo2.position - Vector3.forward - Vector3.right * 2);
        }
        else if (distancia2 <= agent.stoppingDistance + 5 && distancia2 < distancia1)
        {
            SetSpeedValue(0);
            if (puedeDisparar)
                Shoot(objetivo2);


        }


        #endregion
        #endregion

        #region Animaciones
        if (radioVision < distancia2 && radioVision < distancia1)
        {
            SetSpeedValue(0);

        }
        else if (mov.x != 0)
        {
            if (mov.x > 0) SetDirectionValue(1);
            else if (mov.x < 0) SetDirectionValue(-1);
        }
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
    private void SetSpeedValue(float speed)
    {
        if(speed > 0)
            animE.SetFloat("Speed", 1);

        if (speed <= 0)
            animE.SetFloat("Speed", 0);
    }
    private void SetDirectionValue(float dir)
    {
        if(dir>0)
            animE.SetFloat("Direction", 1);
        if (dir < 0)
            animE.SetFloat("Direction", -1);

    }

    private void Damaged()
    {
        agent.isStopped = false;
        damaged = false;
    }

    public void Shoot(Transform target)
    {
        animE.SetTrigger("Attack");
        
        StartCoroutine(InstanciarDisparo(target));
        puedeDisparar = false;
        Invoke("corDisparo", 2);
    }

    private void corDisparo()
    {      
        puedeDisparar = true;
        SetSpeedValue(0);
    }

    IEnumerator InstanciarDisparo(Transform target)
    {
        yield return new WaitForSeconds(1f);
		if (!damaged)
		{
			GameObject newDisparo = Instantiate(disparo, transform.position, transform.rotation);
            Vector3 look = new Vector3(target.position.x - transform.position.x -90, target.position.y - transform.position.y, target.position.z - transform.position.z);
            newDisparo.transform.rotation = Quaternion.LookRotation(look);
			vD.x = target.transform.position.x - transform.position.x;
			vD.y = 0;
			vD.z = target.transform.position.z - transform.position.z;
			newDisparo.GetComponent<Rigidbody>().AddForce(vD.normalized * 60 * 20);
		}
      
    }
}
