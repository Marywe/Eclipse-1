using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntesalaBoss : Salas
{

    // Start is called before the first frame update
    void Start()
    {
        
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
            Controlador.instance.cam = Controlador.instance.ptoscamara[1];
            if (!salaCleanFirstTime) InstanciarEnemigos();
            Controlador.instance.dondeEstas = Controlador.DondeEstas.sAnteBoss;
        }

    }
    //Control de la iluminación de la sala, ademas de cambiar el estado de la posicion de sala del jugador
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) Controlador.instance.dondeEstas = Controlador.DondeEstas.sAnteBoss;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            luces.SetActive(false);
    }

    //Instanciar enemigos en sus posiciones
    void InstanciarEnemigos()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject newMariposa = Instantiate(Controlador.instance.prefabMariposa, puntosSpawn[i].position, puntosSpawn[i].rotation);
        }
        Controlador.instance.currentNumEnems = 3;
        salaCleanFirstTime = true;
    }
}
