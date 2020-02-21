using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    RaycastHit raycast;
    public float distRay=5;

    Transform tr;

    private void Start()
    {
        tr = transform;
    }

    private void Update()
    {
        Debug.DrawRay(tr.position, tr.forward*-distRay, Color.cyan);
        Shoot();
    }
    public void Shoot()
    {
        
        if(Physics.Raycast(tr.position, tr.forward*-distRay, out raycast,distRay))
            Debug.Log(raycast.transform.name);
    }
}
