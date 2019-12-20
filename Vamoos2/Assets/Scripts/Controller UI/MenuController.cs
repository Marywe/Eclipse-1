using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
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
}
