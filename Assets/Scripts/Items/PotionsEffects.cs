using System.Collections;
using UnityEngine;

public class PotionsEffects : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerHp _playerHp;
    private GunSettings _gunSettings;

    private float _time = 20f;
    private bool _isUsedDamaged, _isUsedShield, _isUsedSpeed = false;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerHp = GetComponent<PlayerHp>();
        //_gunSettings = GetComponent<GunInteraction>().currentGun.settings;
    }

    public IEnumerator Damage(float value)
    {
        if (_isUsedDamaged) yield break;
        _isUsedDamaged = true;

        int damage = _gunSettings.damage;
        _gunSettings.damage = Mathf.RoundToInt(_gunSettings.damage * value);

        yield return new WaitForSeconds(_time);

        _gunSettings.damage = damage;
        _isUsedDamaged = false;
    }

    public void Heal(int value) => _playerHp.Heal(value);

    public IEnumerator MaxArmor(int value)
    {
        if (_isUsedShield) yield break;
        _isUsedShield = true;

        _playerHp.armor.maxValue *= 2;
        _playerHp.armor.currentValue = _playerHp.armor.maxValue;
        _playerHp.UpdateArmor();

        yield return new WaitForSeconds(_time);

        _playerHp.armor.maxValue /= 2;
        _playerHp.armor.currentValue = _playerHp.armor.maxValue;
        _playerHp.UpdateArmor();

        _isUsedShield = false;
    }

    public IEnumerator Speed(float value)
    {
        if (_isUsedSpeed) yield break;
        _isUsedSpeed = true;

        float speed = _playerMovement.speed;
        _playerMovement.speed *= value;

        yield return new WaitForSeconds(_time);

        _playerMovement.speed = speed;
        _isUsedSpeed = false;
    }
}
