using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase herencia
/// Control de la segunda sala, donde apareceran los jugadores, controlando el numero de enemigos.
/// </summary>
public class Sala2 : Salas
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

    //Cuando el jugador se halle dentro de la sala, estado iluminacion activado, movimiento camara y movimiento personajes
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            luces.SetActive(true);
            Controlador.instance.ptoscamara[1].SetActive(true);
            if (!salaCleanFirstTime) InstanciarEnemigos();
            Controlador.instance.dondeEstas = Controlador.DondeEstas.s2;
        }
           
    }
    //Control de la iluminación de la sala, ademas de cambiar el estado de la posicion de sala del jugador

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Controlador.instance.ptoscamara[1].SetActive(false);
            luces.SetActive(false);
        }
            
    }

    //Instanciar enemigos en sus posiciones
    void InstanciarEnemigos() 
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject newMariposa = Instantiate(Controlador.instance.prefabMariposa, puntosSpawn[i].position, puntosSpawn[i].rotation);
        }
        Controlador.instance.currentNumEnems = 2;
        salaCleanFirstTime = true;
    }
    
}
