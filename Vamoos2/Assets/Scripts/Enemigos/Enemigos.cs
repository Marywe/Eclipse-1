using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Clase para el comportamiento y estadisticas generales de los enemigos.
public class Enemigos : MonoBehaviour
{
    protected bool puedeDisparar = true;

    [SerializeField]
    protected float maxHealth;
    protected float currentHealth;
    protected Vector3 mov;
    protected int armor = 5;
    protected Animator animE;

    protected GameObject shield;

    [SerializeField]
    protected float radioVision;

    //[SerializeField]
    //protected Transform cam;

    protected bool damaged = false;

    protected Transform objetivo1, objetivo2;

    bool vulnerable = false;
    protected NavMeshAgent agent;

    /*protected void MirarObjetivo(Transform objetivo)
    {
        Vector3 direccion = (objetivo.position - transform.position).normalized;
        Quaternion rotacion = Quaternion.LookRotation(transform.position - cam.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime * 10f);
    }*/

    //Para ver graficamente el radio de vision del enemigo en cuestión
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioVision);
    }

    //Si la vulnerabilidad de los personajes(su union, la cadena) puede recibir daño.
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
    //corrutina para cambiar entre vulnerabilidad la cadena, teniendo un tiempo en concreto
    private IEnumerator CorVulnerabilidad()
    {
        
        shield.SetActive(false);
        yield return new WaitForSeconds(5);
        vulnerable = false;

        if(shield!=null)shield.SetActive(true);

    }
    
    //Implementacion del daño y como lo recibe
    public void TakeDamage(float dmg)
    {
        damaged = true;

        if(vulnerable==true || shield==null)
        currentHealth -= dmg;
        
        else if (vulnerable == false && shield!=null)
        {
            armor -= (int)dmg;
        }
        Debug.Log("Current life: " + currentHealth + " Armor: " + armor);
        if (armor <= 0) Destroy(shield);

        if (currentHealth <= 0) Morirse();
    }

    /// <summary>
    /// Cuando se cumpla las condiciones para su muerte, se resetea el numero de enemigos, se destruye su escudo si lo tuviese
    /// y se procede a destruir.
    /// </summary>
    private void Morirse()
    {
        --Controlador.instance.currentNumEnems;
        if (shield != null) Destroy(shield);        
        Destroy(this.gameObject, 4f);
    }
}
