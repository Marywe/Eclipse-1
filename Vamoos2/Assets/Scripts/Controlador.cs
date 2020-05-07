using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Controlador de los estados generales del juego, como la camara, posicion de los personajes, datos sobre la puntuacion de los jugadores.
//Comunicacion con el resto de scripts.
public class Controlador : MonoBehaviour
{
    [SerializeField]
    private Text textoChips;
    public int chips = 0;
    public bool puertaFinalAbierta = false;
    public Transform cam;
    public GameObject[] ptoscamara;

    [SerializeField] Animator doorAnim;
    public GameObject LucesSalaPreBoss;
    public int currentNumEnems;

    public Transform objetivo1, objetivo2;

    [Header("Enemigos")]
    public GameObject prefabMariposa;
    public GameObject prefabRobot;
    public GameObject prefabEscorpion;
    public GameObject prefabPrisma;
    public GameObject boss;
    //Mediante una enumeracion controlamos en que sala se encuentra el jugador.
    public enum DondeEstas { s1, s2, sAnteBoss, pasillo, sLab, LabDer, LabIzq, preAscensor, debajopreAscensor, Ascensor, sArriba, sBossRush, sBoss}
    public DondeEstas dondeEstas = new DondeEstas();

    public enum OrientacionPuerta { NORTE, OESTE, SUR, ESTE }
    public OrientacionPuerta orientacion = new OrientacionPuerta();

    public static Controlador instance
    {
        get;
        private set;
    }
    //Comprobamos que no haya otros controladores.
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;

        }


        currentNumEnems = 0;
    } //Comprobación Singleton

    /*public void AddPunt(int puntos)
    {
        puntuacion += puntos;
        textoPuntuacion.text = puntuacion.ToString();
    }*/

    //Desde aqui se actualiza la cantidad de chips que le jugador recoja, cuando llegue a 3 podra ir a la sala final.
    //Ademas la camara temporalmente mostrará donde se encuentra esta sala y despues de unos segundos volverá a su posicion incial, junto a los personajes.
    public void AddChips()
    {
        ++chips;
        textoChips.text = chips.ToString();

        if (chips == 4)
        {
            objetivo1.gameObject.GetComponent<CharacterController>().enabled = false;
            objetivo2.gameObject.GetComponent<CharacterController>().enabled = false;

            objetivo1.GetComponent<Azul>().tp = true;
            objetivo2.GetComponent<Rosa>().tp = true;


            puertaFinalAbierta = true;

            LucesSalaPreBoss.SetActive(true);
            //Animación cámara
            int currentRoom = (int)dondeEstas;
            ptoscamara[currentRoom].SetActive(false);
            ptoscamara[13].SetActive(true);
            
            
            //Animación puerta
            Invoke("AbrirPuerta", 2);
            //Camera back
            StartCoroutine(VolverCam(currentRoom));

        }

        
    }

    
    private void AbrirPuerta() //Activacion de eventos Animados
    {
        doorAnim.SetBool("Open", puertaFinalAbierta);
    }
    
    private IEnumerator VolverCam(int currentRoom) //Control de la camara durante el desbloqueo de la sala final
    {
        yield return new WaitForSeconds(5);

        ptoscamara[currentRoom].SetActive(true);
        ptoscamara[13].SetActive(false);
        LucesSalaPreBoss.SetActive(false);

        yield return new WaitForSeconds(1);

        objetivo1.gameObject.GetComponent<CharacterController>().enabled = true;
        objetivo2.gameObject.GetComponent<CharacterController>().enabled = true;
        StartCoroutine(objetivo1.GetComponent<Azul>().TP());
        StartCoroutine(objetivo2.GetComponent<Rosa>().TP());
    }


}
