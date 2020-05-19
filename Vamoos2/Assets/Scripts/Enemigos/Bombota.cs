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

    AudioManager audioManager;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        boss = Controlador.instance.boss;
        audioSource = gameObject.AddComponent<AudioSource>();

        audioManager = Controlador.instance.audioManager;
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
            Vector3 directionBoss = (boss.transform.position - transform.position);
            Moving(directionBoss.normalized);

            if ((directionBoss).magnitude < 0.2f) audioManager.BOSS(audioSource, "BombAtk");
            if ((directionBoss).magnitude < 0.1f)
            {
                ++boss.GetComponent<Boss>().hits;
                Destroy(transform.GetChild(0).gameObject);
                Destroy(gameObject, 1);
            }
        }
     
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cadena")
        {
            Destroy(transform.GetChild(1).gameObject);
            isReturning = true;
            translationSpeed = 6;
        }
    }

    private void Moving(Vector3 direction)
    {
        transform.Translate(direction * translationSpeed * Time.deltaTime);
    }


    private void OnDestroy()
    {
        boss.GetComponentInChildren<Animator>().SetTrigger("Damaged");
        boss.GetComponentInChildren<Animator>().SetBool("Phase1", true);
        --Controlador.instance.currentNumEnems;
        
    }
}
