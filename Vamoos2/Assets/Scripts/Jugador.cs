using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [SerializeField]
    protected float speed = 6.0f;
    [SerializeField]
    protected float gravity = 20.0f;
    [SerializeField]
    Linea line_sc;
    protected bool vulnerable = true;

    [SerializeField]
    private Transform cam = null;

    CharacterController c;

    private void Start()
    {
        c = (CharacterController)gameObject.GetComponent(typeof(CharacterController));
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemigos" && vulnerable==true)
        {
            
            line_sc.RecibirDano();
            Debug.Log("Colisión con psj");            
            vulnerable = false;
            Knockback(other);
            StartCoroutine(CorVulnerabilidad());
        }
    }

    protected IEnumerator CorVulnerabilidad()
    {        
        yield return new WaitForSeconds(5);
        vulnerable = true;
    }

    protected void Rotar()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.position);
    }

    private void Knockback(Collider other)
    {
        Vector3 dir = ((this.transform.position - other.transform.position)*2*Time.deltaTime);
        Debug.Log("knockback");
        c.Move(dir);
    
    }
}
