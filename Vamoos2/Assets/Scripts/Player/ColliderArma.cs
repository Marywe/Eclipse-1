using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderArma : Jugador
{
    public LayerMask enemyLayer;
    Collider[] enemiesHit;

    private bool puedeAtacar;
    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.F1))
        {
            //Animación atacar

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
