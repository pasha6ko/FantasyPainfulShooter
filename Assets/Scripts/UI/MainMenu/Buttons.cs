using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(PlayerSaveData.Instance.LoadLocation());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
