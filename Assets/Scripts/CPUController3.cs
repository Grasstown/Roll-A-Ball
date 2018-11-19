using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPUController3 : MonoBehaviour
{

    //Method 3 - Map all route and use TSP algorithm to find shortest path that goes through all Vertexs
    public class Vertex
    {
        public String name;
        public float keyValue;
        public Vector3 position;
        
        public Vertex()
        {
            name = "";
            keyValue = Mathf.Infinity;
            position = Vector3.positiveInfinity;
        }

        public Vertex(String nm, float kv, Vector3 pos)
        {
            name = nm;
            keyValue = kv;
            position = pos;
        }

        public Vertex(float kv)
        {
            name = "";
            keyValue = kv;
            position = Vector3.positiveInfinity;
        }

        public Vertex(String nm)
        {
            name = nm;
            keyValue = Mathf.Infinity;
            position = Vector3.positiveInfinity;
        }

        public Vertex(Vector3 pos)
        {
            name = "";
            keyValue = Mathf.Infinity;
            position = pos;
        }

        public Vertex(String nm, Vector3 pos)
        {
            name = nm;
            position = pos;
            keyValue = Mathf.Infinity;
        }

        public float GetKeyValue()
        {
            return keyValue;
        }

        public void SetKeyValue(float kv)
        {
            keyValue = kv;
        }

        public Vector3 GetPosition()
        {
            return position;
        }

        public String toString()
        {
            return ("Name: " + name + "\nKey Value" + keyValue + "\nposition: " + position.ToString());
        }
    }

    public class Graph
    {
        List<Vertex> V;
        List<float> E;

        public Graph()
        {
        }

        public Graph(List<Vertex> vertices, List<float> edges)
        {
            V = vertices;
            E = edges;
        }

        public Graph(List<Vertex> vertices)
        {
            V = vertices;
        }

        public Graph(List<float> edges)
        {
            E = edges;
        }

        public List<Vertex> GetVertices()
        {
            return V;
        }
        
        public List<float> GetEdges()
        {
            return E;
        }

        public void SetVertices(List<Vertex> vertices)
        {
            V = vertices;
        }
        
        public void SetEdges(List<float> edges)
        {
            E = edges;
        }
    }

    private float speed;
    public Text countText;

    private int count;

    private Vector3 initialPosition;

    private List<Vector3> shortestRoute;

    void Start()
    {
        initialPosition = transform.position;
        count = 0;
        speed = 6f;

        SetCountText();
        FindMST();

        List<Vertex> points = FindMST();
         for(int i = 0; i < points.Count; i++)
        {
            shortestRoute.Add(points[i].GetPosition());
        }
         for(int i = 0; i < shortestRoute.Count; i++)
        {
            Vector3.MoveTowards(transform.position, shortestRoute[i], speed * Time.deltaTime);
        }
    }

    void Update()
    {
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

    public List<Vertex> FindMST()
    {
        List<Vertex> vertices = new List<Vertex>();
        List<float> edges = new List<float>();

        //generate array of pickups, then add each pickup's name and position to vertex list
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pick-Up");
        vertices.Add(new Vertex(initialPosition));
        foreach(GameObject pickup in pickups)
        {
            Vertex aVertex = new Vertex(pickup.name, pickup.transform.position);
            vertices.Add(aVertex);
        }

        //create graph of map
        Graph PickupMap = new Graph(vertices, edges);
        List<Vertex> pmVertices = PickupMap.GetVertices();
        List<float> pmEdges = PickupMap.GetEdges();

        //create empty set MSTSet
        List<Vertex> MSTSet = new List<Vertex>();

        //assign root node as current node and a key value of 0
        Vertex root = pmVertices[0];
        root.SetKeyValue(0);
        float lowestKeyValue = Mathf.Infinity;

        if(MSTSet.Count != pmVertices.Count)
        {
            for (int i = 1; i < pmVertices.Count; i++)
            {
                Vertex v = pmVertices[i];
                v.keyValue = Vector3.Distance(root.position, v.position);

                if(v.GetKeyValue() < lowestKeyValue)
                {
                    lowestKeyValue = v.GetKeyValue();
                    MSTSet.Add(v);
                    root = v;
                }
            }
        }
        
        return MSTSet;
    }
}
