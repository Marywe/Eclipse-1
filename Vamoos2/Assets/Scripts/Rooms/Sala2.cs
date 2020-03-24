using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sala2 : Salas
{
    public RoomController roomController;
    [SerializeField]
    GameObject luces;
    public Transform[] puntosSpawn;
    public GameObject[] prefabEnems;

    public GameObject[] doors;

    // Start is called before the first frame update
    void Start()
    {
        numPuertas = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Controlador.instance.currentNumEnems == 0) salaClean = true;
        else salaClean = false;

        if (salaClean) sePuedePasar = true;
        else sePuedePasar = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            luces.SetActive(true);
            Controlador.instance.cam = Controlador.instance.ptoscamara[1];
            if (!salaCleanFirstTime) InstanciarEnemigos();
            Controlador.instance.dondeEstas = Controlador.DondeEstas.s2;

            if (other.gameObject.GetComponent<Azul>() != null)
            {

            }
        }
           

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) Controlador.instance.dondeEstas = Controlador.DondeEstas.s2;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            luces.SetActive(false);
    }

    void InstanciarEnemigos() //Instanciar enemigos en sus posiciones
    {
        Instantiate(prefabEnems[0], puntosSpawn[0].position, puntosSpawn[0].rotation);
        Instantiate(prefabEnems[1], puntosSpawn[1].position, puntosSpawn[1].rotation);
        Instantiate(prefabEnems[2], puntosSpawn[2].position, puntosSpawn[2].rotation);
        Controlador.instance.currentNumEnems = 3;
        salaCleanFirstTime = true;
    }
    
}
