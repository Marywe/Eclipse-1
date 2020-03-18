using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sala1 : Salas

{
    public RoomController roomController;
    [SerializeField]
    GameObject luces;

    public GameObject[] doors;
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
        base.PuertasAbiertas(Controlador.instance.currentNumEnems);
        if (sePuedePasar && salaClean)
        {
            for (int i = 0; i < numPuertas; i++)
            {
                doors[i].GetComponent<Animator>().SetBool("SePuedePasar", sePuedePasar);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            luces.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            luces.SetActive(false);
    }

    //Instanciar enemigos en sus posiciones



}
