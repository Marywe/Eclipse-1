using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionJugador : MonoBehaviour
{
    public static PosicionJugador instance;


    private void Awake()
    {
        instance = this;

    }

    public GameObject jugador1;
    public GameObject jugador2;


}

