using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
//Clase que contiene todo el audio del juego, se encarga de generar los sources determinados y reproducirlos.
public class AudioManager : MonoBehaviour
{
    static AudioManager MiAudioManager;

    public int contadorAudios = 1;

    /// <summary>
    /// Mediante un audio mixer, y dos grupos vamos a controlar todos los sonidos del juego.
    /// </summary>
    [SerializeField]
    private AudioMixer mixer;
    public AudioMixerGroup MusicGroup;
    public AudioMixerGroup FXGroup;

    //Mediante Header hacemos mas sencilla la organicacion y asignacion en el inspector de los sources de sonido.
    [Header("TemaPrincipal")]
    public AudioClip PistaDeAudio;

    [Header("Personajes")]
    public AudioClip pjsMoving;

    [Header("Enemigo 1: Mariposa")]
    public AudioClip Movimiento;
    public AudioClip Muerte;
    public AudioClip Hit;

    [Header("Enemigo 2: Robot")]
    public AudioClip MovimientoR;
    public AudioClip MuerteR;
    public AudioClip HitR;
    public AudioClip AtaqueR;

    [Header("Enemigo 3: Scorpio")]
    public AudioClip MovimientoS;
    public AudioClip MuerteS;
    public AudioClip HitS;
    public AudioClip AtaqueS;

    [Header("Enemigo 4: Prisma")]
    public AudioClip MovimientoP;
    public AudioClip MuerteP;
    public AudioClip HitP;
    public AudioClip AtaqueP;

  
    [HideInInspector]
    public AudioSource FuenteMaster;
    [HideInInspector]
    public AudioSource FuenteSFX;

    // Mediante el awake logramos que el audio se reproduzca antes que nada, para asegurar que no haya componentes duplicados relacionados con el audiomanager
    void Awake()
    {
        //Como es un GameObject dedicado al audio, nos aseguramos que solo haya un audiomanager en la escena en la que nos encontremos.
        if (MiAudioManager != null && MiAudioManager != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            MiAudioManager = this;
        }
        DontDestroyOnLoad(gameObject);
        
        FuenteMaster = gameObject.AddComponent<AudioSource>() as AudioSource;
        //FuenteSFX = gameObject.AddComponent<AudioSource>() as AudioSource;
        
        FuenteMaster.outputAudioMixerGroup = MusicGroup;
        //FuenteSFX.outputAudioMixerGroup = FXGroup;

    }
    //Reproducimos el tema principal desde el comienzo de la escena.
    void Start()
    {
        ReproducirMainTheme();
    }

    #region Barras Volumen, asignamos dos sliders para la musica, una para el tema principal y otra para los efectos de sonidos, desde aqui los controlamos
    public void SetLevelMaster(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 18);
    }

    public void SetLevelSFX(float sliderValue)
    {
        mixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
    }
    #endregion

    //Crear source correspondiente así como indicar a que parametro del audio mixer corresponde, tambien si queremos que se repita.
    public void ReproducirMainTheme()
    {
        FuenteMaster.clip = MiAudioManager.PistaDeAudio;
        FuenteMaster.loop = true;
        FuenteMaster.Play();
        
    }

    #region Mariposa
    public void ReMariposa(AudioSource audioS, string s)
    {
        switch (s)
        {
            case "moving":
                audioS.clip = Movimiento;
                audioS.loop = true;
                break;
            case "hit":
                audioS.clip = Hit;
                audioS.loop = false;
                break;
            case "death":
                audioS.clip = MiAudioManager.Muerte;
                audioS.loop = false;
                break;
        }
        audioS.Play();
    }
    #endregion

    #region Robot 
    public void ReRobot(AudioSource audioS, string s)
    {
        switch (s)
        {
            case "moving":
                audioS.clip = MovimientoR;
                audioS.loop = true;
                break;
            case "hit":
                audioS.clip = HitR;
                audioS.loop = false;
                break;
            case "death":
                audioS.clip = MiAudioManager.MuerteR;
                audioS.loop = false;
                break;
            case "attack":
                audioS.clip = MiAudioManager.AtaqueR;
                audioS.loop = false;
                break;
        }
        audioS.Play();
    }

    #endregion

    #region PJS

    public void PJSAndando()
    {
        MiAudioManager.FuenteSFX.clip = MiAudioManager.pjsMoving;
        MiAudioManager.FuenteSFX.loop = true;
        MiAudioManager.FuenteSFX.Play();
    }
    #endregion

    #region Scorpio 
    public void ReScorpio()
    {
        MiAudioManager.FuenteSFX.clip = MiAudioManager.MovimientoS;
        MiAudioManager.FuenteSFX.loop = true;
        MiAudioManager.FuenteSFX.Play();
    }

    public void ReScorpioHit()
    {
        MiAudioManager.FuenteSFX.clip = MiAudioManager.HitS;
        MiAudioManager.FuenteSFX.loop = false;
        MiAudioManager.FuenteSFX.Play();
    }

    public void ReScorpioDeath()
    {
        MiAudioManager.FuenteSFX.clip = MiAudioManager.MuerteS;
        MiAudioManager.FuenteSFX.loop = false;
        MiAudioManager.FuenteSFX.Play();
    }

    public void ReScorpioAtaque()
    {
        MiAudioManager.FuenteSFX.clip = MiAudioManager.AtaqueS;
        MiAudioManager.FuenteSFX.loop = false;
        MiAudioManager.FuenteSFX.Play();
    }

    #endregion

    #region Prisma 
    public void RePrisma()
    {
        MiAudioManager.FuenteSFX.clip = MiAudioManager.MovimientoP;
        MiAudioManager.FuenteSFX.loop = true;
        MiAudioManager.FuenteSFX.Play();
    }

    public void RePrismaHit()
    {
        MiAudioManager.FuenteSFX.clip = MiAudioManager.HitP;
        MiAudioManager.FuenteSFX.loop = false;
        MiAudioManager.FuenteSFX.Play();
    }

    public void RePrismaDeath()
    {
        MiAudioManager.FuenteSFX.clip = MiAudioManager.MuerteP;
        MiAudioManager.FuenteSFX.loop = false;
        MiAudioManager.FuenteSFX.Play();
    }

    public void RePrismaAtaque()
    {
        MiAudioManager.FuenteSFX.clip = MiAudioManager.AtaqueP;
        MiAudioManager.FuenteSFX.loop = false;
        MiAudioManager.FuenteSFX.Play();
    }

    #endregion

}
