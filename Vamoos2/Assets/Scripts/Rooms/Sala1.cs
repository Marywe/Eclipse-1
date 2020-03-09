using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sala1 : Salas

{
    public RoomController roomController;
    // Start is called before the first frame update
    void Start()
    {

        numPuertas = 2;
    }

    // Update is called once per frame
    void Update()
    {
        base.PuertasAbiertas(roomController.numEnems[0]);
        if (sePuedePasar)
        {
            for (int i = 0; i < numPuertas; i++)
            {
                roomController.doors[i].GetComponent<Animator>().SetBool("SePuedePasar", sePuedePasar);
            }
        }

    }

    //Instanciar enemigos en sus posiciones



}
