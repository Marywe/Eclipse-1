using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntesalaBoss : Salas
{

    // Start is called before the first frame update
    void Start()
    {
        salaClean = true;
        salaCleanFirstTime = true;
        numPuertas = 3;
    }

    // Update is called once per frame
    void Update()
    {
        base.ControladorPuertas(doors);  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            luces.SetActive(true);
            Controlador.instance.ptoscamara[2].SetActive(true);
            Controlador.instance.dondeEstas = Controlador.DondeEstas.sAnteBoss;
        }

    }
    //Control de la iluminación de la sala, ademas de cambiar el estado de la posicion de sala del jugador

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Controlador.instance.ptoscamara[2].SetActive(false);
            luces.SetActive(false);
        }
            
    }

   
}
