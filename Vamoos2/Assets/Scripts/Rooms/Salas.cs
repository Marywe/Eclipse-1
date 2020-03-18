using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salas : MonoBehaviour
{
    //public RoomController roomController;
    protected int numPuertas;
    protected bool sePuedePasar = false;
    protected bool salaClean=true;
    protected bool salaCleanFirstTime=false;

    protected void PuertasAbiertas(int nEnems)
    {
        if (nEnems == 0)
        {       
            sePuedePasar = true;
        }
    }
}
