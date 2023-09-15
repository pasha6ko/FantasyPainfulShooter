using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionTeleport : MonoBehaviour, IInteractble
{
    [SerializeField] private string levelName;
    public void Interact(Transform transform)
    {
        SceneManager.LoadScene(levelName);
    }
}
