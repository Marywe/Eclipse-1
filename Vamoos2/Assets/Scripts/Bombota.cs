using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bombota : MonoBehaviour
{
    [SerializeField]
    private float translationSpeed;

    GameObject boss;

    private bool isReturning = false;
    // Start is called before the first frame update
    void Start()
    {
        boss = Controlador.instance.boss;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReturning)
        {
            Vector3 dir1 = (Controlador.instance.objetivo1.position - transform.position);
            Vector3 dir2 = (Controlador.instance.objetivo2.position - transform.position);

            Vector3 direction = Vector3.zero;
            if (dir1.magnitude > dir2.magnitude)
                direction = dir2.normalized;
            else direction = dir1.normalized;

            Moving(direction);
        }

        else
        {
            Vector3 directionBoss = (Controlador.instance.boss.transform.position - transform.position).normalized;
            Moving(directionBoss);
        }
     
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cadena")
        {
            Debug.Log("holy");
            isReturning = true;
        }
        if(other.CompareTag("Player")) Debug.Log("xd");
    }

    private void Moving(Vector3 direction)
    {
        transform.Translate(direction * translationSpeed * Time.deltaTime);
    }


    private void OnDestroy()
    {
        --Controlador.instance.currentNumEnems;
        ++boss.GetComponent<Boss>().hits;
    }
}
