using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirarCamObjetos : MonoBehaviour
{
    Transform tr;
    [SerializeField]
    Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        tr = gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        tr.rotation = Quaternion.LookRotation(transform.position - cam.position);
        Vector3 look;
        look.x = transform.position.x - cam.position.x;
        look.y = 0;
        look.z = transform.position.z - cam.position.z;
        transform.rotation = Quaternion.LookRotation(look);
    }
}
