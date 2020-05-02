using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinalBoss : MonoBehaviour
{
    [SerializeField]
    private Transform[] ptosSpawn;
    [SerializeField]
    GameObject prefabBomba;
    [SerializeField]
    GameObject prefabBombaTocha;

    private const int numBombas = 20;

    public Transform centroPozo;

    public GameObject boss;

    public Transform[] posicionesObjetivos;
    public static bool entering = false;


    public GameObject Araxiel;
    public GameObject Mistu;



    private void Update()
    {
        if (entering)
        {
            Moverse();
        }
    }


    public void Entrada(GameObject Mistu, GameObject Araxiel) //Que no se muevan
    {
        //CINEMACHINE GUAPA 
        CharacterController controller = Mistu.GetComponent<CharacterController>();
       controller.enabled = false;
        Araxiel.GetComponent<CharacterController>().enabled = false;


        //REPRODUCIR RISA DEL PIBE

        Invoke("corEntrada", 3);

    }

    private void Moverse()
    {
        if ((Mistu.transform.position - posicionesObjetivos[0].position).magnitude < 0.3f)
        {
            Mistu.GetComponent<Azul>().SetSpeedValue(1);
            Mistu.GetComponent<Rigidbody>().AddForce((posicionesObjetivos[0].position - Controlador.instance.objetivo1.position).normalized * 50 * Time.deltaTime);
        }
        else Mistu.GetComponent<Azul>().SetSpeedValue(0);

        if ((Araxiel.transform.position - posicionesObjetivos[1].position).magnitude < 0.3f)
        {
            Araxiel.GetComponent<Rosa>().SetSpeedValue(1);
            Araxiel.GetComponent<Rigidbody>().AddForce((posicionesObjetivos[1].position - Controlador.instance.objetivo2.position).normalized * 50 * Time.deltaTime);
        }
        else Araxiel.GetComponent<Rosa>().SetSpeedValue(0);

        if (Mistu.transform.position != posicionesObjetivos[0].position && Araxiel.transform.position != posicionesObjetivos[1].position)
        {
            entering = false;
        }
    }
    void corEntrada()
    {

        Controlador.instance.objetivo1.gameObject.GetComponent<CharacterController>().enabled = true;
        Controlador.instance.objetivo2.gameObject.GetComponent<CharacterController>().enabled = true;
    }

    void SpawnEnems()
    {
        int numEnems = Random.Range(3, 6);
        for (int i = 0; i < numEnems; i++)
        {
            int rndEnem = Random.Range(0, 3);
            switch (rndEnem)
            {
                case 0:
                    GameObject newEnem0 = Instantiate(Controlador.instance.prefabMariposa, ptosSpawn[i].position, ptosSpawn[i].rotation);
                    break;
                case 1:
                    GameObject newEnem1 = Instantiate(Controlador.instance.prefabEscorpion, ptosSpawn[i].position, ptosSpawn[i].rotation); 
                    break;
                case 2:
                    GameObject newEnem2 = Instantiate(Controlador.instance.prefabRobot, ptosSpawn[i].position, ptosSpawn[i].rotation);
                    break;
                case 3:
                    GameObject newEnem3 = Instantiate(Controlador.instance.prefabPrisma, ptosSpawn[i].position, ptosSpawn[i].rotation);
                    break;
            }    
        }
        
        Controlador.instance.currentNumEnems = numEnems;
    }

    public void SpawnBombas()
    {
        InvokeRepeating("InstanciarBombas", 3, 3);
        GameObject.FindWithTag("Boss").GetComponent<Animator>().SetBool("Phase1", false);
        Invoke("CancelarInvoke", 15);
    }

    void CancelarInvoke()
    {
        CancelInvoke("InstanciarBombas");
        GameObject.FindWithTag("Boss").GetComponent<Animator>().SetBool("ENDED", true);
        
    }
    void InstanciarBombas()
    {
        for (int i = 0; i < numBombas; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(SalaBoss.bounds.min.x, SalaBoss.bounds.max.x), -82, Random.Range(SalaBoss.bounds.min.z, SalaBoss.bounds.max.z));
            GameObject newBomba = Instantiate(prefabBomba, randomPosition, Quaternion.identity);
        }
    }

    public void BombaTocha()
    {        
        GameObject newBombaTocha = Instantiate(prefabBombaTocha, centroPozo.position, Quaternion.identity);
        Controlador.instance.currentNumEnems = 1;
        Vector3 direction = Vector3.forward;
        if ((Mistu.transform.position - newBombaTocha.transform.position).magnitude > (Araxiel.transform.position - newBombaTocha.transform.position).magnitude)
            direction = Araxiel.transform.position - newBombaTocha.transform.position;
        else
            direction = Mistu.transform.position - newBombaTocha.transform.position;


        Quaternion rotation = Quaternion.LookRotation(direction);
        newBombaTocha.transform.rotation = Quaternion.RotateTowards(newBombaTocha.transform.rotation, rotation, Time.deltaTime * 50);
        //Desplazamiento
        newBombaTocha.transform.Translate(Vector3.forward * 20 * Time.deltaTime);
    }
}
