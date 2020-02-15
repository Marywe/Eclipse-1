using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scorpio : Enemigos
{
    Animator animE;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 5;
        currentHealth = maxHealth;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.angularSpeed = 0;
        animE = (Animator)gameObject.GetComponentInChildren(typeof(Animator));
    }

    // Update is called once per frame
    void Update()
    {
        base.MirarObjetivo(cam);

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

        //Animaciones
        if (mov.x == 0 && mov.z==0)
        {
            SetSpeedValue(0);
            
        }
        else if (mov.x != 0)
        {
            SetSpeedValue(1);
            if (mov.x > 0) SetDirectionValue(1);
            else if (mov.x < 0) SetDirectionValue(-1);
        }

        #endregion    


    }
    private void SetSpeedValue(float speed)
    {
        animE.SetFloat("Speed", speed);
    }
    private void SetDirectionValue(float dir)
    {
        animE.SetFloat("Direction", dir);
    }
}
