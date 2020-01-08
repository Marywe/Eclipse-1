using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigos : MonoBehaviour
{
    [SerializeField]
    protected float radioVision;

    [SerializeField]
    protected Transform cam;

    
    [SerializeField]
    protected Transform objetivo1, objetivo2;

    bool vulnerable = false;
    protected NavMeshAgent agent;


    protected void MirarObjetivo(Transform objetivo)
    {
        Vector3 direccion = (objetivo.position - transform.position).normalized;
        Quaternion rotacion = Quaternion.LookRotation(transform.position - cam.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime * 10f);
    }

    private void OnDrawGizmosSelected() //Para ver graficamente el radio de vision del enemigo en cuestión
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioVision);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Cadena" && vulnerable==false)
        {
            vulnerable = true;
            StartCoroutine(CorVulnerabilidad());
            Debug.Log("Vulner");
        }
    }

    private IEnumerator CorVulnerabilidad()
    {
        yield return new WaitForSeconds(5);
        vulnerable = false;
    }

}
