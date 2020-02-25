using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 4);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(gameObject);
    }
}
