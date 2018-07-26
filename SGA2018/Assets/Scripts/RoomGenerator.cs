//**********************************************************************************************
//                                                 MapGenerator.cs
//
// Author(s): Tanguy CAVAGNA
// Creation date: July 2018
// Last modification date: July 25, 2018
// Subject: Map generator for the Jam of the SGA,
//***********************************************************************************************

//----------------------------------------------
// Basics imports
//----------------------------------------------
using UnityEngine;
using System.Collections;
using System;

//----------------------------------------------
// Add imports
//----------------------------------------------
using UnityEngine.Tilemaps;

//----------------------------------------------
// Class
//----------------------------------------------
public class RoomGenerator : MonoBehaviour
{
    //++++++++++++++++++++++++++++++++++++++++++++++
    // Variables
    //++++++++++++++++++++++++++++++++++++++++++++++
    private Tile highlightTile;
    public Tile wall;
    public Tile wallDark;
    public Tile wall2;
    public Tile wall3;
    public Tile ground;
    public Tile groundDark;
    public Tile stone;
    public Tile longGrass;
    public Tile wood;
    public Tilemap highlightMap;
    public Tilemap groundMap;

    public int width = 48;
    public int height = 24;

    public string seed;
    public bool useRandomSeed;

    [HideInInspector]
    public int roomOffsetX = 0;

    public int roomOffsetY = 0;

    [Range(0, 100)]
    public int randomFillPercent;

    int[,] map;

    //++++++++++++++++++++++++++++++++++++++++++++++
    // Methodes
    //++++++++++++++++++++++++++++++++++++++++++++++

    /// <summary>
    /// Methode for generate the room
    /// </summary>
    public void GenerateRoom(int roomOffsetX, int roomOffsetY)
    {
        int wallnbr = 0;
        int groundNbr = 0;
        int rngWall = UnityEngine.Random.Range(3, 5);
        int rngGround1 = UnityEngine.Random.Range(20, 25);
        int rngGround2 = UnityEngine.Random.Range(20, 30);
        int rngGround3 = UnityEngine.Random.Range(45, 50);

        map = new int[width, height];
        RandomFillRoom();

        for (int i = 0; i < 5; i++)
        {
            SmoothRoom();
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

                        if (x > 0 && y - 1 != -1)
                        {
                            if (map[x, y - 1] == 1 && map[x, y] == 0)
                            {
                                highlightTile = wallDark;
                            }

                            if (y - 2 != -1)
                            {
                                if (map[x, y - 2] == 1 && map[x, y] == 0 && map[x, y - 1] == 0)
                                {
                                    highlightTile = groundDark;
                                }
                            }
                        }

                        if (x > 0 && map[x, y - 1] != 1 && map[x, y - 2] != 1)
                        {
                            int rng = UnityEngine.Random.Range(1, 4);
                            switch (rng)
                            {
                                case 1:
                                if (groundNbr % rngGround1 == 0)
                                {
                                    highlightTile = stone;
                                    rngGround1 = UnityEngine.Random.Range(45, 50);
                                }
                                    break;

                                case 2:
                                if (groundNbr % rngGround2 == 0)
                                {
                                    highlightTile = longGrass;
                                    rngGround2 = UnityEngine.Random.Range(45, 50);
                                }
                                    break;

                                case 3:
                                if (groundNbr % rngGround3 == 0)
                                {
                                    highlightTile = wood;
                                    rngGround3 = UnityEngine.Random.Range(45, 50);
                                }
                                    break;
                            }

                        }

                        groundMap.SetTile(new Vector3Int(-width / 2 + x + roomOffsetX, -(height / 2 + y + roomOffsetY), 0), highlightTile);
                        groundNbr++;
                    }
                    else
                    {
                        //if (wallnbr == rngWall)
                        //{
                        //    highlightTile = (UnityEngine.Random.Range(0, 10) == 7) ? wall3 : wall2;
                        //    wallnbr = 0;
                        //    rngWall = UnityEngine.Random.Range(3, 5);
                        //}

                        highlightMap.SetTile(new Vector3Int(-width / 2 + x + roomOffsetX, -(height / 2 + y + roomOffsetY), 0), highlightTile);
                        wallnbr++;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Fill the room with walls
    /// </summary>
    void RandomFillRoom()
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

    /// <summary>
    /// Smooth the room for having smooth curves with walls
    /// </summary>
    void SmoothRoom()
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

    /// <summary>
    /// Get the count of walls surrounding a certain other wall
    /// </summary>
    /// <param name="gridX">Width of room</param>
    /// <param name="gridY">Height of room</param>
    /// <returns></returns>
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