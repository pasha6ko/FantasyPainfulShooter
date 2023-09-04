using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunInteraction : MonoBehaviour
{
    [SerializeField] private Gun currentGun;
    public void OnReload()
    {
        currentGun.ReloadGun();
    }
    public void OnFire(InputValue input)
    {
        float value = input.Get<float>();
        currentGun.GunFire(value>0);
    }
}
