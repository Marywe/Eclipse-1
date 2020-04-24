using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//El uso de esta clase es la eliminacion de los disparos de los enemigos cuando estos no tocan nada duran un tiempo
//o si hacen colision con algun lado de la sala. De esta manera evitamos consumo de objetos que no se destruyen y solo
// se dirigen al infinito.
public class Disparo : MonoBehaviour
{
    private void Start()
    {
        if(gameObject.layer!=15)
            Destroy(gameObject, 3.5f);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Salas"))
        {
            Destroy(gameObject);
            if (gameObject.layer == 15) --Controlador.instance.currentNumEnems;
        }
            

    }
}
