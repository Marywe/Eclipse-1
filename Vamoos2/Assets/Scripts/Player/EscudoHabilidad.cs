using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase para modificar las caracteristicas del jugador cuando se realiza la habilidad del escudo.
public class EscudoHabilidad : MonoBehaviour
{
    public Rosa r;
    public Azul a;

    [Header("Stats a sumar")]
    public float speed;
    public float dano;

    private Transform refSueloA, refSueloB;

    public static bool temporal;
    private void Awake()
    {
       

        refSueloA = gameObject.transform.GetChild(2);
        refSueloB = gameObject.transform.GetChild(3);

        temporal = false;
        
    }

    private void Start()
    {
    }
    private void Update()
    {
        refSueloA.position = new Vector3(r.transform.position.x, r.transform.position.y-0.67f, r.transform.position.z - 0.27F);
        refSueloB.position = new Vector3(a.transform.position.x, a.transform.position.y - 0.67f, a.transform.position.z - 0.27F);

        if (gameObject.activeSelf && !temporal)
        {
            Enabled();
        }
       

    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Azul>() != null)
            {
                if (a == null) a = other.gameObject.GetComponent<Azul>();
                a.speed += speed;
                a.dano += dano;
                a.onShield = true;
            }
            else if (other.gameObject.GetComponent<Rosa>() != null)
            {
                if (r == null) r = other.gameObject.GetComponent<Rosa>();
                r.speed += speed;
                r.dano += dano;
                r.onShield = true;
            }
        }
    }*/

    private void Enabled()
    {
        r.speed += speed;
        r.dano += dano;

        a.speed += speed;
        a.dano += dano;
        temporal = true;
    }

    public void Disabled()
    {

        r.speed -= speed;
        r.dano -= dano;

        a.speed -= speed;
        a.dano -= dano;

        temporal = false;
        
    }



}
