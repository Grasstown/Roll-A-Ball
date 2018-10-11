using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text winText;
    public Button quitButton;

    private Rigidbody rb;
    private int count;
    private int cpuCount;

    void Start ()
    {
        //reference to Rigidbody (physics component) on object
        rb = GetComponent<Rigidbody>();

        count = 0;

        SetCountText ();
        winText.text = "";

        quitButton.onClick.AddListener(onClick);
        quitButton.gameObject.SetActive(false);
    }

    //called every time a physics calculation is done
    void FixedUpdate ()
    {
        //gets the object's x and y coordinates
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (movement * speed);
    }

    void Update()
    {
        //get CPU Count
        cpuCount = GameObject.Find("CPU").GetComponent<CPUController1>().GetCount();
        SetCountText ();
    }

    //sends message to rigidbodies of objects everytime they collide with each other
    void OnTriggerEnter(Collider other)
    {   
        //detects if other object is categorized as a pick-up, and removes it from the scene (still in hierarchy)
        if (other.gameObject.CompareTag("Pick-Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText ();
        }
    }

    //updates Count Text
    void SetCountText ()
    {
        countText.text = "Player Count: " + count.ToString();
        if (count + cpuCount == 12)
        {
            if (count == cpuCount)
            {
                winText.text = "Draw!";
                quitButton.gameObject.SetActive(true);
            }
            if (count > cpuCount)
            {
                winText.text = "You Win!";
                quitButton.gameObject.SetActive(true);
            }
            if (count < cpuCount)
            {
                winText.text = "CPU wins!";
                quitButton.gameObject.SetActive(true);
            }
        }
    }

    //makes button quit game once clicked
    void onClick ()
    {
        Application.Quit();
    }
}
    