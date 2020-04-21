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

    private int numBombas = 10;

    private Bounds limits;
    public Collider sala;
    public Transform centroPozo;

    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
       
        limits = sala.bounds;
    }

    public void Entrada() //Que no se muevan
    {
        Controlador.instance.objetivo1.gameObject.GetComponent<CharacterController>().enabled = false;
        Controlador.instance.objetivo2.gameObject.GetComponent<CharacterController>().enabled = false;

        //REPRODUCIR RISA DEL PIBE

        boss.SetActive(true);
        StartCoroutine(corEntrada());
    }
    void Phase1()
    { 
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
        for (int i = 0; i < 3; i++)
        {
            Invoke("InstanciarBombas", tiempoEntreBombas);
            tiempoEntreBombas -= 0.1f;
        }

        //Bomba tocha
    }
    void InstanciarBombas()
    {
        for (int i = 0; i < numBombas; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(limits.min.x, limits.max.x), 0, Random.Range(limits.min.z, limits.max.z));
            GameObject newBomba = Instantiate(prefabBomba, randomPosition, Quaternion.identity);
        }
            
    }

    void BombaTocha()
    {
        GameObject newBombaTocha = Instantiate(prefabBombaTocha, centroPozo.position, Quaternion.identity);
       
    }
}
