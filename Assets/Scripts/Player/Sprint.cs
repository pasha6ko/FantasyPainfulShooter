using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Sprint : MonoBehaviour
{
    public ValueSystem stamina = new ValueSystem();

    [Header("Player Components")]
    [SerializeField] private PlayerMovement movement;

    [Header("UI Componnets")]
    [SerializeField] private Slider left;
    [SerializeField] private Slider right;

    [Header("Stamina Settings")]
    [SerializeField] private float maxStamina;
    [SerializeField] private float timeToFullLose, timeToFullRecovey;
    [SerializeField] private float speed;
    
    private float _speedMultiplier;

    protected bool _lockeAutoRecovery;

    protected IEnumerator FullStaminaRecovery()
    {
        _lockeAutoRecovery = true;
        movement.SetSprintSpeed(1);
        while (stamina.currentValue < stamina.maxValue)
        {
            stamina.currentValue += Time.deltaTime * stamina.maxValue / timeToFullRecovey;
            yield return null;
        }
        _lockeAutoRecovery = false;
    }

    protected void UpdateUI()
    {
        left.value = stamina.currentValue / stamina.maxValue;
        right.value = stamina.currentValue / stamina.maxValue;
    }

    private void Start()
    {
        _lockeAutoRecovery = false;
    }

    private void Update()
    {
        UpdateUI();
        if (stamina.currentValue <= 0)
        {
            
            return;
        }
        if (_lockeAutoRecovery) return;
        if (_speedMultiplier > 0)
        {
            stamina.currentValue -= Time.deltaTime * stamina.maxValue / timeToFullLose;
            return;
        }
        if (_speedMultiplier <= 0)
        {
            stamina.currentValue += Time.deltaTime * stamina.maxValue / timeToFullLose;
            return;
        }
    }

    public void OnSprint(InputValue value)
    {
        _speedMultiplier = value.Get<float>();
        if (!_lockeAutoRecovery)
        {
            movement.SetSprintSpeed(Mathf.Clamp(speed * _speedMultiplier, 1, float.MaxValue));
            return;
        }
        movement.SetSprintSpeed(1);
    }
}
