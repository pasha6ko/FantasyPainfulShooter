using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour, IPickable
{
    [SerializeField] private float healValue;
    public void PickUpItem(Transform player)
    {
        PlayerHp hp = player.GetComponent<PlayerHp>();
        if (hp == null) return;
        hp.Heal(healValue);
        
    }
}
