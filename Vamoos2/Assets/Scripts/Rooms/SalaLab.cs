using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalaLab : Salas
{
    private GameObject newScorpio;
    [SerializeField]
    private Transform plataforma;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.ControladorPuertas(doors);

        if (Controlador.instance.currentNumEnems == 1 && salaCleanFirstTime)
        {
            SubirPlataforma();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            luces.SetActive(true);
            Controlador.instance.cam = Controlador.instance.ptoscamara[1];
            if (!salaCleanFirstTime) InstanciarEnemigos();
            Controlador.instance.dondeEstas = Controlador.DondeEstas.sLab;
        }

    }
    //Control de la iluminación de la sala, ademas de cambiar el estado de la posicion de sala del jugador
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) Controlador.instance.dondeEstas = Controlador.DondeEstas.sLab;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            luces.SetActive(false);
    }

    //Instanciar enemigos en sus posiciones
    void InstanciarEnemigos()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject newMariposa = Instantiate(Controlador.instance.prefabMariposa, puntosSpawn[i].position, puntosSpawn[i].rotation);
        }
        newScorpio = Instantiate(Controlador.instance.prefabEscorpion, puntosSpawn[2].position, puntosSpawn[2].rotation);
        newScorpio.GetComponent<Scorpio>().enabled = false;
        Controlador.instance.currentNumEnems = 4;
        salaCleanFirstTime = true;
    }

    void SubirPlataforma()
    {
        plataforma.transform.position = Vector3.MoveTowards(plataforma.transform.position, target.position, speed * Time.deltaTime);

        if (plataforma.transform.position == target.position) plataforma.gameObject.isStatic = true;
        newScorpio.GetComponent<Scorpio>().enabled = true;
    }
}
