using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPUController3 : MonoBehaviour
{

    //Method 3 - Map all route and use TSP algorithm to find shortest path that goes through all nodes
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
        speed = 6f;
        GameObject closest = FindClosestPickup();
        float xMax = speed * Time.deltaTime;
        if (FindClosestPickup() != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, closest.transform.position, xMax);
        }
        //Alternative
        /* closestPickup = null;
        MSPP();
        if (closestPickup != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, closestPickup.position, speed * Time.deltaTime);
        } */
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

    //finds position of closest object
    public GameObject FindClosestPickup()
    {
        GameObject[] pickups;
        pickups = GameObject.FindGameObjectsWithTag("Pick-Up");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject pickup in pickups)
        {
            Vector3 diff = pickup.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = pickup;
                distance = curDistance;
            }
        }
        return closest;
    }

    //Alternative Method using Dijkstra's Algorithm
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
        float totaldistance = 0;

        foreach (Node pickup in unvisitedPickups.ToArray())
        {
            if (Vector3.Distance(transform.position, pickup.position) < distance)
            {
                closestPickup = pickup;
                distance = Vector3.Distance(transform.position, closestPickup.position);
                visitedPickups.Add(pickup);
                unvisitedPickups.Remove(pickup);
                paths.Add(pickup);
                totaldistance += distance;
            }
        }
    }

    public int GetCount()
    {
        return count;
    }
}
