using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controlador : MonoBehaviour
{
    [SerializeField]
    private Text textoPuntuacion;
    [SerializeField]
    private Text textoChips;

    private int puntuacion=0;
    private int chips=0;

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

    public void AddPunt(int puntos)
    {
        puntuacion += puntos;
        textoPuntuacion.text = puntuacion.ToString();
    }

    public void AddChips()
    {
        ++chips;
        textoChips.text = chips + "";
    }



}
