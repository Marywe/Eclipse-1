using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script que controla las características de los chips para abrir la puerta final
/// </summary>
public class Chip : MonoBehaviour
{
    //Añadir Chips cuando la cosilision sea realizada por un personaje(jugador)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Controlador.instance.AddChips();
            Destroy(gameObject);
        }
    }
}
