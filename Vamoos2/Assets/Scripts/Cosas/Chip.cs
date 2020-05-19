using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script que controla las características de los chips para abrir la puerta final
/// </summary>
public class Chip : MonoBehaviour
{

    AudioManager audioManager;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        audioManager = Controlador.instance.audioManager;
    }
    //Añadir Chips cuando la cosilision sea realizada por un personaje(jugador)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioManager.PickUps(audioSource, "key");
            Controlador.instance.AddChips();
            Destroy(transform.GetChild(0).gameObject);
            gameObject.GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 0.6f);
        }
    }
}
