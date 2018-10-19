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

    GameObject closestPickup;

    void Start()
    {
        count = 0;

        SetCountText();
    }

    void Update()
    {
        closestPickup = null;
        speed = 6f;
        MSPP();
        //MapShortestPickupPath();
        if (closestPickup != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, closestPickup.transform.position, speed * Time.deltaTime);
        }
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

    void MapShortestPickupPath()
    {
        //Mark all nodes unvisited + create unvisited set
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pick-Up");
        Vector3[] pickupPositions = new Vector3[pickups.Length];
        for (int i = 0; i < pickups.Length; i++)
        {
            pickupPositions[i] = pickups[i].transform.position;
        }
        List<Vector3> unvisited = new List<Vector3>() { transform.position };
        foreach (Vector3 pickupPosition in pickupPositions)
        {
            unvisited.Add(pickupPosition);
        }

        //Assign node distances + set start node as current node
        Vector3 current = unvisited[0];
        unvisited.RemoveAt(0);

        List<float> distances = new List<float>();
        foreach (Vector3 node in unvisited)
        {
            distances.Add(Vector3.Distance(current, node));
        }

        //If distance between 2 nodes is smaller than previous distance, with initial distance being infinity, then distance should be smaller distance
        float closestDistance = Mathf.Infinity;
        foreach (float distance in distances)
        {
            if (distance < closestDistance)
            {
                closestDistance = distance;
            }
        }
        foreach (GameObject pickup in pickups)
        {
            if (Vector3.Distance(current, pickup.transform.position) == closestDistance)
            {
                closestPickup = pickup;
            }
        }
    }

    void MSPP()
    {    
        List<GameObject> pickups = new List<GameObject>((GameObject[])GameObject.FindGameObjectsWithTag("Pick-Up"));
        float distance = Mathf.Infinity;
        float totalDistance = 0;
        List<GameObject> unvisitedPickups = pickups;
            
        List<Vector3> visitedPickups = new List<Vector3>();
        visitedPickups.Add(transform.position);
        foreach (GameObject pickup in unvisitedPickups.ToArray())
        {
            if (Vector3.Distance(transform.position, pickup.transform.position) < distance)
            {
                closestPickup = pickup;
                distance = Vector3.Distance(transform.position, closestPickup.transform.position);
                visitedPickups.Add(closestPickup.transform.position);
                unvisitedPickups.Remove(pickup);
            }
        }
    }
}
