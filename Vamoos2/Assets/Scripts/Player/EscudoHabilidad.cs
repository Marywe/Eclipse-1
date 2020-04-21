using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase para modificar las caracteristicas del jugador cuando se realiza la habilidad del escudo.
public class EscudoHabilidad : MonoBehaviour
{
    private Rosa r;
    private Azul a;

    [Header("Stats a sumar")]
    public float speed;
    public float dano;

    private void OnTriggerEnter(Collider other)
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
    }
    


}
