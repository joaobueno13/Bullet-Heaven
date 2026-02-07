using UnityEngine;
using System.Collections.Generic;

public class PropRandomizer : MonoBehaviour
{
    [Header("Props Regulares")]
    public List<GameObject> propPrefabs;

    [Header("Spawn Points com Deslocamento Aleatório")]
    public List<Transform> spawnPointReferences;
    public float maxRandomOffset = 3f;
    public bool randomizeY = false;
    public float minYOffset = -1f;
    public float maxYOffset = 1f;

    [Header("Breakable Props (Special)")]
    [Tooltip("Prefabs para props quebráveis que são mais raros e não podem se sobrepor")]
    public List<GameObject> breakablePropPrefabs;
    [Range(0f, 1f)]
    [Tooltip("Probabilidade (0..1) de um spawn point gerar um prop quebrável")]
    public float breakableSpawnChance = 0.1f;
    [Tooltip("Distância mínima (em unidades no plano X/Y) entre props quebráveis e entre eles e outros props já gerados")]
    public float breakableMinSpacing = 1f;
    [Tooltip("Número máximo de tentativas para encontrar uma posição válida sem sobreposição")]
    public int breakableMaxPlacementAttempts = 5;

    void Start()
    {
        SpawnPropsWithRandomOffset();
    }

    void SpawnPropsWithRandomOffset()
    {
        // Guarda posições já usadas para evitar sobreposição (compara X/Y)
        List<Vector3> placedPositions = new List<Vector3>();

        foreach (Transform referencePoint in spawnPointReferences)
        {
            if (propPrefabs == null || propPrefabs.Count == 0)
                continue;

            // decide se tentamos gerar um prop quebrável neste ponto
            bool trySpawnBreakable = breakablePropPrefabs != null
                                     && breakablePropPrefabs.Count > 0
                                     && Random.value < breakableSpawnChance;

            if (trySpawnBreakable)
            {
                bool placed = TryPlaceBreakableAt(referencePoint, placedPositions);
                if (placed)
                    continue; // se foi colocado um quebrável nesse point, pula spawn normal
                // se não conseguiu colocar quebrável (tentativas esgotadas), cai para spawn normal
            }

            // spawn normal (regular props) sem verificação extra de espaçamento
            int randPrefab = Random.Range(0, propPrefabs.Count);
            GameObject prefabToSpawn = propPrefabs[randPrefab];

            float fixedZ = referencePoint.position.z;

            Vector3 randomOffset = new Vector3(
                Random.Range(-maxRandomOffset, maxRandomOffset),
                randomizeY ? Random.Range(minYOffset, maxYOffset) : 0,
                0
            );

            Vector3 spawnPosition = new Vector3(
                referencePoint.position.x + randomOffset.x,
                referencePoint.position.y + randomOffset.y,
                fixedZ
            );

            GameObject spawnedProp = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            spawnedProp.transform.SetParent(transform);
            placedPositions.Add(spawnPosition);
        }
    }

    bool TryPlaceBreakableAt(Transform referencePoint, List<Vector3> placedPositions)
    {
        for (int attempt = 0; attempt < Mathf.Max(1, breakableMaxPlacementAttempts); attempt++)
        {
            float fixedZ = referencePoint.position.z;

            Vector3 randomOffset = new Vector3(
                Random.Range(-maxRandomOffset, maxRandomOffset),
                randomizeY ? Random.Range(minYOffset, maxYOffset) : 0,
                0
            );

            Vector3 candidatePos = new Vector3(
                referencePoint.position.x + randomOffset.x,
                referencePoint.position.y + randomOffset.y,
                fixedZ
            );

            if (!IsOverlapping(candidatePos, breakableMinSpacing, placedPositions))
            {
                int randPrefab = Random.Range(0, breakablePropPrefabs.Count);
                GameObject prefabToSpawn = breakablePropPrefabs[randPrefab];

                GameObject spawnedProp = Instantiate(prefabToSpawn, candidatePos, Quaternion.identity);
                spawnedProp.transform.SetParent(transform);
                placedPositions.Add(candidatePos);
                return true;
            }
        }

        // não conseguiu achar posição válida sem sobreposição
        return false;
    }

    bool IsOverlapping(Vector3 position, float minDistance, List<Vector3> existingPositions)
    {
        float minDistSq = minDistance * minDistance;
        foreach (var p in existingPositions)
        {
            float dx = p.x - position.x;
            float dy = p.y - position.y;
            float distSq = dx * dx + dy * dy;
            if (distSq < minDistSq)
                return true;
        }
        return false;
    }
}