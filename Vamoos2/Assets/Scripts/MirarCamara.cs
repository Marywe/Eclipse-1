using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirarCamara : MonoBehaviour
{
    Transform tr;
    Transform tr2;
    [SerializeField]
    private Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        tr = gameObject.transform.GetChild(0);
        tr2 = gameObject.transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        Rotar();
    }
    protected virtual void Rotar()
    {
        Vector3 look;
        look.x = transform.position.x - cam.position.x;
        look.y = 0;
        look.z = transform.position.z - cam.position.z;
        transform.rotation = Quaternion.LookRotation(look);
        tr.rotation = Quaternion.LookRotation(transform.position - cam.position);
        if (tr2!=null)tr2.rotation = Quaternion.LookRotation(transform.position - cam.position);
    }
}
