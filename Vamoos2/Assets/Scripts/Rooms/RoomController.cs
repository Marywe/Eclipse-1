using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Control de las puertas, pero con relacion al estado de los personajes en la sala 
/// como cuando se encuentre el jugador peleando.
/// </summary>
public class RoomController : MonoBehaviour
{
    public GameObject[] sala;
    public bool fighting;
    
    public Azul a;
    public Rosa r;
    private void Start()
    {
        fighting = false;
    }

}
