using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPUController1 : MonoBehaviour {

    //Method 1 - Move in a random direction every second

    private float speed;
    public Text countText;
    private Rigidbody rb;

    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        speed = 6f;

        SetCountText();
        InvokeRepeating("MoveRandomDirection", 0f, 1f);
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

    //adds force in a random direction, called each second
    public void MoveRandomDirection()
    {
        Vector3[] directions = new Vector3[] { Vector3.forward, Vector3.back, Vector3.left, Vector3.right};
        Vector3 randomDirection = directions[UnityEngine.Random.Range(0, 3)];
        rb.AddForce(randomDirection * speed * 20);
    }
}
