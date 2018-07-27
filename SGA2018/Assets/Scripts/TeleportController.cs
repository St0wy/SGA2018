using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Point
{

    private int _x;
    private int _y;

    public int X
    {
        get { return _x; }
    }

    public int Y
    {
        get { return _y; }
    }

    public Point(int x, int y)
    {
        this._x = x;
        this._y = y;
    }
}

public class TeleportController : MonoBehaviour
{

    private Dictionary<Point, bool> clearedRooms;
    private Dictionary<Point, GameObject> enemisList;

    public GameObject TopTeleport;
    public GameObject DownTeleport;
    public GameObject LeftTeleport;
    public GameObject RightTeleport;
    public GameObject teleportParticle;
    public Camera cam;
    public FloorGenerator floor;
    public GameObject enemy;

    public AudioClip tp;

    private int roomX = 6;
    private int roomY = 6;

    private const int OFFSET_X = 30 + 3;
    private const int OFFSET_Y = 18 + 3;

    private void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = tp;

        for (int x = 0; x < floor.floor.GetLength(0); x++)
        {
            for (int y = 0; y < floor.floor.GetLength(1); y++)
            {
                if (floor.floor[x, y] == 1)
                {
                    clearedRooms.Add(new Point(x, y), false);

                    for (int nrbE = 0; nrbE < 7; nrbE++)
                    {
                        enemisList.Add(new Point(x, y), Instantiate(enemy));
                        enemisList[new Point(x, y)].gameObject.transform.SetPositionAndRotation(new Vector2(transform.position.x - 2, transform.position.y), Quaternion.identity);
                    }
                }
            }
        }
    }

    void Update()
    {
        if (!floor.CheckRoomUp(floor.floor, roomX, roomY))
            TopTeleport.SetActive(false);
        else
            TopTeleport.SetActive(true);

        if (!floor.CheckRoomDown(floor.floor, roomX, roomY))
            DownTeleport.SetActive(false);
        else
            DownTeleport.SetActive(true);

        if (!floor.CheckRoomLeft(floor.floor, roomX, roomY))
            LeftTeleport.SetActive(false);
        else
            LeftTeleport.SetActive(true);

        if (!floor.CheckRoomRight(floor.floor, roomX, roomY))
            RightTeleport.SetActive(false);
        else
            RightTeleport.SetActive(true);
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == TopTeleport && floor.CheckRoomUp(floor.floor, roomX, roomY))
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + OFFSET_Y, cam.transform.position.z);
            transform.position = new Vector3(transform.transform.position.x, cam.transform.position.y - 4, transform.position.z);
            roomY--;
        }

        if (col.gameObject == DownTeleport && floor.CheckRoomDown(floor.floor, roomX, roomY))
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - OFFSET_Y, cam.transform.position.z);
            transform.position = new Vector3(transform.transform.position.x, cam.transform.position.y + 3, transform.position.z);
            roomY++;
        }

        if (col.gameObject == LeftTeleport && floor.CheckRoomLeft(floor.floor, roomX, roomY))
        {
            cam.transform.position = new Vector3(cam.transform.position.x - OFFSET_X, cam.transform.position.y, cam.transform.position.z);
            transform.position = new Vector3(cam.transform.position.x + 8, transform.transform.position.y, transform.position.z);
            roomX--;
        }

        if (col.gameObject == RightTeleport && floor.CheckRoomRight(floor.floor, roomX, roomY))
        {
            cam.transform.position = new Vector3(cam.transform.position.x + OFFSET_X, cam.transform.position.y, cam.transform.position.z);
            transform.position = new Vector3(cam.transform.position.x - 8, transform.transform.position.y, transform.position.z);
            roomX++;
        }

        GetComponent<AudioSource>().Play();

        foreach (var particleSystem in teleportParticle.GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.Stop();
            particleSystem.Play();
        }
    }
}
