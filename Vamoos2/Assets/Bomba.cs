using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public GameObject Bombas;
    private bool HaAparecido = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (HaAparecido)
        {
            Bombas.transform.position += Vector3.down/1.5f;
        }
    }

    public void EventoBombas()
    {
        HaAparecido = true;
        Bombas.SetActive(true);
    }

}
