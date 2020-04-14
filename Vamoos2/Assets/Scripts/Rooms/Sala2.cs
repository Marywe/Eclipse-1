using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase herencia
/// Control de la segunda sala, donde apareceran los jugadores, controlando el numero de enemigos.
/// </summary>
public class Sala2 : Salas
{
    [SerializeField]
    GameObject luces;
    public Transform[] puntosSpawn;

    public GameObject[] doors;

    // Start is called before the first frame update
    void Start()
    {
        numPuertas = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //comprobacion estado sala
        if (Controlador.instance.currentNumEnems == 0) salaClean = true;
        else salaClean = false;

        if (salaClean) sePuedePasar = true;
        else sePuedePasar = false;

        //controlar todas las puertas para permitir al jugador que pueda pasar o no
        base.PuertasAbiertas(Controlador.instance.currentNumEnems);
        if (sePuedePasar && salaClean)
        {
            for (int i = 0; i < numPuertas; i++)
            {
                doors[i].GetComponent<Animator>().SetBool("SePuedePasar", true);
            }
        }

        else
        {
            for (int i = 0; i < numPuertas; i++)
            {
                doors[i].GetComponent<Animator>().SetBool("SePuedePasar", false);
            }
        }

    }

    //Cuando el jugador se halle dentro de la sala, estado iluminacion activado, movimiento camara y movimiento personajes
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            luces.SetActive(true);
            Controlador.instance.cam = Controlador.instance.ptoscamara[1];
            if (!salaCleanFirstTime) InstanciarEnemigos();
            Controlador.instance.dondeEstas = Controlador.DondeEstas.s2;
        }
           
    }
    //Control de la iluminación de la sala, ademas de cambiar el estado de la posicion de sala del jugador
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) Controlador.instance.dondeEstas = Controlador.DondeEstas.s2;
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
