using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreAscensor : Salas
{
    // Start is called before the first frame update
    void Start()
    {
        numPuertas = 4;
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
            Controlador.instance.ptoscamara[7].SetActive(true);
            if (!salaCleanFirstTime) InstanciarEnemigos();
            Controlador.instance.dondeEstas = Controlador.DondeEstas.preAscensor;
        }

    }
    //Control de la iluminación de la sala, ademas de cambiar el estado de la posicion de sala del jugador

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Controlador.instance.ptoscamara[7].SetActive(false);
            luces.SetActive(false);
        }

    }

    //Instanciar enemigos en sus posiciones
    void InstanciarEnemigos()
    {
        GameObject newPrisma = Instantiate(Controlador.instance.prefabPrisma, puntosSpawn[0].position, puntosSpawn[0].rotation);  
        Controlador.instance.currentNumEnems = 1;
        salaCleanFirstTime = true;
    }
}
