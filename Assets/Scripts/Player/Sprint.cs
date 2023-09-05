using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sprint : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private float maxStamina, stamina;
    [SerializeField] private float timeToFullLose,timeToFullRecovey;
    [SerializeField] private float speed;
    [SerializeField] private bool _lockeAutoRecovery;
    [SerializeField] private float speedMultiplier;

    private void Start()
    {
        _lockeAutoRecovery = false;
        ResetStamina();
    }

    public void OnSprint(InputValue value)
    {
        speedMultiplier = value.Get<float>();
        if (!_lockeAutoRecovery)
        {
            movement.SetSprintSpeed(Mathf.Clamp(speed * speedMultiplier, 1, float.MaxValue));
            return;
        }
        movement.SetSprintSpeed(1);
    }
    public void Update()
    {
        if (stamina <= 0)
        {
            StartCoroutine(FullStaminaRecovery());
            return;
        }
        
        if (_lockeAutoRecovery) return;
        
        if (speedMultiplier > 0)
        {
            stamina -= Time.deltaTime * maxStamina / timeToFullLose;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
            print("Sprint");
            return;
        }
        print("Lock");
        if (speedMultiplier <= 0)
        {
            stamina += Time.deltaTime * maxStamina / timeToFullLose;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
            return;
        }
       
    }
    private void ResetStamina()
    {
        stamina = maxStamina;
    }
    private IEnumerator FullStaminaRecovery()
    {
        _lockeAutoRecovery = true;
        movement.SetSprintSpeed(1);
        while (stamina < maxStamina)
        {
            stamina += Time.deltaTime * maxStamina / timeToFullRecovey;
            yield return null;
        }
        _lockeAutoRecovery = false;
    }
}
