using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    
    public float speed = 6.0f;
    protected float gravity = 10;
    public Linea line_sc;
    protected bool vulnerable = true;
    [SerializeField]
    protected int distKnockback = 8000;
    [SerializeField]
    float tiempoVul = 2f;
    protected GameObject sprites;
    public float dano = 1;

    [SerializeField]
    protected Transform cam = null;

    protected Rigidbody rb;
    CharacterController c;

    protected float xAxis;
    protected float zAxis;
    private void Start()
    { 
        //DontDestroyOnLoad(this);
        c = (CharacterController)gameObject.GetComponent(typeof(CharacterController));
    }


    protected IEnumerator CorVulnerabilidad()
    {
        vulnerable = false;
        yield return new WaitForSeconds(tiempoVul);
        vulnerable = true;
    }

    protected virtual void Rotar()
    {
        sprites.transform.rotation = Quaternion.LookRotation(transform.position - cam.position);
    }

    public void Danado()
    {
        CameraShake.ShakeOnce(0.3f, 0.3f);
        line_sc.RecibirDano();
        StartCoroutine(CorVulnerabilidad());
    }

}
