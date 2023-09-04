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
    private int magazin;
    
    void Start()    
    {
        magazin = settings.magazineLimit;
    }

    public void ReloadGun()
    {
        //reload by animation

    }

    public void GunFire()
    {
       
    }
    float currentAngle;
    private IEnumerator Recoil(float angle, float speed)
    {
        currentAngle -= angle;
        mainCamera.transform.localRotation = Quaternion.Euler(mainCamera.transform.localRotation.eulerAngles.x + currentAngle, 0, 0);
        while (currentAngle < 0)
        {  
            currentAngle += speed * Time.deltaTime;
            currentAngle = Mathf.Min(currentAngle, 0);
            mainCamera.transform.localRotation = Quaternion.Euler(mainCamera.transform.localRotation.eulerAngles.x+ speed * Time.deltaTime, 0, 0);
            yield return null;
        }
    }


}

[System.Serializable]
public class GunSettings
{
    public string name;
    [Range(0,300f)]public float fireSpeed, bulletSpeed,bodyDamage,headDamage,coolingTime, reloadTime,switchTime,FireRange,crouchFireRange,recoilAngle,recoilSpeed;
    public int magazineLimit;
    public bool Holding, AlternativeShooting;
    private bool autoReload;
    public GameObject[] bulletHole;
}
