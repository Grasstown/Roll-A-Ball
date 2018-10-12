using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPUController3 : MonoBehaviour
{

    //Method 3 - Use Dijkstra's Algorithm to map where pickups are and move to closest pickup

    private float speed;
    public Text countText;

    private int count;

    void Start()
    {
        count = 0;

        SetCountText();
    }

    void Update()
    {
        speed = 1f;
        MapPickups();
    }

    //sends message to rigidbodies of objects everytime they collide with each other
    void OnTriggerEnter(Collider other)
    {
        //detects if other object is categorized as a pick-up, and removes it from the scene (still in hierarchy)
        if (other.gameObject.CompareTag("Pick-Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    //updates Count Text
    void SetCountText()
    {
        countText.text = "CPU Count: " + count.ToString();
    }

    public int GetCount()
    {
        return count;
    }

    void MapPickups()
    {
        Vector3 position = transform.position;

        List<GameObject> pickups = new List<GameObject>((GameObject[])GameObject.FindGameObjectsWithTag("Pick-Up"));
        float distance = Mathf.Infinity;
        GameObject closestPickup = null;
        List<GameObject> unvisitedPickups = pickups;
        foreach (GameObject pickup in unvisitedPickups)
        {
            if (Vector3.Distance(position, pickup.transform.position) < distance)
            {
                closestPickup = pickup;
            }
        }
    }
}
