using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombota : MonoBehaviour
{
    [SerializeField]
    private float translationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir1 = (Controlador.instance.objetivo1.position - transform.position);
        Vector3 dir2 = (Controlador.instance.objetivo2.position - transform.position);

        Vector3 direction = Vector3.zero;
        if (dir1.magnitude > dir2.magnitude)
            direction = dir2.normalized;
        else direction = dir1.normalized;
        

        transform.Translate(direction * translationSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        --Controlador.instance.currentNumEnems;
    }
}
