using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IPickable
{
    public void PickUp(Transform player);
}

public class Item : MonoBehaviour
{
    public string name;
    public Image image;
    public GameObject itemPrefab;

}
