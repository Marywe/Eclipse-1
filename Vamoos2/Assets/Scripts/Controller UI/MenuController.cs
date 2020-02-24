using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    private float rbgValue = 0.5f;
    public Rect SliderBrillo;

    private void Update()
    {
        RenderSettings.ambientLight = new Color(rbgValue, rbgValue,rbgValue, 1);
    }

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

    public void CambiarBrillo(float value)
    {
        rbgValue = value;
    }
    void OnGUI()
    {
        rbgValue = GUI.HorizontalSlider(SliderBrillo, rbgValue, 0f, 1f);
    }


    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

    }


}
