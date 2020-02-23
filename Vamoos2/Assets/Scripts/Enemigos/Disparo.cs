using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    private void Start()
    {
        Destroy(this, 4);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(this);
    }
}
