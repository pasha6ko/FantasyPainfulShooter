using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPReward : MonoBehaviour
{
    public EXPSystem playerXP;
    [SerializeField] private int rewardValue;

    public void GetEXP()
    {
        playerXP.AddExp(rewardValue);
    }
}
