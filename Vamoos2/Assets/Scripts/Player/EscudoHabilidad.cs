using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscudoHabilidad : MonoBehaviour
{
    Rosa r;

    private void Start()
    {
        r = gameObject.GetComponentInParent<Rosa>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Player"))
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CompareTag("Player"))
        {

        }
    }
}
