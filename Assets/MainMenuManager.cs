using KetosGames.SceneTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneLoader.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
