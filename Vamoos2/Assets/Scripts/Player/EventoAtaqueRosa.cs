using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase para activar los eventos de ataque del personaje Azul(Mistu)
public class EventoAtaqueRosa : MonoBehaviour
{
    public ColliderArma rosa;
    public void FirstRet()
    {
        rosa.FtAt();
    }
    public void SecRet()
    {
        rosa.SecAt();
    }
    public void ThirdRet()
    {
        rosa.ThAt();
    }
}
