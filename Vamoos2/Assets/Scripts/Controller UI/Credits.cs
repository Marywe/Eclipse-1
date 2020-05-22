using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public MenuController menuController;
    public PauseMenu pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BackToMenu()
    {
        menuController.CreditsToMenu();
    }

    void MenuBack()
    {
        SceneManager.LoadScene(0);
    }

}
