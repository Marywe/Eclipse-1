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
        maxHealth = 2;
        currentHealth = maxHealth;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.angularSpeed = 0;
        animE = (Animator)gameObject.GetComponentInChildren(typeof(Animator));
    }

    // Update is called once per frame
    void Update()
    {
        //base.MirarObjetivo(cam);

        #region Seguimiento
        //Con esto podemos modificar 
        Vector3 vectorMov1 = new Vector3(objetivo1.position.x - this.transform.position.x, objetivo1.position.y - this.transform.position.y, objetivo1.position.z - this.transform.position.z);
        Vector3 vectorMov2 = new Vector3(objetivo2.position.x - this.transform.position.x, objetivo2.position.y - this.transform.position.y, objetivo2.position.z - this.transform.position.z);

        float distancia1 = Vector3.Distance(objetivo1.position, transform.position);
        float distancia2 = Vector3.Distance(objetivo2.position, transform.position);

        if (distancia1 <= radioVision && distancia1 < distancia2)
        {
            SetSpeedValue(1);
            mov = vectorMov1;
            agent.SetDestination((objetivo1.position) - Vector3.forward - Vector3.right * 2);

            if (distancia1 <= agent.stoppingDistance + 1)
            {
                SetSpeedValue(0);
                if(puedeDisparar)
                Shoot(objetivo1);
            }

        }

        if (distancia2 <= radioVision && distancia2 < distancia1)
        {
            SetSpeedValue(1);
            mov = vectorMov2;
            agent.SetDestination(objetivo2.position - Vector3.forward - Vector3.right * 2);
            if (distancia1 <= agent.stoppingDistance + 1)
            {
                SetSpeedValue(0);
                if(puedeDisparar)
                Shoot(objetivo2);
            }

        }

        //Animaciones
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
            this.enabled = false;
            this.GetComponent<Collider>().enabled = false;
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
            Invoke("Damaged", 0.2f);
        }
    }
    private void SetSpeedValue(float speed)
    {
        animE.SetFloat("Speed", speed);
    }
    private void SetDirectionValue(float dir)
    {
        animE.SetFloat("Direction", dir);
    }

    private void Damaged()
    {
        damaged = false;
        agent.isStopped = false;
    }

    public void Shoot(Transform target)
    {
        animE.SetTrigger("Attack");
        //instantiiate
        
        StartCoroutine(InstanciarDisparo(target));
        puedeDisparar = false;
        Invoke("corDisparo", 2);

    }

    private void corDisparo()
    {      
        puedeDisparar = true;
    }

    IEnumerator InstanciarDisparo(Transform target)
    {
        yield return new WaitForSeconds(0.5f);

        GameObject newDisparo = Instantiate(disparo, transform.position, Quaternion.identity);
        vD.x = target.transform.position.x - transform.position.x;
        vD.y = 0;
        vD.z = target.transform.position.z - transform.position.z;
        newDisparo.GetComponent<Rigidbody>().AddForce(vD.normalized * 60 * 20);
      
    }
}
