using UnityEngine;

[CreateAssetMenu(fileName = "GunSettings", menuName = "Weapon")]
public class GunSettings : ScriptableObject
{
    public string name;
    public GameObject bulletPrefab;
    public bool isShotGun;
    [Range(0, 300f)] public float bulletsPerShoot, fireSpeed, fireDistance, reloadTime, fireRange, recoilAngle, recoilSpeed;
    public int damage;
    public int magazineLimit;
}