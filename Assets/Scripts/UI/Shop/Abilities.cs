using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    [HideInInspector] public Dictionary<GameObject, int> abilitiesCount = new Dictionary<GameObject, int>();

    [Header("Player Components")]
    [SerializeField] public PlayerHp playerHp;
    [SerializeField] public List<GunSettings> gunSettingsList;
    [SerializeField] public EXPSystem expSystem;

    [Header("Items Closure Components")]
    [SerializeField] public List<GameObject> closuresList;

    private void Start()
    {
        foreach (GameObject closure in closuresList)
        {
            closure.SetActive(false);
            abilitiesCount[closure] = 0;
        }
    }
}
