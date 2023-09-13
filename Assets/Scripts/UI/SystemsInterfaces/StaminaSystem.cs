using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class StaminaSystem : ValueSystem
{
    [Header("Player Components")]
    [SerializeField] protected PlayerMovement movement;

    [Header("UI Componnets")]
    [SerializeField] protected Slider left;
    [SerializeField] protected Slider right;

    [Header("Stamina Settings")]
    [SerializeField] protected float maxStamina;
    [SerializeField] protected float timeToFullLose, timeToFullRecovey;

    protected bool _lockeAutoRecovery;

    protected IEnumerator FullStaminaRecovery()
    {
        _lockeAutoRecovery = true;
        movement.SetSprintSpeed(1);
        while (currentValue < maxValue)
        {
            currentValue += Time.deltaTime * maxValue / timeToFullRecovey;
            yield return null;
        }
        _lockeAutoRecovery = false;
    }

    protected void UpdateUI()
    {
        left.value = currentValue / maxValue;
        right.value = currentValue / maxValue;
    }
}
