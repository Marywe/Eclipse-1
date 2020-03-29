using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script que controla las características de los chips para abrir la puerta final
/// </summary>
public class Chip : MonoBehaviour
{  
    Transform tr;
    public Transform cam;
    private bool PuertaGrandeAbierta = false;
    private void Start()
    {
        tr = gameObject.transform.GetChild(0);
    }
    //Añadir Chips cuando la cosilision sea realizada por un personaje(jugador)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Controlador.instance.AddChips();
            Destroy(gameObject);
        }

    }

    //Cuando se alcance el numero especifico de chips, modificara la variablee de la puerta para que el jugador pueda pasar
    private void Update()
    {
        if (Controlador.instance.chips == 3)
        {
            PuertaGrandeAbierta = true;
        }
    }
}
