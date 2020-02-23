using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sonidos
{
    public string nombre;
    public AudioClip PistaDeAudio;

    [Range(0f, 1f)]
    public float volumen;
    //[Range(.1f, 3f)] 
    //public float pitch; 

    public bool loop;

    [HideInInspector]
    public AudioSource Fuente;

}