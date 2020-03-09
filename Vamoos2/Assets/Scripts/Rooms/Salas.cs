using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salas : MonoBehaviour
{
    //public RoomController roomController;
    protected int numPuertas;
    protected bool sePuedePasar = false;
    // Start is called before the first frame update
    void Start()
    {
        //roomController = gameObject.GetComponent<RoomController>();
        //if (roomController != null) Debug.Log("Joder");
    }

    protected void PuertasAbiertas(int nSala)
    {
        if (nSala == 0)
        {
                     
                sePuedePasar = true;
            
        }
    }
}
