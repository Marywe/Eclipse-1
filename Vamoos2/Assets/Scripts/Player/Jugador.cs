using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [SerializeField]
    protected float speed = 6.0f;
    [SerializeField]
    protected float gravity = 10;
    [SerializeField]
    Linea line_sc;
    protected bool vulnerable = true;
    [SerializeField]
    protected int distKnockback = 5000;
    [SerializeField]
    float tiempoVul = 2f;
    protected GameObject sprites;


    [SerializeField]
    protected Transform cam = null;

    CharacterController c;
    
    private void Start()
    {

        DontDestroyOnLoad(this);
        c = (CharacterController)gameObject.GetComponent(typeof(CharacterController));
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemigos" && vulnerable==true)
        {       
            //Animación recibir daño

            line_sc.RecibirDano();
            Debug.Log("Colisión con psj");
            vulnerable = false;
            StartCoroutine(CorVulnerabilidad());
            
        }
    }

    protected IEnumerator CorVulnerabilidad()
    {       
        yield return new WaitForSeconds(tiempoVul);
        vulnerable = true;
    }

    protected virtual void Rotar()
    {
        sprites.transform.rotation = Quaternion.LookRotation(transform.position - cam.position);
    }

}
