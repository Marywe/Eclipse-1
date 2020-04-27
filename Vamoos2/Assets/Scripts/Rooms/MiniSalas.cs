using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSalas : Salas
{
    // Start is called before the first frame update
    void Start()
    {
        salaClean = true;
        salaCleanFirstTime = true;
        numPuertas = 1;
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

            if (this.gameObject.layer == 0)
            {
                Controlador.instance.ptoscamara[6].SetActive(true);
                Controlador.instance.dondeEstas = Controlador.DondeEstas.LabIzq;
            }
            else
            {
                Controlador.instance.ptoscamara[5].SetActive(true);
                Controlador.instance.dondeEstas = Controlador.DondeEstas.LabDer;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            luces.SetActive(false);
            if (this.gameObject.layer == 0)
            {
                Controlador.instance.ptoscamara[6].SetActive(false);
                Controlador.instance.dondeEstas = Controlador.DondeEstas.LabIzq;
            }
            else
            {
                Controlador.instance.ptoscamara[5].SetActive(false);
                Controlador.instance.dondeEstas = Controlador.DondeEstas.LabDer;
            }
        }
    }
}
