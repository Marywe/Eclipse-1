using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sonidos[] Sonidos;

    // Start is called before the first frame update 
    void Awake()
    {
        foreach (Sonidos s in Sonidos)
        {
            s.Fuente = gameObject.AddComponent<AudioSource>();
            s.Fuente.clip = s.PistaDeAudio;

            s.Fuente.volume = s.volumen;
            //s.Fuente.pitch = s.pitch; 
            s.Fuente.loop = s.loop;
        }
    }
    private void Start()
    {
        Reproducir("Mariposa");
    }

    public void Reproducir(string nombre)
    {
        Sonidos s = Array.Find(Sonidos, Sonidos => Sonidos.nombre == nombre);
        if (s == null)
        {
            Debug.Log("El sonido" + nombre + " no se encuentra");
            return;
        }
        s.Fuente.Play();
    }
}