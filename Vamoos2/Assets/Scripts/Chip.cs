using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    private bool PuertaGrandeAbierta = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Controlador.instance.AddChips();
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        if (Controlador.instance.chips == 4)
        {
            PuertaGrandeAbierta = true;
        }
    }
}
