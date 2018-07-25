using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreation : MonoBehaviour
{
    private readonly int floorMaxSize = 12;
    public int[,] floor = new int[12, 12];

    // Use this for initialization
    void Start()
    {
        CreateFloor(ref floor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < floor.GetLength(0); i++)
        {
            for (int j = 0; j < floor.GetLength(1); j++)
            {
                if (floor[i, j] == 1)
                {
                    Gizmos.DrawCube(new Vector3(i, j), new Vector3(1, 1));
                }
            }
        }
    }   

    public void CreateFloor(ref int[,] floor)
    {
        int numberOfRoom = 10;
        EmptyMatrix(floor);
        floor[6, 6] = 1;

        for (int i = 0; i < numberOfRoom; i++)
        {
            int x = Random.Range(0, floorMaxSize - 1);
            int y = Random.Range(0, floorMaxSize - 1);

            if (floor[x, y] != 1 && !IsAlone(floor, x, y))
            {
                floor[x, y] = 1;
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
