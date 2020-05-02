using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script para que los sprites miren a la cámara
/// </summary>
public class MirarCamara : MonoBehaviour
{
    Transform tr;
    Transform tr2;
    private Transform cam;
    // llamamos a la camara y conseguimos las posiciones de los personajes
    void Start()
    {
        tr = gameObject.transform.GetChild(0);
        tr2 = gameObject.transform.GetChild(1);
        cam = Controlador.instance.cam;
    }

    // Update is called once per frame
    void Update()
    {
        Rotar();
    }
    //Encargado de ajustar la rotacion de la camara en relacion a la posicion de los personajes.
    protected virtual void Rotar()
    {
        Vector3 look;
        look.x = transform.position.x - cam.position.x;
        look.y = 0;
        look.z = transform.position.z - cam.position.z;
        transform.rotation = Quaternion.LookRotation(look);

        tr.rotation = Quaternion.LookRotation(tr.position - cam.position);
        if (tr2!=null)tr2.rotation = Quaternion.LookRotation(tr2.position - cam.position);
    }
}
