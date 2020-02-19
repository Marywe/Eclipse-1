using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigos : MonoBehaviour
{
    protected int maxHealth;
    protected int currentHealth;
    protected Vector3 mov;

    protected GameObject shield;

    [SerializeField]
    protected float radioVision;

    [SerializeField]
    protected Transform cam;

    
    [SerializeField]
    protected Transform objetivo1, objetivo2;

    bool vulnerable = false;
    protected NavMeshAgent agent;

    private void Start()
    {
        
    }
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
            //Sprites, corrutina, health-armor
            vulnerable = true;
            StartCoroutine(CorVulnerabilidad());
            Debug.Log("Vulner");

        }
    }

    private IEnumerator CorVulnerabilidad()
    {
     
        shield.SetActive(false);
        yield return new WaitForSeconds(5);
        vulnerable = false;

        shield.SetActive(true);

    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0) Morirse();
    }

    private void Morirse()
    {
        this.enabled = false;
        this.GetComponent<Collider>().enabled = false;

        //Animación muerte

        Destroy(this.gameObject, 2);
    }
}
