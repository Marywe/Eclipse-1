using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Comprobamos que enemigo esta mas cerca del jugador
public class NearbyEnemy : MonoBehaviour
{
    protected List<Transform> enemies = new List<Transform>();

    //Hacemos comprobaciones para saber la posicion del enemigo,
    //haciendole ir hacia el jugador si es el que se encuentra mas cerca.
    Transform GetClosestEnemy()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
}
