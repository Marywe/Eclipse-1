using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigos : MonoBehaviour
{
    [SerializeField]
    public float radioVision;

    [SerializeField]
    private Transform cam;

    Transform objetivo1, objetivo2;
    NavMeshAgent agent;

    //public Camera cam;
    //public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        objetivo1 = PosicionJugador.instance.jugador1.transform;
        objetivo2 = PosicionJugador.instance.jugador2.transform;


        agent = GetComponent<NavMeshAgent>();
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

    void MirarObjetivo(Transform objetivo)
    {

        Vector3 direccion = (objetivo.position - transform.position).normalized;
        Quaternion rotacion = Quaternion.LookRotation(transform.position - cam.position);

    }

    private void OnDrawGizmosSelected() //Para ver graficamente el radio de vision del enemigo en cuestión
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioVision);

    }

    
}
