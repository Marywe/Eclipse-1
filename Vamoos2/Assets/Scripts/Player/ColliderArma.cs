using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script para los ataques 
public class ColliderArma : MonoBehaviour
{
    public LayerMask enemyLayer;
    Collider[] enemiesHit;
    [SerializeField]
    private Animator anim;
    public Vector3 cubeSz;

    float lastButTime;
    public float maxComboDelay = 0.9f;
    private int nBut = 0;
    private Vector3 posicion;
    private bool puedeAtacar;
    private Rosa r;
    [SerializeField]
    private float danioAdicional = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
       
        posicion.x = 0.7f;
        posicion.y = 0;
        posicion.z = 0;
        transform.position = posicion;
        puedeAtacar = true;
        r = gameObject.GetComponentInParent<Rosa>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Dirección ataque
        float attackDirection = r.anim.GetFloat("Direction");
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
        #endregion

        if (puedeAtacar) BasicAttack();

    }

    //Para visualizar los bordes del collider
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, cubeSz);
    }

    #region Tipos de Ataque
    //ataque basico hacia un enemigo
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

                enemiesHit = Physics.OverlapBox(transform.position, cubeSz / 2, Quaternion.identity, enemyLayer);
                //enemiesHit = Physics.OverlapSphere(this.transform.position, cubeSz, enemyLayer);
                foreach (Collider enemy in enemiesHit)
                {
                    enemy.GetComponent<Enemigos>().TakeDamage(r.dano);
                }
                
            }

            nBut = Mathf.Clamp(nBut, 0, 3);
        }
        
    }
    public void FtAt()
    {
        if (nBut >= 2)
        {
            SetBasicAttack(0.5f);

            enemiesHit = Physics.OverlapBox(transform.position, cubeSz / 2, Quaternion.identity, enemyLayer);

            //enemiesHit = Physics.OverlapSphere(this.transform.position, cubeSz, enemyLayer);
            foreach (Collider enemy in enemiesHit)
            {
                enemy.GetComponent<Enemigos>().TakeDamage(r.dano);
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
            enemiesHit = Physics.OverlapBox(transform.position, cubeSz / 2, Quaternion.identity, enemyLayer);
            //  enemiesHit = Physics.OverlapSphere(this.transform.position, cubeSz, enemyLayer);
            foreach (Collider enemy in enemiesHit)
            {
                enemy.GetComponent<Enemigos>().TakeDamage(r.dano + danioAdicional);
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
    #endregion

    //Establecer la animacion correspondiente
    public void SetBasicAttack(float f)
    {
        anim.SetFloat("AttackN", f);
    }
    //Cooldown del ataque
    private IEnumerator corBasicAtt()
    {
        puedeAtacar = false;
        yield return new WaitForSeconds(r.cdbasicAttack);
        puedeAtacar = true;
    }
    
    
}
