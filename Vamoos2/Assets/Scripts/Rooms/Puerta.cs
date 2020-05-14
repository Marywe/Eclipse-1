using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script encargado del comportamiento animado de las puertas de las salas del juego.
/// Haciendo uso de las colisiones, detectar si el jugador se encuentra cerca de una puerta,
/// activando o no la animacion de la misma.
/// </summary>
public class Puerta : MonoBehaviour
{
    Animator anim;
    bool cerca = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        anim.SetBool("Cerca", cerca);

    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cerca = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cerca = false;
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (gameObject.tag)
            {
                case "NORTE":
                    Controlador.instance.orientacion = Controlador.OrientacionPuerta.NORTE;
                    break;
                case "SUR":
                    Controlador.instance.orientacion = Controlador.OrientacionPuerta.SUR;
                    break;
                case "ESTE":
                    Controlador.instance.orientacion = Controlador.OrientacionPuerta.ESTE;
                    break;
                case "OESTE":
                    Controlador.instance.orientacion = Controlador.OrientacionPuerta.OESTE;
                    break;

            }
        }
    }
}
