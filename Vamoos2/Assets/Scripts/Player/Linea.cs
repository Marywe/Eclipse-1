using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cadena que une a los personajes. Este script controla su comportamiento.
public class Linea : MonoBehaviour
{
    [SerializeField]
    private GameObject Mistu;
    [SerializeField]
    private GameObject Araxiel;

    private LineRenderer line;
    private CapsuleCollider capsule;
    private Animator anim;

    public int colisiones = 0;
    public int vidaMax;

    PauseMenu go;

    void Start()
    {
        Time.timeScale = 1f;                
        line = this.gameObject.GetComponent<LineRenderer>();
        capsule = this.gameObject.GetComponent<CapsuleCollider>();
        anim = this.gameObject.GetComponent<Animator>();
        go = (PauseMenu)gameObject.GetComponent(typeof(PauseMenu));

        //capsule.radius = 1 / 2; // Width / 2 calculada en el Inspector
        capsule.center = Vector3.zero;
        capsule.direction = 2; // Z-axis para la orientación
    }

    // Update is called once per frame
    void Update()
    {
        LongitudYVainas();
        ModificarCollider();


    }

    //Detectar colisiones con los enemigos
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemigos")
        {
            Debug.Log("Colisión con línea");
        }
    }

    //Para aumentar la cantida de vida de la linea
    public void ModificarVidaMax()
    {
        ++vidaMax;
    }
    //Para modificar el daño(nº de colisiones)
    public void RecibirDano() 
    {      
        ++colisiones;
        Debug.Log(colisiones);
    }

    //Modicar el collider de la linea durante los movimientos de los personajes
    private void ModificarCollider()
    {
        capsule.transform.position = Mistu.transform.position + (Araxiel.transform.position - Mistu.transform.position) / 2;
        capsule.transform.LookAt(Mistu.transform.position); // :>
        capsule.height = ((Araxiel.transform.position - Mistu.transform.position).magnitude) - 0.5f; //Módulo del vector entre ambos menos un poquete
    }

    //Calcular la longitud de la cadena entre los dos personajes
    private void LongitudYVainas()
    {
        //Pa que la cadena empieze en una unidad de distancia mayor al radio del centro del personaje
      
        Vector3 unitario = (Araxiel.transform.position - Mistu.transform.position).normalized;
        Vector3 aPrima = unitario/2 + Mistu.transform.position;
        Vector3 bPrima = Araxiel.transform.position - unitario / 2;

        //Esta es para que empiece en el centro
        line.SetPosition(0, aPrima);
        line.SetPosition(1, bPrima);
    }

    void ActualizarColores()
    {
        if (colisiones <= 2) { }
    }

}
