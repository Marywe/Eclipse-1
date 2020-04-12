using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Clase encargada del manejo de las escenas, en concreto su transicion y como el nombre indica, las funciones disponibles en el Menu del juego.
/// </summary>
public class MenuController : MonoBehaviour
{
    //public Rect SliderBrillo;
    Resolution[] Resoluciones;
    public Dropdown DropDeResoluciones;
    [SerializeField]
    private GameObject LoadingScreen;
    [SerializeField]
    private Slider loadingPorcentaje;

    public GameObject Advertencia;
    public float tiempoEsperaAdvertencia = 5;
    static bool primeraVez = true;

    //Control del Menu con Mando
    public GameObject MenuPpl;
    public GameObject MenuOpt;
    public GameObject MenuCredits;

    /// <summary>
    /// Desde el metodo Start, ademas de mostrar una imagen de advertencia, realizamos las comprobaciones para modificar la resolucion del juego, así como la
    /// posibilidad de modificar en el menú.
    /// </summary>
    void Start()
    {
     
        Invoke("FuncionAdvertencia", tiempoEsperaAdvertencia);

        #region Resoluciones
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
        #endregion
    }

    private void Update()
    {
        
    }
    /// <summary>
    /// Función encargada de cargar la escena del juego, mediante una corrutina.
    /// </summary>
    /// <param name="Scene"></param>
    public void PlayGame(string Scene)
    {
        Debug.Log("Cambiando a la escena " + Scene); //Para que en la consola de unity aparezca si se esta realizando
        StartCoroutine(OperationAsync(Scene));
        
    }
    //corrutina que creamos para la pantalla durante la carga de escenas, a modo de transicion.
    IEnumerator OperationAsync(string Scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(Scene);
        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingPorcentaje.value = progress;
            yield return null;
        }
    }
    /// <summary>
    /// Salida del juego, cierre de la aplicación
    /// </summary>
    public void Exit()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }

    /* Barra de Brillo
    private void Update()
    {
        RenderSettings.ambientLight = new Color(rbgValue, rbgValue,rbgValue, 1);
    }
    
    public void CambiarBrillo(float value)
    {
        rbgValue = value;
    void OnGUI()
    {
        rbgValue = GUI.HorizontalSlider(SliderBrillo, rbgValue, 0f, 1f);
    }
    }*/

    /// <summary>
    /// Funcion que accede a los valores de unity de las cualidades gráficas, para poder modificarlas tambien desde el menu.
    /// </summary>
    /// <param name="qualityIndex"></param>
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

    }
    
    //Junto al start, se encargada de comprobodar la resolucion de pantalla.
    public void SetResolution(int IndexResolcion)
    {
        Resolution resolution = Resoluciones[IndexResolcion];
        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
    }

    public void FuncionAdvertencia()
    {

        Advertencia.SetActive(false);
       
    }

}
