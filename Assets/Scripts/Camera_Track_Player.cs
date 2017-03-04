using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Track_Player : MonoBehaviour {

    public GameObject thing_to_track;
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(thing_to_track.transform.position.x, thing_to_track.transform.position.y, -10);

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize < 5) // back
            Camera.main.orthographicSize++;
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > 2) // forward
            Camera.main.orthographicSize--;
        
	}
}
