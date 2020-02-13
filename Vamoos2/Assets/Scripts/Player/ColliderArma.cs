using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderArma : MonoBehaviour
{
    public LayerMask enemyLayer;
    Collider[] enemiesHit;
    Animator anim;
    private bool puedeAtacar;
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
        Gizmos.DrawWireSphere(transform.position, 1f);
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Animación atacar
            anim.SetTrigger("Attack");

            enemiesHit = Physics.OverlapCapsule(this.transform.position, this.transform.position * 2, 1, enemyLayer);

            foreach (Collider enemy in enemiesHit)
            {
                Debug.Log("uwu");
                enemy.GetComponent<Enemigos>().TakeDamage(1);
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
