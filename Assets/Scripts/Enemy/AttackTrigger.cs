using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public Action attackTriggerAction;
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player")) attackTriggerAction?.Invoke();
    }
}
