using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    private float rbgValue = 0.5f;
    public Rect SliderBrillo;
    Resolution[] Resoluciones;
    public Dropdown DropDeResoluciones;
    void Start()
    {
        Resoluciones = Screen.resolutions;
        DropDeResoluciones.ClearOptions();

        List<string> opcionesResol = new List<string>();

        int ResolucionActualIndex = 0;
        for(int i = 0; i < Resoluciones.Length; ++i)
        {
            string opcionElegida = Resoluciones[i].width + " x " + Resoluciones[i].height;
            opcionesResol.Add(opcionElegida);

            if(Resoluciones[i].width == Screen.currentResolution.width && Resoluciones[i].height == Screen.currentResolution.height) {
                ResolucionActualIndex = i;
            }
        }
        DropDeResoluciones.AddOptions(opcionesResol);
        DropDeResoluciones.value = ResolucionActualIndex;
        DropDeResoluciones.RefreshShownValue();

    }
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
    
    public void SetResolution(int IndexResolcion)
    {
        Resolution resolution = Resoluciones[IndexResolcion];
        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
    }


}
