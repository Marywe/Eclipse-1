using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
//Clase que contiene todo el audio del juego, se encarga de generar los sources determinados y reproducirlos.
public class AudioManager : MonoBehaviour
{
    static AudioManager MiAudioManager;

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

    [Header("Enemigo 1: Mariposa")]
    public static AudioClip Movimiento;
    public AudioClip Muerte;
    public AudioClip Hit;

    [Header("Enemigo 2: Robot")]
    public AudioClip MovimientoR;
    public AudioClip MuerteR;
    public AudioClip HitR;
    public AudioClip AtaqueR;

    AudioSource FuenteMaster;
    AudioSource FuenteSFX;

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
        FuenteSFX = gameObject.AddComponent<AudioSource>() as AudioSource;

        FuenteMaster.outputAudioMixerGroup = MusicGroup;
        FuenteSFX.outputAudioMixerGroup = FXGroup;

    }
    //Reproducimos el tema principal desde el comienzo de la escena.
    void Start()
    {
        //ReproducirMainTheme();


        #region Intento Audio 2
        /*/sonidos de enemigos
        do
        {
            switch (Enemigos.FindObjectsOfType<Enemigos>())
            {
                case Enemigos.Instantiate.GetComponentsInChildren<Roboto>
                    ReRobot();
                    if ()
                    {
                        ReRobotAtaque();
                    }else if ()
                    {
                        ReRobotHit();
                    }else if ()
                    {
                        ReRobotDeath();
                    }
                    break;




            }

        } while (PauseMenu.GamePaused == false) ;
        */
        #endregion

    }

    #region Barras Volumen, asignamos dos sliders para la musica, una para el tema principal y otra para los efectos de sonidos, desde aqui los controlamos
    public void SetLevelMaster(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 18);
    }

    public void SetLevelSFX(float sliderValue)
    {
        mixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 18);
    }
    #endregion

    //Como el resto de funciones a continuacion, son todas funciones que crearan los sources correspondientes cuando sea oportuno, así como 
    // que indicamos a que parametro del audio mixer corresponden, tambien si queremos que se repitan.
    public void ReproducirMainTheme()
    {
        MiAudioManager.FuenteMaster.clip = MiAudioManager.PistaDeAudio;
        MiAudioManager.FuenteMaster.loop = true;
        MiAudioManager.FuenteMaster.Play();
    }

    #region Mariposa
    public void ReMariposa()
    {
        MiAudioManager.FuenteSFX.clip = MiAudioManager.Movimiento;
        MiAudioManager.FuenteSFX.loop = true;
        MiAudioManager.FuenteSFX.Play();
    }

    public void ReMariposaHit()
    {
        MiAudioManager.FuenteSFX.clip = MiAudioManager.Hit;
        MiAudioManager.FuenteSFX.loop = false;
        MiAudioManager.FuenteSFX.Play();
    }

    public void ReMariposaDeath()
    {
        MiAudioManager.FuenteSFX.clip = MiAudioManager.Muerte;
        MiAudioManager.FuenteSFX.loop = false;
        MiAudioManager.FuenteSFX.Play();
    }
    #endregion

    #region Robot
    public void ReRobot()
    {
        MiAudioManager.FuenteSFX.clip = MiAudioManager.MovimientoR;
        MiAudioManager.FuenteSFX.loop = true;
        MiAudioManager.FuenteSFX.Play();
    }

    public void ReRobotHit()
    {
        MiAudioManager.FuenteSFX.clip = MiAudioManager.HitR;
        MiAudioManager.FuenteSFX.loop = true;
        MiAudioManager.FuenteSFX.Play();
    }

    public void ReRobotDeath()
    {
        MiAudioManager.FuenteSFX.clip = MiAudioManager.MuerteR;
        MiAudioManager.FuenteSFX.loop = false;
        MiAudioManager.FuenteSFX.Play();
    }

    public void ReRobotAtaque()
    {
        MiAudioManager.FuenteSFX.clip = MiAudioManager.AtaqueR;
        MiAudioManager.FuenteSFX.loop = true;
        MiAudioManager.FuenteSFX.Play();
    }

    #endregion

}
