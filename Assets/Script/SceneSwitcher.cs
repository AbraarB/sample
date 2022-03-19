using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void openScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
