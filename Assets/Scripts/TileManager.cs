using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject[] powerUpPrefabs; // Array of power-up prefabs
    private Transform playerTransform;
    private float zSpawn = 0;
    public float tileLength = 30;
    public int numberOfTiles = 5;
    public int powerUpSpawnRate = 3;
    private List<GameObject> activeTiles = new List<GameObject>();

    // Reference to PlayerController to get laneDistance
    private PlayerController playerController;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = playerTransform.GetComponent<PlayerController>();

        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    void Update()
    {
        if (playerTransform.position.z - 35 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(1, tilePrefabs.Length));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;

        // Randomly spawn a power-up
        if (Random.Range(0, 10) < powerUpSpawnRate)
        {
            int powerUpIndex = Random.Range(0, powerUpPrefabs.Length);
            int laneIndex = Random.Range(0, 3); // Randomly select a lane (0:left, 1:middle, 2:right)
            float lanePositionX = (laneIndex - 1) * playerController.laneDistance; // Calculate lane position using laneDistance
            Vector3 powerUpPosition = go.transform.position + new Vector3(lanePositionX, 1, tileLength / 2);
            Instantiate(powerUpPrefabs[powerUpIndex], powerUpPosition, Quaternion.identity);
        }
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}