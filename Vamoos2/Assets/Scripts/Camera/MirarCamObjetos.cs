using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Al igual que ocurre con los personajes, los objetos tambien se posicionan visualmente mirando a la camara.
public class MirarCamObjetos : MonoBehaviour
{
    Transform tr;
    Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        tr = gameObject.transform.GetChild(0);
        cam = Controlador.instance.cam;
    }

    // con Update, mantenemos actualizado la rotacion de los objeto
    void Update()
    {
        tr.rotation = Quaternion.LookRotation(transform.position - cam.position);
        Vector3 look;
        look.x = transform.position.x - cam.position.x;
        look.y = 0;
        look.z = transform.position.z - cam.position.z;
        transform.rotation = Quaternion.LookRotation(look);
    }
}
