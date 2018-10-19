using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPUController2 : MonoBehaviour
{

    //Method 2 - Find closest Pick-Up and move to it

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
        speed = 6f;
        GameObject closest = FindClosestPickup();
        float xMax = speed * Time.deltaTime;
        if (FindClosestPickup() != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, closest.transform.position, xMax);
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

    public int GetCount()
    {
        return count;
    }
}
