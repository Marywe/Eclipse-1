using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderArmaArrow : MonoBehaviour
{
    public LayerMask enemyLayer;
    Collider[] enemiesHit;
    Animator anim;
    private bool puedeAtacar;
    private int dano = 1;

    [SerializeField]
    private float radio = 2;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInParent<Animator>();
        puedeAtacar = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (puedeAtacar) Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radio);
    }

    private void Attack()
    {
        if (Input.GetButton("Fire2"))
        {
            //Animación atacar
            anim.SetTrigger("Attack");

            enemiesHit = Physics.OverlapSphere(this.transform.position, radio, enemyLayer);

            foreach (Collider enemy in enemiesHit)
            {
                Debug.Log("uwu");
                enemy.GetComponent<Enemigos>().TakeDamage(dano);
            }

            StartCoroutine(corBasicAtt());

        }
    }

    private IEnumerator corBasicAtt()
    {
        puedeAtacar = false;
        yield return new WaitForSeconds(2);
        puedeAtacar = true;
    }
}
