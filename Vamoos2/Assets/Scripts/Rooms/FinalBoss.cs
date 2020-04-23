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

    private const int numBombas = 10;

    public Transform centroPozo;

    public GameObject boss;

    public Transform[] posicionesObjetivos;
    public static bool entering = false;


    public GameObject Araxiel;
    public GameObject Mistu;

    public CharacterController cr;
    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
    }

    private void Update()
    {
        if (entering)
        {
            Moverse();
        }
    }


    public   void Entrada() //Que no se muevan
    {
        //CINEMACHINE GUAPA 

        Mistu.GetComponent<CharacterController>().enabled = false;
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
    void Phase1()
    {
    }

    void Phase2()
    {

    }
    void corEntrada()
    {

        Controlador.instance.objetivo1.gameObject.GetComponent<CharacterController>().enabled = true;
        Controlador.instance.objetivo2.gameObject.GetComponent<CharacterController>().enabled = true;
    }

    void SpawnEnems()
    {
        GameObject newEnem = Instantiate(Controlador.instance.prefabMariposa, ptosSpawn[0].position, ptosSpawn[0].rotation);
        Controlador.instance.currentNumEnems = 1;
    }

    public void SpawnBombas()
    {
        InvokeRepeating("InstanciarBombas", 3, 3);
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
            Vector3 randomPosition = new Vector3(Random.Range(-249, -197), -79, Random.Range(-625, -587));
            GameObject newBomba = Instantiate(prefabBomba, randomPosition, Quaternion.identity);
        }
    }

    public void BombaTocha()
    {
        GameObject.FindWithTag("Boss").GetComponent<Animator>().SetBool("ENDED", false);

        GameObject newBombaTocha = Instantiate(prefabBombaTocha, centroPozo.position, Quaternion.identity);
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
