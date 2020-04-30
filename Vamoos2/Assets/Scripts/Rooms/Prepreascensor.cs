using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prepreascensor : Salas
{
    // Start is called before the first frame update
    void Start()
    {
        salaClean = true;
        salaCleanFirstTime = true;
        numPuertas = 2;
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
            Controlador.instance.ptoscamara[8].SetActive(true);
            Controlador.instance.dondeEstas = Controlador.DondeEstas.debajopreAscensor;
        }
    }

    //Al salir de la sala, apagar luces
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Controlador.instance.ptoscamara[8].SetActive(false);
            luces.SetActive(false);
        }
    }
}
