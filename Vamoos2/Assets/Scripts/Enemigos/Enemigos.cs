using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigos : MonoBehaviour
{
    protected bool puedeDisparar = true;

    [SerializeField]
    protected int maxHealth;
    protected int currentHealth;
    protected Vector3 mov;
    protected int armor = 5;
    private Animator anim;

    protected GameObject shield;

    [SerializeField]
    protected float radioVision;

    //[SerializeField]
    //protected Transform cam;

    protected bool damaged = false;
    [SerializeField]
    protected Transform objetivo1, objetivo2;

    bool vulnerable = false;
    protected NavMeshAgent agent;


    /*protected void MirarObjetivo(Transform objetivo)
    {
        Vector3 direccion = (objetivo.position - transform.position).normalized;
        Quaternion rotacion = Quaternion.LookRotation(transform.position - cam.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime * 10f);
    }*/

    private void OnDrawGizmosSelected() //Para ver graficamente el radio de vision del enemigo en cuestión
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioVision);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Cadena" && vulnerable==false && shield!=null)
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
        damaged = true;

        if(vulnerable==true || shield==null)
        currentHealth -= dmg;
        
        else if (vulnerable == false && shield!=null)
        {
            armor -= dmg;
        }
        Debug.Log("Current life: " + currentHealth + " Armor: " + armor);
        if (armor <= 0) Destroy(shield);

        if (currentHealth <= 0) Morirse();
    }

    private void Morirse()
    {
        if (shield != null) Destroy(shield);        
        Destroy(this.gameObject, 4f);
    }
}
