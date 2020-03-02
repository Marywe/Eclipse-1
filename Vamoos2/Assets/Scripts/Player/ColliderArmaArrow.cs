using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderArmaArrow : MonoBehaviour
{
    public LayerMask enemyLayer;
    Collider[] enemiesHit;
    [SerializeField]
    Animator anim;
    private bool puedeAtacar;

    public Vector3 posicion;

    [SerializeField]
    private Vector3 cubeSz;
    float lastButTime;
    public float maxComboDelay = 0.9f;
    public int nBut;

    private Azul a;

    // Start is called before the first frame update
    void Start()
    {
        cubeSz.x = 2.5f;
        cubeSz.y = 3;
        cubeSz.z = 3;
        posicion.x =0.7f;
        posicion.y = 0;
        posicion.z = 0;
        transform.position = posicion;
        puedeAtacar = true;
        a = gameObject.GetComponentInParent<Azul>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float attackDirection = a.anim.GetFloat("Direction");
        if (attackDirection >= 0)
        {
            posicion.x = 0.7f;
            transform.localPosition = posicion;
        }
        else if (attackDirection < 0)
        {
            posicion.x = -0.7f;
            transform.localPosition = posicion;
        }

            if (puedeAtacar) BasicAttack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(base.transform.position, cubeSz);
    }

    private void BasicAttack()
    {
        if (Time.time - lastButTime > maxComboDelay)
        {
            nBut = 0;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            lastButTime = Time.time;
            nBut++;
            anim.SetBool("Attack", true);

            if (nBut == 1)
            {
                SetBasicAttack(0);

                enemiesHit = Physics.OverlapBox(transform.position, cubeSz / 2);
                //enemiesHit = Physics.OverlapSphere(this.transform.position, cubeSz, enemyLayer);
                foreach (Collider enemy in enemiesHit)
                {
                    enemy.GetComponent<Enemigos>().TakeDamage(a.dano);
                }

            }

            nBut = Mathf.Clamp(nBut, 0, 3);
        }

    }

    public void SetBasicAttack(float f)
    {
        anim.SetFloat("AttackN", f);
    }
    private IEnumerator corBasicAtt()
    {
        puedeAtacar = false;
        yield return new WaitForSeconds(2);
        puedeAtacar = true;
    }

    public void FtAt()
    {
        if (nBut >= 2)
        {
            SetBasicAttack(0.5f);
            enemiesHit = Physics.OverlapBox(transform.position, cubeSz / 2);
            //enemiesHit = Physics.OverlapSphere(this.transform.position, cubeSz, enemyLayer);
            foreach (Collider enemy in enemiesHit)
            {
                enemy.GetComponent<Enemigos>().TakeDamage(a.dano);
            }

        }
        else
        {
            StartCoroutine(corBasicAtt());
            anim.SetBool("Attack", false);
            nBut = 0;
        }

    }
    public void SecAt()
    {
        if (nBut >= 3)
        {
            Debug.Log("hhh");
            SetBasicAttack(1f);
            enemiesHit = Physics.OverlapBox(transform.position, cubeSz / 2);
            //enemiesHit = Physics.OverlapSphere(this.transform.position, cubeSz, enemyLayer);
            foreach (Collider enemy in enemiesHit)
            {
                enemy.GetComponent<Enemigos>().TakeDamage(a.dano);
            }

        }
        else
        {
            nBut = 0;
            anim.SetBool("Attack", false);
            StartCoroutine(corBasicAtt());
        }
    }
    public void ThAt()
    {
        Debug.Log("htt");
        anim.SetBool("Attack", false);
        StartCoroutine(corBasicAtt());
        nBut = 0;
    }
}
