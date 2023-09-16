using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : Hp
{
    [SerializeField] private Slider slider;
    [SerializeField] private ItemDrop itemDrop;
    [SerializeField] private EXPReward reward;
    [SerializeField] private int level;

    protected override void Start()
    {
        base.Start();
        hp.SetLevel(level);
        hp.MaximizeValue();
    }

    private void FixedUpdate()
    {
        if (slider == null) return;
        slider.value = hp.currentValue / hp.maxValue;
    }

    public override void Die()
    {
        reward.GetEXP();
        if (itemDrop != null)
            itemDrop.SpawnItem(level);
        base.Die();
    }

}
