using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject PauseMenuUI;
    [SerializeField]
    private GameObject GameHUD;
    [SerializeField]
    GameObject GameoverUI;

    public bool GamePaused = false;


    private void Update() //Funcion para el uso de la tecla Escape para abrir el menu
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

        if (GetComponent<Linea>().colisiones >= 3)
        {
            EndGame();
        }

    }
    public void ContinueGame()
    {
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
    }

    public void EndGame()
    {
        GameoverUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        Debug.Log("GAME OVER TOLAI");
    }










}
