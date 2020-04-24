using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//Junto con los demas scripts de cameras, creamos un movimiento de azote a la camara.
public class CameraShake : MonoBehaviour
{
    public Transform camTransform;
    private static float shakeDuration = 0;
    private static float shakeAmount = 0;

    private float vel;
    private Vector3 vel2 = Vector3.zero;

    Vector3 originalPos;

    //Comprobamos si tenemos una camara en escena y modificamos su posicion si asi es necesario.
    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = transform;

        }

        originalPos = camTransform.localPosition;
    }

    //Funcion relacionada con los presonajes, es llamada por el script Jugador cuando recibe daño.
    public static void ShakeOnce(float lenght, float strength)
    {
        shakeDuration = lenght;
        shakeAmount = strength;
    }

    /// <summary>
    /// Comprobacion de los temblores de la camara, hacer uso de transiciones para 
    /// mover la camara de posicion en funcion a la localizacion del jugador
    /// </summary>
    
    void Update()
    {
        
        if (shakeDuration > 0)
        {
            Vector3 newPos = originalPos + Random.insideUnitSphere * shakeAmount;

            camTransform.localPosition = Vector3.SmoothDamp(camTransform.localPosition, newPos, ref vel2, 0.05f);

            shakeDuration -= Time.deltaTime;
            shakeAmount = Mathf.SmoothDamp(shakeAmount, 0, ref vel, 0.4f);
        }
        else
        {
            camTransform.localPosition = originalPos;
        }

        switch (Controlador.instance.dondeEstas)
        {
            case Controlador.DondeEstas.s1:
                originalPos = Controlador.instance.ptoscamara[0].transform.position;
                break;
            case Controlador.DondeEstas.s2:
                originalPos = Controlador.instance.ptoscamara[1].transform.position;
                break;
            case Controlador.DondeEstas.sAnteBoss:
                originalPos = Controlador.instance.ptoscamara[2].transform.position;
                break;

        }
    }
}
