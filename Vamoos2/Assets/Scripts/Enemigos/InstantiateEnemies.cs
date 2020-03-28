using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mediante esta clase genereamos los enemigos.
public class InstantiateEnemies : MonoBehaviour
{
    [SerializeField]
    Transform[] mismuertos;
    List<Transform> posSpawn = new List<Transform>();
    [SerializeField]
    GameObject enemigo;
    // Start is called before the first frame update
    void Start()
    {
        //tras crear una lista de posiciones, las vamos rellenando con enemigos.
        foreach (Transform poss in posSpawn)
        {
            GameObject newEnemy = Instantiate(enemigo);

            Debug.Log("Oye :/");
        }
    }
   
}
