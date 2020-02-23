using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    RaycastHit raycast;
    public float distRay = 5;

    Transform tr;
    bool puedeDisparar = true;

    [SerializeField]
    GameObject disparo;
    Vector3 vD;
    private void Start()
    {
        tr = transform;
    }

    private void Update()
    {
        Debug.DrawRay(tr.position, tr.forward * -distRay, Color.cyan);
        Shoot();
    }
    public void Shoot()
    {

        if (Physics.Raycast(tr.position, tr.forward * -distRay, out raycast, distRay))
            if (raycast.transform.tag == "Player" && puedeDisparar)
            {
                //Animación

                //instantiiate
                GameObject newDisparo = Instantiate(disparo, transform.position, Quaternion.identity);
                vD.x = raycast.transform.position.x - tr.position.x;
                vD.y = 0;
                vD.z = raycast.transform.position.z - tr.position.z;
                newDisparo.GetComponent<Rigidbody>().AddForce(vD.normalized * 60 * 20);
                StartCoroutine(corDisparo());

            }
    }

    IEnumerator corDisparo()
    {
        puedeDisparar = false;
        yield return new WaitForSeconds(3);
        puedeDisparar = true;
    }
}
