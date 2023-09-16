using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public void PlayGame()
    {
        //SceneManager.LoadScene(PlayerSaveData.Instance.GetSavedLocation());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
