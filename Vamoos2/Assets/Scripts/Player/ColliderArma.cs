using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderArma : MonoBehaviour
{
    public LayerMask enemyLayer;
    Collider[] enemiesHit;
    Animator anim;
    private bool puedeAtacar;

    private int dano = 1;

    [SerializeField]
    private float radio = 2;

    float lastButTime;
    public float maxComboDelay = 0.9f;
    public int nBut = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        puedeAtacar = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (puedeAtacar) BasicAttack();

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radio);
    }

    private void BasicAttack()
    {
        if (Time.time - lastButTime > maxComboDelay)
        {
            nBut = 0;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            lastButTime = Time.time;
            nBut++;
            anim.SetBool("Attack", true);

            if (nBut == 1)
            {
                SetBasicAttack(0);
                

                enemiesHit = Physics.OverlapSphere(this.transform.position, radio, enemyLayer);
                foreach (Collider enemy in enemiesHit)
                {
                    enemy.GetComponent<Enemigos>().TakeDamage(dano);
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
            enemiesHit = Physics.OverlapSphere(this.transform.position, radio, enemyLayer);
            foreach (Collider enemy in enemiesHit)
            {
                enemy.GetComponent<Enemigos>().TakeDamage(dano);
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
            SetBasicAttack(1f);
            enemiesHit = Physics.OverlapSphere(this.transform.position, radio, enemyLayer);
            foreach (Collider enemy in enemiesHit)
            {
                enemy.GetComponent<Enemigos>().TakeDamage(dano);
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
        anim.SetBool("Attack", false);
        StartCoroutine(corBasicAtt());
        nBut = 0;
    }
}
