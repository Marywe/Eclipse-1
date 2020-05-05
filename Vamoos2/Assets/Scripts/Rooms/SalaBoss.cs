using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalaBoss : MonoBehaviour
{
    public static Bounds bounds;

    [SerializeField]
    private Transform[] ptosSpawn;
    [SerializeField]
    GameObject prefabBomba;
    [SerializeField]
    GameObject prefabBombaTocha;

    private const int numBombas = 35;

    public Transform centroPozo;

    public GameObject boss;

    public Transform[] posicionesObjetivos;
    public static bool entering = false;


    public GameObject Araxiel;
    public GameObject Mistu;

    private Animator bossanim;

    private bool haHechoCosas = false;
    // Start is called before the first frame update
    private void Start()
    {
        Mistu = Controlador.instance.objetivo1.gameObject;
        Araxiel = Controlador.instance.objetivo2.gameObject;
        bounds = GetComponent<Collider>().bounds;
        bossanim = boss.transform.GetChild(0).GetComponent<Animator>();
    }
    private void Update()
    {
       

        if (bossanim.GetCurrentAnimatorStateInfo(0).IsTag("bomba") && !haHechoCosas)
        {
            SpawnBombas();
            haHechoCosas = true;
        }
        else if (bossanim.GetCurrentAnimatorStateInfo(0).IsTag("spawn") && !haHechoCosas)
        {
            SpawnEnems();
            haHechoCosas = true;
        }
        else if (bossanim.GetCurrentAnimatorStateInfo(0).IsTag("bombote") && !haHechoCosas)
        {
            haHechoCosas = true;
        }
        else if (bossanim.GetCurrentAnimatorStateInfo(0).IsTag("idle") && haHechoCosas)
        {
            haHechoCosas = false;
        }
        

        //ACABAR SPAWN CUANDO SE HAYAN MUERTO LOS ENEMIES
        if (bossanim.GetCurrentAnimatorStateInfo(0).IsTag("idle") && !bossanim.GetBool("BombTime") && Controlador.instance.currentNumEnems==0) 
        {
            bossanim.SetBool("BombTime", true);
            bossanim.SetBool("ENDED", true);
        }



    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Controlador.instance.ptoscamara[12].SetActive(true);
            Controlador.instance.dondeEstas = Controlador.DondeEstas.sBoss;
            boss.SetActive(true);
            bossanim.SetBool("ENDED", false);
            yield return 0;


            Entrada();
        }
    }

    public void Entrada() //Que no se muevan
    {
        

        //REPRODUCIR RISA DEL PIBE

        //Invoke("corEntrada", 3);
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
        bossanim.SetBool("Phase1", false);
        InvokeRepeating("InstanciarBombas", 3, 3);  
        Invoke("CancelarInvoke", 15);
    }

    void CancelarInvoke()
    {
        CancelInvoke("InstanciarBombas");
        bossanim.SetBool("ENDED", true);
    }
    void InstanciarBombas()
    {
        for (int i = 0; i < numBombas; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(SalaBoss.bounds.min.x, SalaBoss.bounds.max.x), 8.54f, Random.Range(SalaBoss.bounds.min.z, SalaBoss.bounds.max.z));
            GameObject newBomba = Instantiate(prefabBomba, randomPosition, Quaternion.identity);
        }
    }

    public void BombaTocha()
    {
        GameObject newBombaTocha = Instantiate(prefabBombaTocha, centroPozo.position, Quaternion.identity);
        Controlador.instance.currentNumEnems = 1;
       
    }    
}
