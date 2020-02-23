using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public Slider BarraBrillo;
   

    public void PlayGame(string Scene)
    {
        Debug.Log("Cambiando a la escena " + Scene); //Para que en la consola de unity aparezca si se esta realizando
        SceneManager.LoadScene("Prueba");

    }

    public void Exit()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }

    public void CambiarBrillo()
    {
        RenderSettings.ambientIntensity = BarraBrillo.value;
        
    }


    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

    }


}
