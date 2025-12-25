using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject[] chunkPrefabs;
    public Transform player;
    public int chunkSize = 20;
    public int renderDistance = 1;

    [Header("Despawn")]
    public int despawnDistance = 2;
    public float cullCheckDelay = 0.5f;

    private float cullTimer;


    private Dictionary<Vector2Int, GameObject> spawnedChunks = new();
    private Vector2Int currentPlayerChunk;

    void Start()
    {
        UpdatePlayerChunk(true);
    }

    void Update()
    {
        UpdatePlayerChunk(false);

        cullTimer -= Time.deltaTime;
        if (cullTimer <= 0f)
        {
            cullTimer = cullCheckDelay;
            CullChunks();
        }
    }

    void UpdatePlayerChunk(bool force)
    {
        Vector2Int newChunk = new Vector2Int(
            Mathf.FloorToInt(player.position.x / chunkSize),
            Mathf.FloorToInt(player.position.y / chunkSize)
        );

        if (newChunk == currentPlayerChunk && !force)
            return;

        currentPlayerChunk = newChunk;
        GenerateChunksAround(currentPlayerChunk);
    }

    void GenerateChunksAround(Vector2Int center)
    {
        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int y = -renderDistance; y <= renderDistance; y++)
            {
                Vector2Int chunkPos = center + new Vector2Int(x, y);

                if (spawnedChunks.ContainsKey(chunkPos))
                    continue;

                SpawnChunk(chunkPos);
            }
        }
    }

    void SpawnChunk(Vector2Int chunkGridPos)
    {
        int rand = Random.Range(0, chunkPrefabs.Length);

        Vector3 worldPos = new Vector3(
            chunkGridPos.x * chunkSize,
            chunkGridPos.y * chunkSize,
            0
        );

        GameObject chunk = Instantiate(chunkPrefabs[rand], worldPos, Quaternion.identity);
        spawnedChunks.Add(chunkGridPos, chunk);
    }

    void CullChunks()
    {
        foreach (var chunk in spawnedChunks)
        {
            int dist = Mathf.Max(
                Mathf.Abs(chunk.Key.x - currentPlayerChunk.x),
                Mathf.Abs(chunk.Key.y - currentPlayerChunk.y)
            );

            chunk.Value.SetActive(dist <= despawnDistance);
        }
    }

}