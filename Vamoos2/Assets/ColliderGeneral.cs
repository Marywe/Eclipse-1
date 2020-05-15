using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderGeneral : MonoBehaviour
{
    public GameObject dialogue;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogue.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogue.SetActive(false);
        }
    }
}
