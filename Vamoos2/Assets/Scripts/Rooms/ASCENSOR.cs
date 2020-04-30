using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASCENSOR : Salas
{
    Animator ascensorAnim;

    // Start is called before the first frame update
    void Start()
    {
        salaClean = true;
        salaCleanFirstTime = true;
        numPuertas = 2;
        ascensorAnim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (ascensorAnim.GetCurrentAnimatorStateInfo(0).IsTag("SUBIENDO") || ascensorAnim.GetCurrentAnimatorStateInfo(0).IsTag("BAJANDO"))
        {
            salaClean = false;
        }
        else salaClean = true;

        base.ControladorPuertas(doors);
    }


}
