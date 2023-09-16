using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerZone : MonoBehaviour
{
    [SerializeField] private EXPSystem player;
    [SerializeField] private List<GameObject> m_SpawnList = new();
    [SerializeField] private Collider colider;
    [SerializeField] private Transform enemisParent;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private int enemyLimit;

    private IEnumerator Start()
    {
        while (true)
        {
            if(enemisParent.childCount< enemyLimit)
                Spawn();
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }
    private void Spawn()
    {
        print("Spawn");
        Vector3 point1 = transform.position + colider.bounds.extents;
        Vector3 point2 = transform.position - colider.bounds.extents;

        Vector3 rayStartPosition = new Vector3(Random.Range(point1.x, point2.x), transform.position.y + colider.bounds.extents.y, Random.Range(point1.z, point2.z));

        RaycastHit hit;
        if (!Physics.Raycast(rayStartPosition, rayStartPosition + Vector3.down * 10000, out hit)) ;

        Debug.DrawLine(rayStartPosition, new Vector3(rayStartPosition.x, hit.point.y, rayStartPosition.z), Color.red, 1000);

        print("spawn");

        GameObject clone = Instantiate(m_SpawnList[Random.Range(0, m_SpawnList.Count - 1)], hit.point, Quaternion.Euler(Vector3.zero),enemisParent);
        
        if (player == null) return;
        clone.GetComponent<EXPReward>().playerXP = player;
    }

}
