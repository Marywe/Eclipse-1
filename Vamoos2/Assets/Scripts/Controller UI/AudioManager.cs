using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static AudioManager MiAudioManager;

    [SerializeField]
    private AudioMixer mixer;
    public AudioMixerGroup MusicGroup;
    public AudioMixerGroup FXGroup;

    //public Sonidos[] Sonidos;
    [Header("TemaPrincipal")]
    public AudioClip PistaDeAudio;

    [Header("Enemigo 1: Mariposa")]
    public AudioClip PistaDeAudioEnemigo1;
    //public AudioClip Muerte;


    AudioSource FuenteMaster;
    AudioSource FuenteSFX;

    // Start is called before the first frame update 
    void Awake()
    {

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
    void Start()
    {
        //esto se colocará en los respectivos scripts, en lo momentos oportunos
        ReMariposa();
        ReproducirMainTheme();
    }

    void ReproducirMainTheme()
    {
        MiAudioManager.FuenteMaster.clip = MiAudioManager.PistaDeAudio;
        MiAudioManager.FuenteMaster.loop = true;
        MiAudioManager.FuenteMaster.Play();
    }

    void ReMariposa()
    {
        MiAudioManager.FuenteSFX.clip = MiAudioManager.PistaDeAudioEnemigo1;
        //MiAudioManager.FuenteSFX.clip = MiAudioManager.Muerte;
        MiAudioManager.FuenteSFX.loop = true;
        MiAudioManager.FuenteSFX.Play();
    }

    public void SetLevelMaster(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 18);
    }

    public void SetLevelSFX(float sliderValue)
    {
        mixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 18);
    }

}
