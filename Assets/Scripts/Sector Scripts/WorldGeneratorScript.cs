using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldGeneratorScript : MonoBehaviour
{
    [SerializeField] private GameObject sectorPrefab;
    [SerializeField] private GameObject resourcePrefab;
    [SerializeField] private int width;
    [SerializeField] private int height;
    
    [Header("Perlin noise")]
    [SerializeField] private float scale;

    [SerializeField] private float randomShift;
    [SerializeField] private float randomMax;
    [SerializeField] private TerrainType hills;
    [SerializeField] private TerrainType mountains;
    [SerializeField] private TerrainType plains;
    [SerializeField] private TerrainType water;

    private void Awake()
    {
        randomShift = Random.Range(-randomMax, randomMax);
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var position = new Vector3(x, y, 0);
                var sector = Instantiate(sectorPrefab, position, Quaternion.identity);
                sector.GetComponent<SectorScript>().terrainType = DecideTerrain(x, y);

                if (DecideResources(x, y))
                {
                    Instantiate(resourcePrefab, position, Quaternion.identity);
                }
            }
        }
    }
    
    /// <summary>
    /// Returns a terrain type based on the position of a sector.
    /// Uses perlin noise
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private TerrainType DecideTerrain(float x, float y)
    {
        x *= scale;
        y *= scale;
        var perlin = Mathf.PerlinNoise(x + randomShift, y + randomShift);
        if (perlin <= 0.30)
        {
            return water;
        }

        if (perlin <= 0.60)
        {
            return plains;
        }

        if (perlin <= 0.85)
        {
            return hills;
        }

        return mountains;
    }

    /// <summary>
    /// Returns if a certain sector should have resources
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private bool DecideResources(float x, float y)
    {
        x *= scale;
        y *= scale;
        // Subtracting instead of adding to have be separate from terrain.
        var perlin = Mathf.PerlinNoise(x - randomShift, y - randomShift);
        return perlin >= 0.8;
    }
}
