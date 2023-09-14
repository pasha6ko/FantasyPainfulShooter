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

    private Image _leftImage, _rightImage, _leftFillImage, _rightFillImage;

    protected bool _lockeAutoRecovery;

    private void Start()
    {
        _leftImage = left.transform.GetChild(0).GetComponent<Image>();
        _rightImage = right.transform.GetChild(0).GetComponent<Image>();

        _leftFillImage = left.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        _rightFillImage = right.transform.GetChild(1).GetChild(0).GetComponent<Image>();

        _lockeAutoRecovery = false;
    }

    private void Update()
    {
        UpdateUI();
        if (stamina.currentValue <= 0)
        {
            SetColor(0.5f);

            StartCoroutine(FullStaminaRecovery());
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

    private void SetColor(float value)
    {
        _leftFillImage.color = new Color(_leftFillImage.color.r, _leftFillImage.color.g, _leftFillImage.color.b, value);
        _rightFillImage.color = new Color(_rightFillImage.color.r, _rightFillImage.color.g, _rightFillImage.color.b, value);

        _leftImage.color = new Color(_leftImage.color.r, _leftImage.color.g, _leftImage.color.b, value);
        _rightImage.color = new Color(_rightImage.color.r, _rightImage.color.g, _rightImage.color.b, value);
    }

    protected IEnumerator FullStaminaRecovery()
    {
        _lockeAutoRecovery = true;
        movement.SetSprintSpeed(1);
        while (stamina.currentValue < stamina.maxValue)
        {
            stamina.currentValue += Time.deltaTime * stamina.maxValue / timeToFullRecovey;
            yield return null;
        }
        SetColor(1f);
        _lockeAutoRecovery = false;
    }

    protected void UpdateUI()
    {
        left.value = stamina.currentValue / stamina.maxValue;
        right.value = stamina.currentValue / stamina.maxValue;
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
