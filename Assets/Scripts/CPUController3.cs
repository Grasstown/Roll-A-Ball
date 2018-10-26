using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPUController3 : MonoBehaviour
{

    //Method 3 - Use Dijkstra's Algorithm to map where pickups are and move to closest pickup

    public class Node
    {
        public String name;
        public Vector3 position;
        public Node()
        {
            name = "";
            position = Vector3.zero;
        }

        public Node(String nm, Vector3 pos)
        {
            name = nm;
            position = pos;
        }

        public Node(Vector3 pos)
        {
            name = "";
            position = pos;
        }

        public Node(String nm)
        {
            position = Vector3.zero;
        }

        public String toString()
        {
            return ("Name: " + name + "\nPosition" + position.ToString());
        }
    }

    private float speed;
    public Text countText;

    private int count;

    Node closestPickup;

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
            transform.position = Vector3.MoveTowards(transform.position, closestPickup.position, speed * Time.deltaTime);
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

    //this is a reference method
  /*   void MapShortestPickupPath()
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
    } */

    void MSPP()
    {    
        List<GameObject> pickups = new List<GameObject>((GameObject[])GameObject.FindGameObjectsWithTag("Pick-Up"));
        float distance = Mathf.Infinity;
        
        List<Node> unvisitedPickups = new List<Node>();
            
        List<Node> visitedPickups = new List<Node>();

        foreach (GameObject pickup in pickups)
        {
            Vector3 pickupPosition = pickup.transform.position;
            Node genericNode = new Node(pickup.name, pickupPosition);
            unvisitedPickups.Add(genericNode);
        }

        List<Node> paths = new List<Node>();

        foreach (Node pickup in unvisitedPickups.ToArray())
        {
            if (Vector3.Distance(transform.position, pickup.position) < distance)
            {
                closestPickup = pickup;
                distance = Vector3.Distance(transform.position, closestPickup.position);
                visitedPickups.Add(pickup);
                unvisitedPickups.Remove(pickup);
                paths.Add(pickup);
            }
        }
        //add list of total distances and choose shortest total distance to follow
    }
}
