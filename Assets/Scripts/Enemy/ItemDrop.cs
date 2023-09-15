using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsPrefabs = new List<GameObject>();

    [SerializeField] private int level1, level2;

    private void FixedUpdate()
    {
    }
    public void SpawnItem(int value)
    {
        GameObject itemToSpawn = ChooseItemByLevel(value);

        if (Random.Range(0, 2) == 0) return;

        GameObject clone = Instantiate(itemToSpawn,transform.position + Vector3.up, Quaternion.Euler(0,0,0));
    }

    private GameObject ChooseItemByLevel(int value)
    {
        int index;
        if (value < level1) index = 0;
        else if (value >= level1 && value <= level2) index = 1;
        else index = 2;

        return objectsPrefabs[Random.Range(0, index + 1)];
    }

}
