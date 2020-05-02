using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalaRobot : Salas
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
            Controlador.instance.ptoscamara[10].SetActive(true);
            if (!salaCleanFirstTime) InstanciarEnemigos();
            Controlador.instance.dondeEstas = Controlador.DondeEstas.sArriba;
        }

    }
    //Control de la iluminación de la sala, ademas de cambiar el estado de la posicion de sala del jugador

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Controlador.instance.ptoscamara[10].SetActive(false);
            luces.SetActive(false);
        }

    }

    //Instanciar enemigos en sus posiciones
    void InstanciarEnemigos()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject newRobot = Instantiate(Controlador.instance.prefabRobot, puntosSpawn[i].position, puntosSpawn[i].rotation);
        }
        Controlador.instance.currentNumEnems = 2;
        salaCleanFirstTime = true;
    }
}
