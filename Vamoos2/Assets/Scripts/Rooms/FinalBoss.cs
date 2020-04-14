using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinalBoss : MonoBehaviour
{
    public UnityEvent uwu;
    [SerializeField]
    private Transform[] ptosSpawn;
    [SerializeField]
    GameObject prefabBomba;
    [SerializeField]
    GameObject prefabBombaTocha;

    private int numBombas = 10;


    public Collider sala;
    private Bounds limits;

    // Start is called before the first frame update
    void Start()
    {
        limits = sala.bounds;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Entrada() //Que no se muevan
    {
        Controlador.instance.objetivo1.gameObject.GetComponent<CharacterController>().enabled = false;
        Controlador.instance.objetivo2.gameObject.GetComponent<CharacterController>().enabled = false;

        //REPRODUCIR RISA DEL PIBE

        StartCoroutine(corEntrada());
    }
    void Phase1()
    {
        uwu.Invoke();
        
    }

    void Phase2()
    {

    }
    IEnumerator corEntrada()
    {

        yield return new WaitForSeconds(3);
        Controlador.instance.objetivo1.gameObject.GetComponent<CharacterController>().enabled = true;
        Controlador.instance.objetivo2.gameObject.GetComponent<CharacterController>().enabled = true;
    }

    void SpawnEnems()
    {
        GameObject newEnem = Instantiate(Controlador.instance.prefabMariposa, ptosSpawn[0].position, ptosSpawn[0].rotation);
        Controlador.instance.currentNumEnems = 1;
    }

    void SpawnBombas()
    {
        float tiempoEntreBombas = 3;
        for (int i = 0; i < numBombas; i++)
        {
            Invoke("InstanciarBombas", tiempoEntreBombas);
            tiempoEntreBombas -= 0.1f;
        }

        //Bomba tocha
    }
    void InstanciarBombas()
    {
        Vector3 randomPosition = new Vector3(Random.Range(limits.min.x, limits.max.x), 0, Random.Range(limits.min.z, limits.max.z));
        GameObject newBomba = Instantiate(prefabBomba, randomPosition, Quaternion.identity);        
    }

    void BombaTocha()
    {
        GameObject newBombaTocha = Instantiate(prefabBombaTocha);
        Ray rayo1 = new Ray(newBombaTocha.transform.position, Controlador.instance.objetivo1.position);
        Ray rayo2 = new Ray(newBombaTocha.transform.position, Controlador.instance.objetivo2.position);

        if (Physics.Raycast(rayo1, out RaycastHit hit));
    }
}
