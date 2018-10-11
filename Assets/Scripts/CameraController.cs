using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //references Player Object in Unity (sphere)
    public GameObject player;

    //distance of camera from player
    private Vector3 offset;

	// Use this for initialization - called on the first frame
	void Start ()
    {
        //gets initial distance of camera from player
        offset = transform.position - player.transform.position;
	}
	
	// LateUpdate is called once per frame, after Update() is called
	void LateUpdate ()
    {
        //keeps camera a distance of offset away from player
		transform.position = player.transform.position + offset;
	}
}
