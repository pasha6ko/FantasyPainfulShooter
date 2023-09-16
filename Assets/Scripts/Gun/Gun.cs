using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour
{
    [Header("Gun Components")]
    [SerializeField] public GunSettings settings;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private int allAmmoCount;
    [SerializeField] private int magazin;
    [SerializeField] private bool trigger, aiming, locked;// Make ful private after all

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI bullets;

    private Coroutine _firePricess;

    public bool isLocked { get => locked; set => locked = value; }


    private void Start()
    {
        OnEnable();
    }
    private void OnEnable()
    {
        trigger = false;
        gunAnimator.SetFloat("FireSpeed", settings.fireSpeed);

        UpdateBullets();
    }

    private void OnDisable()
    {
        if (_firePricess == null) return;

        StopCoroutine(_firePricess);
        _firePricess = null;
    }

    public void StartGunReload()
    {
        if (locked) return;
        locked = true;
        gunAnimator.SetTrigger("Reload");
    }
    public void ReloadGun()
    {
        magazin = Mathf.Clamp(settings.magazineLimit, 0, allAmmoCount);
        allAmmoCount -= magazin;
        locked = false;

        UpdateBullets();
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
        _firePricess = StartCoroutine(FireProcees());
    }
    private void Fire()
    {
        Vector3 direction = mainCamera.transform.forward;
        direction =  direction + mainCamera.transform.right * Random.Range(-settings.fireRange, settings.fireRange) / 300;

        Ray ray = new Ray(mainCamera.position, direction);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, maxDistance: settings.fireDistance)) return;

        Debug.DrawLine(mainCamera.position, hit.point, Color.red, 10f);
        IDamageble hp = hit.transform.GetComponent<IDamageble>();
        if (hp == null) return;
        hp.TakeDamage(settings.damage);
    }
    private IEnumerator FireProcees()
    {
        gunAnimator.SetBool("Fire", true);
        while (magazin > 0 && !locked)
        {
            if (!trigger)
            {
                gunAnimator.SetBool("Fire", false);
                break;
            }
            if (settings.isShotGun)
            {
                for (int i = 0; i < settings.bulletsPerShoot; i++)
                {
                    Fire();
                }
            }
            else
            {
                Fire();
            }
            magazin--;
            UpdateBullets();
            yield return new WaitForSeconds(1f / settings.fireSpeed);
        }
        _firePricess = null;
        gunAnimator.SetBool("Fire", false);

    }

    private void UpdateBullets()
    {
        bullets.text = magazin.ToString();
    }
}

