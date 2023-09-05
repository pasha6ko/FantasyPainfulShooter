using System;
using System.Collections;
using UnityEngine;
public class Gun : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GunSettings settings;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private int allAmmoCount;
    [SerializeField] private int magazin;
    [SerializeField] private bool trigger, aiming, locked;// Make ful private after all
    private Coroutine _firePricess;


    void Start()
    {
        OnEnable();
    }
    private void OnEnable()
    {
        trigger = false;
        gunAnimator.SetFloat("FireSpeed", settings.fireSpeed);
    }

    public void StartGunReload()
    {
        locked = true;
        gunAnimator.SetTrigger("Reload");
    }
    public void ReloadGun()
    {
        magazin = Mathf.Clamp(settings.magazineLimit, 0, allAmmoCount);
        allAmmoCount -= magazin;
        locked = false;
    }
    public void GunAim(bool value)
    {
        aiming = value;
        gunAnimator.SetBool("Aim", aiming);
    }

    public void GunFire(bool value)
    {
        trigger = value;

        if (!trigger) return;
        if (_firePricess != null) return;
        if (locked) return;
        _firePricess = StartCoroutine(Fire());
    }
    private void BulletFire()
    {
        Ray ray = new Ray (mainCamera.position, mainCamera.forward);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit)) return;
        Debug.DrawLine(mainCamera.position, hit.point, Color.red, 10f);
        IDamageble hp = hit.transform.GetComponent<IDamageble>();
        if (hp == null) return;
        hp.TakeDamage(settings.damage);
    }
    IEnumerator Fire()
    {
        gunAnimator.SetBool("Fire", true);
        while (magazin > 0 && !locked)
        {
            if (!trigger) break;
            BulletFire();
            magazin--;
            yield return new WaitForSeconds(1f / settings.fireSpeed);
        }
        _firePricess = null;
        gunAnimator.SetBool("Fire", false);
    }
}

[System.Serializable]
public class GunSettings
{
    public string name;
    public GameObject bulletPrefab;
    [Range(0, 300f)] public float fireSpeed, bulletSpeed, coolingTime, reloadTime, switchTime, damage, fireRange, recoilAngle, recoilSpeed;
    public int magazineLimit;
}
