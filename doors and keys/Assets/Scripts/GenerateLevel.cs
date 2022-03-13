using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    [Header("Doors")]
    [SerializeField] GameObject doorsPrefab;
    [SerializeField] Transform outerWallsWrapper;
    [SerializeField] int doorsNumber;
    [SerializeField] float wallColliderOffset;
    [SerializeField] LayerMask layerMaskForDoors;
    [SerializeField] Vector3 boxCollisionReduce;
    [SerializeField] Transform doorsWrapper;

    [Header("Chest")]
    [SerializeField] GameObject chestPrefab;
    [SerializeField] Transform mapCenter;
    [SerializeField] Vector3 chestSpawnAreaSize;
    [SerializeField] LayerMask correctLayerMaskForChest;
    [SerializeField] LayerMask notCorrectLayerMaskForChest;

    private Transform[] outerWalls;

    void Awake()
    {
        CountOuterWalls();
        GenerateDoors();
        SpawnChest();
    }

    private void CountOuterWalls()
    {
        outerWalls = new Transform[outerWallsWrapper.childCount];

        for (int i = 0; i < outerWalls.Length; i++)
        {
            outerWalls[i] = outerWallsWrapper.GetChild(i);
        }
    }

    private void GenerateDoors()
    {
        List<int> checkedIndexes = new List<int>();

        for (int i = 0; i < doorsNumber; i++)
        {
            int random;
            Collider[] colliders;

            do
            {
                random = Random.Range(0, outerWalls.Length);

                if (!checkedIndexes.Contains(random))
                {
                    BoxCollider collider = outerWalls[random].GetComponent<BoxCollider>();
                    Vector3 absForward = new Vector3(Mathf.Abs(collider.transform.forward.x), Mathf.Abs(collider.transform.forward.y), Mathf.Abs(collider.transform.forward.z));
                    colliders = Physics.OverlapBox(collider.bounds.center, (collider.bounds.size + (absForward * wallColliderOffset) - boxCollisionReduce) / 2, Quaternion.identity, layerMaskForDoors);

                    if (colliders.Length >= 2)
                    {
                        checkedIndexes.Add(random);
                    }
                }

            } while (checkedIndexes.Contains(random));

            Transform doorsSpawnTransform = outerWalls[random];
            Destroy(outerWalls[random].gameObject);
            GameObject doors = Instantiate(doorsPrefab, doorsSpawnTransform.position, doorsSpawnTransform.rotation);
            doors.transform.SetParent(doorsWrapper);

            checkedIndexes.Add(random);
        }
    }

    private void SpawnChest()
    {
        Vector3 spawnPosition;

        do
        {
            spawnPosition = mapCenter.position + new Vector3(Random.Range(-chestSpawnAreaSize.x / 2, chestSpawnAreaSize.x / 2), mapCenter.position.y, Random.Range(-chestSpawnAreaSize.z / 2, chestSpawnAreaSize.z / 2));

        } while (Physics.OverlapBox(spawnPosition, Vector3.Scale(chestPrefab.GetComponent<BoxCollider>().size, chestPrefab.transform.localScale), Quaternion.identity, correctLayerMaskForChest).Length <= 0
        || Physics.OverlapBox(spawnPosition, Vector3.Scale(chestPrefab.GetComponent<BoxCollider>().size, chestPrefab.transform.localScale), Quaternion.identity, notCorrectLayerMaskForChest).Length > 0);

        Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);
        Instantiate(chestPrefab, spawnPosition, randomRotation);
    }


    private void OnDrawGizmos()
    {
        foreach (Transform transform in outerWallsWrapper)
        {
            BoxCollider collider = transform.GetComponent<BoxCollider>();
            Gizmos.color = Color.red;
            Vector3 absForward = new Vector3(Mathf.Abs(transform.forward.x), Mathf.Abs(transform.forward.y), Mathf.Abs(transform.forward.z));
            Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size + (absForward * wallColliderOffset) - boxCollisionReduce);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(mapCenter.position, chestSpawnAreaSize);
    }
}
