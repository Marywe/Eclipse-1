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

    public Transform[] originalTr;
     Vector3[] originalPos = new Vector3[13];

    //Comprobamos si tenemos una camara en escena y modificamos su posicion si asi es necesario.
    private void Start()
    {
        for (int i = 0; i < originalTr.Length; i++)
        {
            originalPos[i] = originalTr[i].localPosition;
        }
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
        int n = (int)Controlador.instance.dondeEstas;

        if (shakeDuration > 0 && n!=9)
        {
            Vector3 newPos = originalPos[n] + Random.insideUnitSphere * shakeAmount;

            Controlador.instance.ptoscamara[n].transform.localPosition = Vector3.SmoothDamp(Controlador.instance.ptoscamara[n].transform.localPosition, newPos, ref vel2, 0.05f);

            shakeDuration -= Time.deltaTime;
            shakeAmount = Mathf.SmoothDamp(shakeAmount, 0, ref vel, 0.4f);
        }
        else if(shakeDuration <= 0 && n !=9)
        {
            Controlador.instance.ptoscamara[n].transform.localPosition = originalPos[n];
        }
    }
}
