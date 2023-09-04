using System;
using System.Collections;
using UnityEngine;
public class Gun : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GunSettings settings;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform muzzle;
    [SerializeField] Transform AimPoint;
    [SerializeField] private int allAmmoCount;
    [SerializeField] private int magazin;
    [SerializeField] private bool trigger, locked;// Make ful private after all
    private Coroutine _firePricess;


    void Start()
    {
        trigger = false;
        ReloadGun();
    }

    public void ReloadGun()
    {
        magazin = Mathf.Clamp(settings.magazineLimit, 0, allAmmoCount);
        allAmmoCount -= magazin;
    }

    public void GunFire(bool value)
    {
        trigger = value;
        if (!trigger) return;
        if (_firePricess != null) return;
        _firePricess = StartCoroutine(Fire());
    }
    private void SpawnBullet()
    {
        GameObject bulletClone = Instantiate(settings.bulletPrefab, muzzle.position,muzzle.rotation);
        Rigidbody bulletRb = bulletClone.transform.GetComponent<Rigidbody>();
        bulletRb.AddForce(muzzle.forward * settings.bulletSpeed, ForceMode.VelocityChange);
    }
    IEnumerator Fire()
    {
        while (magazin > 0)
        {
            if (!trigger) break;
            SpawnBullet();
            magazin--;
            yield return new WaitForSeconds(1f / settings.fireSpeed);
        }
        _firePricess = null;
    }
    float currentAngle;
    private IEnumerator Recoil(float angle, float speed)
    {
        currentAngle -= angle;
        transform.localRotation = Quaternion.Euler(mainCamera.transform.localRotation.eulerAngles.x + currentAngle, 0, 0);
        while (currentAngle < 0)
        {
            currentAngle += speed * Time.deltaTime;
            currentAngle = Mathf.Min(currentAngle, 0);
            transform.localRotation = Quaternion.Euler(mainCamera.transform.localRotation.eulerAngles.x + speed * Time.deltaTime, 0, 0);
            yield return null;
        }
    }
}

[System.Serializable]
public class GunSettings
{
    public string name;
    public GameObject bulletPrefab;
    [Range(0, 300f)] public float fireSpeed, bulletSpeed, coolingTime, reloadTime, switchTime, fireRange, recoilAngle, recoilSpeed;
    public int magazineLimit;
}
