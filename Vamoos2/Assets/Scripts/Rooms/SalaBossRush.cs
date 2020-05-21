using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalaBossRush : Salas
{
    bool haSpawneado = false;
    public GameObject chip;
    // Start is called before the first frame update
    void Start()
    {
        numPuertas = 1;
    }

    // Update is called once per frame
    void Update()
    {
        base.ControladorPuertas(doors);

        if(salaCleanFirstTime && salaClean && !haSpawneado)
        {
            chip.SetActive(true);
            haSpawneado = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            luces.SetActive(true);
            Controlador.instance.ptoscamara[11].SetActive(true);
            if (!salaCleanFirstTime) InstanciarEnemigos();
            Controlador.instance.dondeEstas = Controlador.DondeEstas.sBossRush;
        }

    }
    //Control de la iluminación de la sala, ademas de cambiar el estado de la posicion de sala del jugador

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Controlador.instance.ptoscamara[11].SetActive(false);
            luces.SetActive(false);
        }

    }

    //Instanciar enemigos en sus posiciones
    void InstanciarEnemigos()
    {
       
        GameObject newRobot = Instantiate(Controlador.instance.prefabRobot, puntosSpawn[0].position, puntosSpawn[0].rotation);
        GameObject newMariposa = Instantiate(Controlador.instance.prefabEscorpion, puntosSpawn[1].position, puntosSpawn[1].rotation);
        GameObject newPrisma = Instantiate(Controlador.instance.prefabMariposa, puntosSpawn[2].position, puntosSpawn[2].rotation);
        GameObject newEscorpion = Instantiate(Controlador.instance.prefabPrisma, puntosSpawn[3].position, puntosSpawn[3].rotation);

        Controlador.instance.currentNumEnems = 4;
        salaCleanFirstTime = true;
    }
}
