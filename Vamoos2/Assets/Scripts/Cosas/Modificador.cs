using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esta clase como indica su nombre es la encargada de modificar los estados de los personajes en funcion a unos objetos determinados.
public class Modificador : MonoBehaviour
{
    private string tagMod;
    Vector3 initialPosition;
    AudioSource audioSource;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        audioManager = Controlador.instance.audioManager;

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
                    audioManager.PickUps(audioSource, "pick");
                    ModificarVida(other.gameObject);
                    break;
                case "Recarga":
                    audioManager.PickUps(audioSource, "pick");
                    ModificarRecarga(other.gameObject);
                    break;
                case "Velocidad":
                    audioManager.PickUps(audioSource, "pick");
                    ModificarVel(other.gameObject);
                    break;
                case "Ataque":
                    audioManager.PickUps(audioSource, "pick");
                    ModificarDano(other.gameObject);
                    break;
                case "Untagged":
                    audioManager.PickUps(audioSource, "healthpack");

                    if(other.GetComponent<Jugador>().line_sc.colisiones>0)
                        --other.GetComponent<Jugador>().line_sc.colisiones;

                    if (other.GetComponent<Jugador>().line_sc.colisiones > 0)
                        --other.GetComponent<Jugador>().line_sc.colisiones;
                    break;
            }
            transform.GetChild(0).gameObject.SetActive(false);
            gameObject.GetComponent<Collider>().enabled = false;

            Destroy(gameObject, 1f);
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

        if (other.GetComponent<Rosa>() != null) other.GetComponent<Rosa>().speed2 += 1;
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