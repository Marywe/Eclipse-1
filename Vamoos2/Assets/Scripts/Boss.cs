using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Controlador.instance.currentNumEnems==0 && !anim.GetBool("BombTime")) //Cuando termina la fase spawn
        {
            anim.SetBool("ENDED", true);
            anim.SetBool("BombTime", true);
        }

        if(Controlador.instance.currentNumEnems == 0 && !anim.GetBool("Phase1")) //Cuando termina la bomba tocha
        {
            anim.SetBool("ENDED", true);
            anim.SetBool("BombTime", false);
            anim.SetBool("Phase1", true);
        }
    }
}
