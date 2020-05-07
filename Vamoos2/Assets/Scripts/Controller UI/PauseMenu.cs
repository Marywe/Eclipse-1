using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

//Clase del menu de pausa dentro del juego, mediante la cual el jugador podrá pausar la sesion de juego, así como poder salir del juego.
//Tambien es la clase encargada del apartado visual del canvas, ya sea el HUD de los personajes, el propio menu de pausa y la pantalla de GameOver
public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject PauseMenuUI;
    [SerializeField]
    private GameObject GameHUD;
    [SerializeField]
    GameObject GameoverUI;

    public bool GamePaused = false;
    [SerializeField]
    private Linea l;

    //Selectores para el control del Mando.
    public GameObject SelecPauseMenu;
    public GameObject SelecGAMEOVER;

    //Funcion para el uso de la tecla Escape para abrir el menu
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 9"))
        {           
            if (GamePaused)
            {
                ContinueGame();
            }
            else
            { 
                Pause();
            }

        }

        if (l.colisiones >= l.vidaMax)
        {
            EndGame();
        }

    }

    /// <summary>
    /// Mediante las funciones siguientes controlamos el menu de pausa, pudiendo para el juego, reanudarlo o salir. 
    /// Así mismo, activamos o desactivamos GameObjects encargados del apartado visual.
    /// </summary>
    public void ContinueGame()
    {
        GameoverUI.SetActive(false);
        GameHUD.SetActive(true);
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void MenuBack(string Scene)
    {
        ContinueGame();
        Debug.Log("Cambiando a la escena " + Scene);
        SceneManager.LoadScene(Scene);
    }

    public void Pause()
    {
        GameHUD.SetActive(false);
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;

        EventSystem.current.SetSelectedGameObject(SelecPauseMenu);
    }

    public void EndGame()
    {
        GameoverUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        Debug.Log("GAME OVER TOLAI");
        

        EventSystem.current.SetSelectedGameObject(SelecGAMEOVER);
    }











}
