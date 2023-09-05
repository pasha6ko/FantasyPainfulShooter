using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponPrefs;
    [SerializeField] private GunInteraction interaction;
    private GameObject _currentWeaon;
    private int _currentWeaponIndex;
    private void Start()
    {
        SetWeaponByIndex(0);
    }
    public void OnScrollWheel(InputValue value)
    {
        Vector2 wheel = value.Get<Vector2>();
        if (wheel.y == 0) return;
        SwitchWeapon(wheel.y>0?1:-1);
    }

    private void SwitchWeapon(int direction)
    {
        _currentWeaponIndex += direction;
        _currentWeaponIndex = Math.Clamp(_currentWeaponIndex, 0, weaponPrefs.Count - 1);
        SetWeaponByIndex(_currentWeaponIndex);
        
    }
    private void SetWeaponByIndex(int index)
    {
        print(index);
        if(_currentWeaon != null) _currentWeaon.SetActive(false);
        _currentWeaon = weaponPrefs[index];
        _currentWeaon.SetActive(true);
        interaction.currentGun = _currentWeaon.transform.GetComponent<Gun>();
        _currentWeaponIndex = index;
    }

}
