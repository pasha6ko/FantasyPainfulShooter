using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPReward : MonoBehaviour
{
    [SerializeField] private int rewardValue;
    private EXPSystem _player;

    private void Start()
    {
         _player = GameObject.FindObjectOfType(typeof(EXPSystem)) as EXPSystem;
        print(_player.transform.name);
    }

    public void GetEXP()
    {
        _player.AddExp(rewardValue);
    }
}
