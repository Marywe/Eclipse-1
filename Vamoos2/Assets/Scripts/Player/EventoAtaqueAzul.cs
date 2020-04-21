using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase para activar los eventos de ataque del personaje Azul(Mistu)
public class EventoAtaqueAzul : MonoBehaviour
{
    public ColliderArmaArrow c;
    public void FirstRet()
    {
        c.FtAt();
    }
    public void SecRet()
    {
        c.SecAt();
    }
    public void ThirdRet()
    {
        c.ThAt();
    }
}
