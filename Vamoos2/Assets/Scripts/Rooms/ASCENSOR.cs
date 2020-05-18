using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASCENSOR : Salas
{
    Animator ascensorAnim;

    // Start is called before the first frame update
    void Start()
    {
        salaClean = true;
        salaCleanFirstTime = true;
        numPuertas = 2;
        ascensorAnim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (ascensorAnim.GetCurrentAnimatorStateInfo(0).IsTag("SUBIENDO") || ascensorAnim.GetCurrentAnimatorStateInfo(0).IsTag("BAJANDO"))
        {
            salaClean = false;
        }
        else salaClean = true;

        base.ControladorPuertas(doors);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Controlador.instance.ptoscamara[9].SetActive(true);
            Controlador.instance.dondeEstas = Controlador.DondeEstas.Ascensor;
            ascensorAnim.SetTrigger("Moverse");

            Controlador.instance.objetivo1.SetParent(ascensorAnim.gameObject.transform);
            Controlador.instance.objetivo2.SetParent(ascensorAnim.gameObject.transform);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Controlador.instance.objetivo1.position = new Vector3(Controlador.instance.objetivo1.position.x, ascensorAnim.transform.position.y + 2, Controlador.instance.objetivo1.position.z);
            //Controlador.instance.objetivo2.position = new Vector3(Controlador.instance.objetivo2.position.x, ascensorAnim.transform.position.y +2, Controlador.instance.objetivo2.position.z);
        }
    }

    //Al salir de la sala, apagar luces
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Controlador.instance.ptoscamara[9].SetActive(false);
            Controlador.instance.objetivo1.SetParent(null);
            Controlador.instance.objetivo2.SetParent(null);
        }
    }


}
