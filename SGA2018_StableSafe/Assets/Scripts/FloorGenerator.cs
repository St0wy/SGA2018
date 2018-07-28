using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorGenerator : MonoBehaviour
{

    [Range(9, 21)]
    [SerializeField]
    private int floorMaxSize = 12;

    public RoomGenerator room;
    public Camera cam;

    [HideInInspector]
    public int[,] floor = new int[12, 12];

    // Use this for initialization
    void Start()
    {
        CreateFloor(ref floor);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    //minimap.ClearAllTiles();
        //    room.highlightMap.ClearAllTiles();
        //    room.groundMap.ClearAllTiles();
        //    EmptyMatrix(floor);
        //    CreateFloor(ref floor);
        //}
    }

    public void CreateFloor(ref int[,] floor)
    {
        int numberOfRoom = 10;
        EmptyMatrix(floor);
        floor[6, 6] = 1;

        int i = 0;
        while (i < numberOfRoom)
        {
            int x = Random.Range(0, floorMaxSize - 1);
            int y = Random.Range(0, floorMaxSize - 1);

            if (floor[x, y] != 1 && !IsAlone(floor, x, y))
            {
                floor[x, y] = 1;
                i++;
            }
        }

        room.highlightMap.ClearAllTiles();
        ApplyRooms();
    }

    private void ApplyRooms()
    {
        int tempOffsetX = room.roomOffsetX;
        int tempOffsetY = room.roomOffsetY;

        for (int i = 0; i < floor.GetLength(0); i++)
        {
            for (int j = 0; j < floor.GetLength(1); j++)
            {
                if (floor[i, j] != 0)
                {
                    tempOffsetX = i * (room.width + 3);
                    tempOffsetY = j * (room.height + 3);

                    room.GenerateRoom(tempOffsetX, tempOffsetY);
                }
            }
        }


    }

    public void EmptyMatrix(int[,] matrix)
    {
        for (int i = 0; i < floor.GetLength(0); i++)
        {
            for (int j = 0; j < floor.GetLength(1); j++)
            {
                matrix[i, j] = 0;
            }
        }
    }

    public bool IsAlone(int[,] matrix, int x, int y)
    {
        bool isAlone = true;
        if (CheckRoomLeft(matrix, x, y) || CheckRoomRight(matrix, x, y) || CheckRoomUp(matrix, x, y) || CheckRoomDown(matrix, x, y))
        {
            isAlone = false;
        }

        return isAlone;
    }

    public bool CheckRoomLeft(int[,] matrix, int x, int y)
    {
        bool roomLeft = false;
        if (x > 0)
        {
            if (matrix[x - 1, y] == 1)
            {
                roomLeft = true;
            }
        }

        return roomLeft;
    }

    public bool CheckRoomRight(int[,] matrix, int x, int y)
    {
        bool roomLeft = false;
        if (x < floorMaxSize)
        {
            if (matrix[x + 1, y] == 1)
            {
                roomLeft = true;
            }
        }

        return roomLeft;
    }

    public bool CheckRoomUp(int[,] matrix, int x, int y)
    {
        bool roomLeft = false;
        if (y > 0)
        {
            if (matrix[x, y - 1] == 1)
            {
                roomLeft = true;
            }
        }

        return roomLeft;
    }

    public bool CheckRoomDown(int[,] matrix, int x, int y)
    {
        bool roomLeft = false;
        if (y < floorMaxSize)
        {
            if (matrix[x, y + 1] == 1)
            {
                roomLeft = true;
            }
        }

        return roomLeft;
    }
}
