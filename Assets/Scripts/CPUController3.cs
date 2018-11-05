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

    public Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        count = 0;

        SetCountText();
    }

    void Update()
    {
        speed = 6f;
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

        //calculate edges between vertices
        for(int i=0; i < vertices.Count-1; i++)
        {
            float edge = Vector3.Distance(vertices[i].position, vertices[i+1].position);
            edges.Add(edge);
        }

        //create graph of map
        Graph PickupMap = new Graph(vertices, edges);

        //assign root node as current transform
        Vertex root = new Vertex(0);
        foreach(Vertex vertex in vertices)
        {
            vertex.keyValue = Vector3.Distance(root.position, vertex.position);
        }


        List<Vertex> MSTSet = new List<Vertex>();
        
        return MSTSet;
    }
}
