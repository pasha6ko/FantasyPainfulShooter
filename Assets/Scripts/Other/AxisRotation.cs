using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisRotation : MonoBehaviour
{
    [SerializeField] private Vector3 direction ;
    [SerializeField] private float angle;
    void Update()
    {
        transform.Rotate(direction, angle * Time.deltaTime);
    }
}
