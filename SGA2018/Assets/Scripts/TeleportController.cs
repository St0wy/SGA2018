using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{

    public GameObject TopTeleport;
    public GameObject DownTeleport;
    public GameObject LeftTeleport;
    public GameObject RightTeleport;
    public Camera cam;
    public FloorGenerator floor;

    private int roomX = 6;
    private int roomY = 6;

    private const int OFFSET_X = 30 + 3;
    private const int OFFSET_Y = 18 + 3;

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
            transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z);
            roomY--;
        }

        if (col.gameObject == DownTeleport && floor.CheckRoomDown(floor.floor, roomX, roomY))
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - OFFSET_Y, cam.transform.position.z);
            transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z);
            roomY++;
        }

        if (col.gameObject == LeftTeleport && floor.CheckRoomLeft(floor.floor, roomX, roomY))
        {
            cam.transform.position = new Vector3(cam.transform.position.x - OFFSET_X, cam.transform.position.y, cam.transform.position.z);
            transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z);
            roomX--;
        }

        if (col.gameObject == RightTeleport && floor.CheckRoomRight(floor.floor, roomX, roomY))
        {
            cam.transform.position = new Vector3(cam.transform.position.x + OFFSET_X, cam.transform.position.y, cam.transform.position.z);
            transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z);
            roomX++;
        }
    }
}
