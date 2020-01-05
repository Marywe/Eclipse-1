using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scorpio : Enemigos
{
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.angularSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {

        #region Seguimiento
        //Con esto podemos modificar 

        float distancia1 = Vector3.Distance(objetivo1.position, transform.position);
        float distancia2 = Vector3.Distance(objetivo2.position, transform.position);

        if (distancia1 <= radioVision)
        {
            agent.SetDestination(objetivo1.position);
        }

        if (distancia2 <= radioVision)
        {
            agent.SetDestination(objetivo2.position);
        }
        #endregion    


    }
}
