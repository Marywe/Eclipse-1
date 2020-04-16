using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controlador del estado del estado de las puertas en general.
public class Salas : MonoBehaviour
{
    //public RoomController roomController;
    protected int numPuertas;
    protected bool sePuedePasar = false;
    protected bool salaClean=true;
    protected bool salaCleanFirstTime=false;

    [SerializeField]
    protected GameObject luces;
    public Transform[] puntosSpawn;
    public GameObject[] doors;

    protected void ControladorPuertas(GameObject[] doors)
    {
        //comprobacion estado sala
        if (Controlador.instance.currentNumEnems == 0) salaClean = true;
        else salaClean = false;

        if (salaClean) sePuedePasar = true;
        else sePuedePasar = false;

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

}
