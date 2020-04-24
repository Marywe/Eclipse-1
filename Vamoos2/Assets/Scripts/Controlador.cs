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

    [SerializeField] Transform ptoCamPuerta;
    [SerializeField] Animator doorAnim;
    public int currentNumEnems;

    public Transform objetivo1, objetivo2;

    [Header("Enemigos")]
    public GameObject prefabMariposa;
    public GameObject prefabRobot;
    public GameObject prefabEscorpion;
    public GameObject prefabPrisma;

    //Mediante una enumeracion controlamos en que sala se encuentra el jugador.
    public enum DondeEstas { s1, s2, sAnteBoss, s3, s4, s5, s6, s7, s8, sLab,  sBoss}
    public DondeEstas dondeEstas = new DondeEstas();

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
        if (chips == 3)
        {
            Time.timeScale = 0;
            puertaFinalAbierta = true;

            //Animación cámara
            Transform temp = cam.transform;
            cam.transform.position = Vector3.Lerp(cam.transform.position, ptoCamPuerta.position, 3 * Time.deltaTime);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, ptoCamPuerta.rotation, 3 * Time.deltaTime);
            //Animación puerta
            Invoke("AbrirPuerta", 3);
            //Camera back
            Invoke("VolverCam", 5);


        }

        ++chips;
        textoChips.text = chips.ToString();
    }

    //Activacion de eventos Animados
    private void AbrirPuerta()
    {
        doorAnim.SetBool("Open", puertaFinalAbierta);
    }
    //Control de la camara durante el desbloqueo de la sala final
    private void VolverCam(Transform t)
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, t.position, 3 * Time.deltaTime);
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, t.rotation, 3 * Time.deltaTime);

        Time.timeScale = 1f;
    }


}
