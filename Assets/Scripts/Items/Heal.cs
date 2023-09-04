using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour, IPickable
{
    public void PickUp(Transform player)
    {
        PlayerHp hp = player.GetComponent<PlayerHp>();
        if (hp == null) return;
        hp.Heal(20);
    }
}
