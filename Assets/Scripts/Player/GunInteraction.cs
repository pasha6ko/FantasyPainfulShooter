using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunInteraction : MonoBehaviour
{   
    public Gun currentGun;
    public void OnReload()
    {
        currentGun.StartGunReload();
    }
    public void OnFire(InputValue input)
    {
        float value = input.Get<float>();
        currentGun.GunFire(value>0);
    }
    public void OnAim(InputValue input)
    {
        float value = input.Get<float>();
        currentGun.GunAim(value > 0);
    }
}
