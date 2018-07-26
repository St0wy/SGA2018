using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System;

public class MapGenerator : MonoBehaviour
{
    private Tile highlightTile;
    public Tile wall;
    public Tile ground;
    public Tilemap highlightMap;
    public Tilemap groundMap;

    public int width;
    public int height;

    public string seed;
    public bool useRandomSeed;

    private int roomOffsetX = 0;
    private int roomOffsetY = 0;

    [Range(0, 100)]
    public int randomFillPercent;

    int[,] map;

    void Start()
    {
        GenerateMap();
        roomOffsetX += 50;
        GenerateMap();
        roomOffsetX -= 100;
        GenerateMap();
    }

    void Update()
    {
        roomOffsetX = 0;
        roomOffsetY = 0;

        if (Input.GetMouseButtonDown(0))
        {
            highlightMap.ClearAllTiles();
            GenerateMap();
            roomOffsetX += 50;
            GenerateMap();
            roomOffsetX -= 100;
            GenerateMap();
        }
    }

    void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 5; i++)
        {
            SmoothMap();
        }

        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                { 
                    highlightTile = (map[x, y] == 1) ? wall : ground;

                    if (highlightTile == ground)
                    {
                        groundMap.SetTile(new Vector3Int(-width / 2 + x + roomOffsetX, -height / 2 + y + roomOffsetY, 0), highlightTile);
                    }
                    else
                    {
                        highlightMap.SetTile(new Vector3Int(-width / 2 + x + roomOffsetX, -height / 2 + y + roomOffsetY, 0), highlightTile);
                    }

                }
            }
        }
    }


    void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = Time.realtimeSinceStartup.ToString();
        }

        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }
            }
        }
    }

    void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 4)
                    map[x, y] = 1;
                else if (neighbourWallTiles < 4)
                    map[x, y] = 0;

            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }
}