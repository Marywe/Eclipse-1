using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controlador : MonoBehaviour
{
    [SerializeField]
    private Text textoChips;
    public int chips = 0;
    public bool puertaFinalAbierta = false;
    [SerializeField] Camera cam;
    [SerializeField] Transform ptoCamPuerta;
    [SerializeField] Animator doorAnim;

    public static Controlador instance
    {
        get;
        private set;
    }
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
    } //Comprobación Singleton

    /*public void AddPunt(int puntos)
    {
        puntuacion += puntos;
        textoPuntuacion.text = puntuacion.ToString();
    }*/

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

    private void AbrirPuerta()
    {
        doorAnim.SetBool("Open", puertaFinalAbierta);
    }
    private void VolverCam(Transform t)
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, t.position, 3 * Time.deltaTime);
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, t.rotation, 3 * Time.deltaTime);

        Time.timeScale = 1f;
    }


}
