using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase herencia
/// Control de la primera sala, donde apareceran los jugadores, controlando si han completado
/// los objetivos o no.
/// </summary>
public class Sala1 : Salas

{
    // Start is called before the first frame update
    void Start()
    {
        salaClean = true;
        salaCleanFirstTime = true;
        numPuertas = 1;
    }

    //Cuando los jugadores completen derrotar a los enemigos, podrán activar el evento de pasar de sala.
    void Update()
    {
        base.ControladorPuertas(doors);
    }
    //Al entrar a la sala, activar luces y mover la camara
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            luces.SetActive(true);

            Controlador.instance.ptoscamara[0].SetActive(true);
            Controlador.instance.dondeEstas = Controlador.DondeEstas.s1;
        }
    }

    //Al salir de la sala, apagar luces
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Controlador.instance.ptoscamara[0].SetActive(false);
            luces.SetActive(false);
        }
    }



}
