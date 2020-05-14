using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esta clase como indica su nombre es la encargada de modificar los estados de los personajes en funcion a unos objetos determinados.
public class Modificador : MonoBehaviour
{
    private string tagMod;
    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        tagMod = gameObject.tag;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(initialPosition.x, transform.position.y, initialPosition.z);
    }

    /// <summary>
    /// Establecer el modificador a aplicar en funcion a la colision que se detecte.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (tagMod)
            {
                case "Vida":
                    ModificarVida(other.gameObject);
                    break;
                case "Recarga":
                    ModificarRecarga(other.gameObject);
                    break;
                case "Velocidad":
                    ModificarVel(other.gameObject);
                    break;
                case "Ataque":
                    ModificarDano(other.gameObject);
                    break;
                case "Untagged":
                    if(other.GetComponent<Jugador>().line_sc.colisiones>0)
                        --other.GetComponent<Jugador>().line_sc.colisiones;

                    if (other.GetComponent<Jugador>().line_sc.colisiones > 0)
                        --other.GetComponent<Jugador>().line_sc.colisiones;
                    break;
            }

            Destroy(gameObject);
        }

    }

    /// <summary>
    /// Las funciones encargadas de acceder al componente correspondiente y modificar su valor, por ejemplo si el jugador consigue un modificador para aumentar el daño.
    /// </summary>
    /// <param name="other"></param>
    void ModificarDano(GameObject other)
    {
        other.GetComponent<Jugador>().dano += 0.75f;
    }
    void ModificarVel(GameObject other)
    {
        other.GetComponent<Jugador>().speed += 1;
    }
    void ModificarRecarga(GameObject other)
    {
        other.GetComponent<Jugador>().cdSkill -= 1f;
    }
    void ModificarVida(GameObject other)
    {
        other.GetComponent<Jugador>().line_sc.ModificarVidaMax();
    }
}