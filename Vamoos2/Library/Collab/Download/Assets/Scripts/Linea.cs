using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linea : MonoBehaviour
{
    [SerializeField]
    private GameObject Yoshi;
    [SerializeField]
    private GameObject Kirby;

    [SerializeField]
    private Material[] materiales;

    private LineRenderer line;
    private CapsuleCollider capsule;
    private Animator anim;

    private int colisiones = 0;
    private bool vulnerable = true;

    void Start()
    {
        
        line = this.gameObject.GetComponent<LineRenderer>();
        capsule = this.gameObject.GetComponent<CapsuleCollider>();
        anim = this.gameObject.GetComponent<Animator>();

        //capsule.radius = 1 / 2; // Width / 2 calculada en el Inspector
        capsule.center = Vector3.zero;
        capsule.direction = 2; // Z-axis para la orientación
    }

    // Update is called once per frame
    void Update()
    {
        LongitudYVainas();
        ModificarCollider();
        ContadorDeVida();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemigos" && vulnerable == true) //Cambia material
        {
            this.RecibirDano();
            Debug.Log("Colisión con línea");
            Destroy(other.gameObject);
            Controlador.instance.AddPunt(30);
            vulnerable = false;
            StartCoroutine(CorVulnerabilidad());
        }
    }

    protected IEnumerator CorVulnerabilidad()
    {

        yield return new WaitForSeconds(5);
        vulnerable = true;
    }

    private void ContadorDeVida()
    {
        if (colisiones==3)
        {
            Debug.Log("Te has comido un nabo jajaj");
            //RIP
        }
    }

    public void RecibirDano() //Para llamar a esta función desde los personajes
    {
        ++colisiones;
        line.material = materiales[colisiones];
        
    }

    private void ModificarCollider()
    {
        //Movidas del collider
        capsule.transform.position = Yoshi.transform.position + (Kirby.transform.position - Yoshi.transform.position) / 2;
        capsule.transform.LookAt(Yoshi.transform.position); // :>
        capsule.height = ((Kirby.transform.position - Yoshi.transform.position).magnitude) - 0.5f; //Módulo del vector entre ambos menos un poquete
    }

    private void LongitudYVainas()
    {
        //Pa que la cadena empieze en una unidad de distancia mayor al radio del centro del personaje
        //Sorry no sé explicarme pero me he inventado esto y ya jajjh 
        Vector3 unitario = (Kirby.transform.position - Yoshi.transform.position).normalized;
        Vector3 aPrima = unitario + Yoshi.transform.position;

        //line.SetPosition(0, Yoshi.transform.position); Esta era pa que empiece en el centro
        line.SetPosition(0, aPrima);
        line.SetPosition(1, Kirby.transform.position);
    }



}
