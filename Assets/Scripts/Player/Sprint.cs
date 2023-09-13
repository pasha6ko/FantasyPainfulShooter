using UnityEngine;
using UnityEngine.InputSystem;

public class Sprint : StaminaSystem
{
    [Header("Sprint Settings")]
    [SerializeField] private float speed;

    private float _speedMultiplier = 0;

    private void Start()
    {
        _lockeAutoRecovery = false;

        maxValue = maxStamina;
        currentValue = maxValue;
    }

    private void Update()
    {
        UpdateUI();
        if (currentValue <= 0)
        {
            StartCoroutine(FullStaminaRecovery());
            return;
        }
        if (_lockeAutoRecovery) return;
        if (_speedMultiplier > 0)
        {
            currentValue -= Time.deltaTime * maxValue / timeToFullLose;
            return;
        }
        if (_speedMultiplier <= 0)
        {
            currentValue += Time.deltaTime * maxValue / timeToFullLose;
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
