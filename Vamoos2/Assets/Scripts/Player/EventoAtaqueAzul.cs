using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
