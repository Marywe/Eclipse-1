using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
    Animator animPuerta;
    // Start is called before the first frame update
    void Start()
    {
        animPuerta = gameObject.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(animPuerta.GetBool("SePuedePasar") && animPuerta.GetBool("Cerca"))
            {

            }
        }
    }
}
