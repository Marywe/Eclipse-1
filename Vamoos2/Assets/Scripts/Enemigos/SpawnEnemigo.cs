using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generador de enemigos, control de la cantidad de enemigos
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
    //Mediante una corrutina controlamos el numeros de prefabs de enemigos que generamos
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

    //tras un tiempo, cambiamos la variable
    private IEnumerator CooldownEnemigos()
    {        
        yield return new WaitForSeconds(tiempoCooldown);
        sePuedeSpawnear = true;        
    }

}
