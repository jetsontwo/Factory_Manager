using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Track_Player : MonoBehaviour {

    public GameObject thing_to_track;
    public float max_x, max_y;
    public float approach_speed;
    private Vector3 next_spot;
    void Start()
    {
        float vert_size = Camera.main.orthographicSize;
        float horz_size = vert_size * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update () {

        Vector3 diff = new Vector3(transform.position.x - thing_to_track.transform.position.x, transform.position.y - thing_to_track.transform.position.y, 0);
        if (Mathf.Abs(transform.position.magnitude - thing_to_track.transform.position.magnitude) > 0.2f)
            next_spot = transform.position - (diff * approach_speed * Time.deltaTime);
        

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize < 5) // back
            Camera.main.orthographicSize++;
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > 2) // forward
            Camera.main.orthographicSize--;

        if(next_spot.x < max_x && next_spot.x > 0)
            transform.position = new Vector3(next_spot.x, transform.position.y, -10);
        if (next_spot.y > max_y && next_spot.y < 0)
            transform.position = new Vector3(transform.position.x, next_spot.y, -10);
        

    }
}
