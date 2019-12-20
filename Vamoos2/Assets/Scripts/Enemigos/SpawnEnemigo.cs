using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemigo : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabEnemigo;

    [SerializeField]
    private float tiempoCooldown = 1f;

    private bool sePuedeSpawnear = true;
    
    void Update()
    {
        Spawnear();
    }

    private void Spawnear()
    {
        if (sePuedeSpawnear)
        {
            GameObject newEnemigo = Instantiate(prefabEnemigo);
            newEnemigo.transform.position = transform.position;
            sePuedeSpawnear = false;
            StartCoroutine(CooldownEnemigos());
        }
    }
    private IEnumerator CooldownEnemigos()
    {        
        yield return new WaitForSeconds(tiempoCooldown);
        sePuedeSpawnear = true;        
    }

}
