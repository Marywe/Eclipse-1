using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        foreach (Transform poss in posSpawn)
        {
            GameObject newEnemy = Instantiate(enemigo);

            Debug.Log("Oye :/");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
