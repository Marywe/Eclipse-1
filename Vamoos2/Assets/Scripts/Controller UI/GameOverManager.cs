using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    PauseMenu pm;


    private void Start()
    {
        pm = (PauseMenu)gameObject.GetComponent(typeof(PauseMenu));
    }

   
}
