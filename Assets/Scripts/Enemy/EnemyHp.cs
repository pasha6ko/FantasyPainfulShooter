using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : Hp
{
    [SerializeField] private Slider slider;
    [SerializeField] private ItemDrop itemDrop;
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
        if(itemDrop != null)
            itemDrop.SpawnItem(level);
        base.Die();
    }

}
