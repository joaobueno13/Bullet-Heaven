using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PropRandomizer : MonoBehaviour
{
    public List<GameObject> propPrefabs;

    [Header("Spawn Points with Random Offset")]
    public List<Transform> spawnPointReferences;
    public float maxRandomOffset = 3f;
    public bool randomizeY = false;
    public float minYOffset = -1f;
    public float maxYOffset = 1f;

    void Start()
    {
        SpawnPropsWithRandomOffset();
    }

    void SpawnPropsWithRandomOffset()
    {
        foreach (Transform referencePoint in spawnPointReferences)
        {
            if (propPrefabs.Count > 0)
            {
                int randPrefab = Random.Range(0, propPrefabs.Count);
                GameObject prefabToSpawn = propPrefabs[randPrefab];

                // Manter o Z do ponto de referência
                float fixedZ = referencePoint.position.z;

                Vector3 randomOffset = new Vector3(
                    Random.Range(-maxRandomOffset, maxRandomOffset),
                    randomizeY ? Random.Range(minYOffset, maxYOffset) : 0,
                    0 // Z sempre 0 para manter o Z do referencePoint
                );

                Vector3 spawnPosition = new Vector3(
                    referencePoint.position.x + randomOffset.x,
                    referencePoint.position.y + randomOffset.y,
                    fixedZ
                );

                Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            }
        }
    }
}