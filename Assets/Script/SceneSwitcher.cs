using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static int currentScene;

    public void openScene(int index)
    {
        currentScene = index;
        SceneManager.LoadScene(index);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
