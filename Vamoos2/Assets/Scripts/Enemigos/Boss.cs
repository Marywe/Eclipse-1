using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    protected Animator anim;
    public int hits = 0;

    public float duracionMuerte;
    public GameObject pantallaFinal;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("Hits", hits);

        if (hits == 3)
        {
            StartCoroutine(corMuerte());
        }
    }

    IEnumerator corMuerte(){

        yield return new WaitForSeconds(duracionMuerte);
        pantallaFinal.SetActive(true);
        Time.timeScale = 0f;
    }
}
