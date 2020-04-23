using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalaBoss : MonoBehaviour
{
    public GameObject boss;
    public FinalBoss f;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Controlador.instance.dondeEstas = Controlador.DondeEstas.sBoss;
            boss.SetActive(true);
            boss.transform.GetChild(0).GetComponent<Animator>().SetBool("ENDED", false);
            f.Entrada();
        }
    }
}
