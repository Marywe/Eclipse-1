using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpBoss : MonoBehaviour
{
    public Transform[] posicionesObjetivos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform mistu = Controlador.instance.objetivo1;
            Transform araxiel = Controlador.instance.objetivo2;

            mistu.GetComponent<CharacterController>().enabled = false;
            araxiel.GetComponent<CharacterController>().enabled = false;

            mistu.position = posicionesObjetivos[0].position;
            araxiel.position = posicionesObjetivos[1].position;

            yield return null;
            //StartCoroutine(corEnable(mistu, araxiel));

            mistu.GetComponent<CharacterController>().enabled = true;
            araxiel.GetComponent<CharacterController>().enabled = true;
        }
    }

    IEnumerator corEnable(Transform m, Transform a)
    {
        yield return new WaitForSeconds(0.5f);
        m.GetComponent<CharacterController>().enabled = true;
        a.GetComponent<CharacterController>().enabled = true;
    }
}
