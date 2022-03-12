using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    [SerializeField] GameObject doorsPrefab;
    [SerializeField] Transform outerWallsWrapper;
    [SerializeField] int doorsNumber;
    [SerializeField] float gizmosOffset;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Vector3 boxCollisionReduce;

    private Transform[] outerWalls;

    void Start()
    {
        CountOuterWalls();
        GenerateDoors();
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
        List<int> indexes = new List<int>();

        for (int i = 0; i < doorsNumber; i++)
        {
            int random;
            Collider[] colliders;

            do
            {
                random = Random.Range(0, outerWalls.Length);

                BoxCollider collider = outerWalls[random].GetComponent<BoxCollider>();
                Vector3 absForward = new Vector3(Mathf.Abs(collider.transform.forward.x), Mathf.Abs(collider.transform.forward.y), Mathf.Abs(collider.transform.forward.z));
                colliders = Physics.OverlapBox(collider.bounds.center + (collider.transform.forward * gizmosOffset / 2), 
                    (collider.bounds.size + (absForward * gizmosOffset) - boxCollisionReduce) / 2, Quaternion.identity, layerMask);

            } while (indexes.Contains(random) || colliders.Length >= 2);

            Transform doorsSpawnTransform = outerWalls[random];
            Destroy(outerWalls[random].gameObject);
            Instantiate(doorsPrefab, doorsSpawnTransform.position, doorsSpawnTransform.rotation);

            indexes.Add(random);
        }
    }

    private void OnDrawGizmos()
    {
        foreach (Transform transform in outerWallsWrapper)
        {
            BoxCollider collider = transform.GetComponent<BoxCollider>();
            Gizmos.color = Color.red;
            Vector3 absForward = new Vector3(Mathf.Abs(transform.forward.x), Mathf.Abs(transform.forward.y), Mathf.Abs(transform.forward.z));
            Gizmos.DrawWireCube(collider.bounds.center + (transform.forward * gizmosOffset / 2), collider.bounds.size + (absForward * gizmosOffset) - boxCollisionReduce);
        }
    }
}
