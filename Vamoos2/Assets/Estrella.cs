using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estrella : MonoBehaviour
{
    private GameObject escudo;
    private bool vulnerable;

    public GameObject dialogue;

    private void Start()
    {
        escudo = transform.GetChild(1).gameObject;
        vulnerable = true;
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "Estrella") 
        {
            if (other.gameObject.tag == "Cadena" && vulnerable)
            {
                escudo.SetActive(false);
                vulnerable = false;

                yield return new WaitForSeconds(5);
                escudo.SetActive(true);
                vulnerable = true;
            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                dialogue.SetActive(true);
            }
        }

        yield return null;
    }

    private void OnTriggerExit(Collider other)
    {
        if(gameObject.name != "Estrella" && other.CompareTag("Player"))
        {
            dialogue.SetActive(false);
        }
    }



}
