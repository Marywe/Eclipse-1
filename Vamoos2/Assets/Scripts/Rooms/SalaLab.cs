using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SalaLab : Salas
{
    private GameObject newPrisma;
    [SerializeField]
    private Transform plataforma;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform target;

    [SerializeField]
    Collider noCaerse;
    // Start is called before the first frame update
    void Start()
    {
        target.GetComponent<MeshRenderer>().enabled = false;
        newPrisma = Controlador.instance.prefabPrisma;
        numPuertas = 3;
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
            Controlador.instance.ptoscamara[4].SetActive(true);
            if (!salaCleanFirstTime) InstanciarEnemigos();
            Controlador.instance.dondeEstas = Controlador.DondeEstas.sLab;
        }

    }
    //Control de la iluminación de la sala, ademas de cambiar el estado de la posicion de sala del jugador


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            luces.SetActive(false);
            Controlador.instance.ptoscamara[4].SetActive(false);
        }
        }

    //Instanciar enemigos en sus posiciones
    void InstanciarEnemigos()
    {
        for (int i = 1; i <= 2; i++)
        {
            GameObject newMariposa = Instantiate(Controlador.instance.prefabMariposa, puntosSpawn[i].position, puntosSpawn[i].rotation);
        }
        GameObject newScorpio = Instantiate(Controlador.instance.prefabEscorpion, puntosSpawn[0].position, puntosSpawn[0].rotation);

        newPrisma = Instantiate(Controlador.instance.prefabPrisma, puntosSpawn[3].position, puntosSpawn[3].rotation);
        newPrisma.GetComponent<NavMeshAgent>().enabled = false;
        Controlador.instance.currentNumEnems = 4;
        salaCleanFirstTime = true;
    }

    void SubirPlataforma()
    {
        plataforma.transform.position = Vector3.MoveTowards(plataforma.transform.position, target.position, speed * Time.deltaTime);

        if (plataforma.transform.position == target.position)
        {
            noCaerse.enabled = false;
            plataforma.gameObject.isStatic = true;
            newPrisma.GetComponent<Prisma>().enabled = true;
            newPrisma.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
