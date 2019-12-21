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

    [SerializeField]
    Transform objetivo1, objetivo2;
    NavMeshAgent agent;

    bool vulnerable = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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

        MirarObjetivo(cam);
        

    }

    void MirarObjetivo(Transform objetivo)
    {

        Vector3 direccion = (objetivo.position - transform.position).normalized;
        Quaternion rotacion = Quaternion.LookRotation(transform.position - cam.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime * 5f);

    }

    private void OnDrawGizmosSelected() //Para ver graficamente el radio de vision del enemigo en cuestión
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioVision);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Cadena")
        {
            vulnerable = true;
        }
    }

    private IEnumerator vulnerabilidad()
    {
        yield return new WaitForSeconds(5);
        vulnerable = false;
    }

}
