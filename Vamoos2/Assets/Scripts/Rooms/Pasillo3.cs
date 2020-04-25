using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pasillo3 : Salas
{
    // Start is called before the first frame update
    void Start()
    {
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
            if (!salaCleanFirstTime) InstanciarEnemigos();
            Controlador.instance.ptoscamara[3].SetActive(true);
            Controlador.instance.dondeEstas = Controlador.DondeEstas.pasillo;
        }
    }

    //Al salir de la sala, apagar luces
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Controlador.instance.ptoscamara[3].SetActive(false);
            luces.SetActive(false);
        }
    }

    void InstanciarEnemigos()
    {
       
        GameObject newScorpio = Instantiate(Controlador.instance.prefabEscorpion, puntosSpawn[0].position, puntosSpawn[0].rotation);
        
        Controlador.instance.currentNumEnems = 1;
        salaCleanFirstTime = true;
    }
}
