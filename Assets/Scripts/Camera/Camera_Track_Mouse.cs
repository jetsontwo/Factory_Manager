using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Track_Mouse : MonoBehaviour {

    public float max_x, max_y, speed_mod;
    private float mouse_x_max, mouse_x_min, mouse_y_max, mouse_y_min, horiz, vert;
    private Vector3 mouse_pos;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        mouse_x_max = Screen.width - Screen.width / 10;
        mouse_x_min = Screen.width / 10;
        mouse_y_max = Screen.height - Screen.height / 8;
        mouse_y_min = Screen.height / 8;
    }
	
	// Update is called once per frame
	void Update () {
        mouse_pos = Input.mousePosition;

        if (mouse_pos.x >= mouse_x_max && transform.position.x <= max_x)
            transform.Translate(new Vector3((mouse_pos.x - mouse_x_max) * Time.deltaTime * speed_mod, 0, 0));
        else if (mouse_pos.x <= mouse_x_min && transform.position.x >= 0)
            transform.Translate(new Vector3((mouse_pos.x - mouse_x_min) * Time.deltaTime * speed_mod, 0, 0));

        if (mouse_pos.y >= mouse_y_max && transform.position.y <= 0)
            transform.Translate(new Vector3(0, (mouse_pos.y - mouse_y_max) * Time.deltaTime * speed_mod, 0));
        else if (mouse_pos.y <= mouse_y_min && transform.position.y >= max_y)
            transform.Translate(new Vector3(0, (mouse_pos.y - mouse_y_min) * Time.deltaTime * speed_mod, 0));

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize < 5) // back
            Camera.main.orthographicSize++;
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > 2) // forward
            Camera.main.orthographicSize--;

        horiz = 0;
        if (Input.GetKey(KeyCode.D))
            horiz = 1;
        else if (Input.GetKey(KeyCode.A))
            horiz = -1;

        if (horiz > 0 && transform.position.x <= max_x)
            transform.Translate(new Vector3(50 * Time.deltaTime * speed_mod, 0, 0));
        else if (horiz < 0 && transform.position.x >= 0)
            transform.Translate(new Vector3(-50 * Time.deltaTime * speed_mod, 0, 0));

        vert = 0;
        if (Input.GetKey(KeyCode.W))
            vert = 1;
        else if (Input.GetKey(KeyCode.S))
            vert = -1;

        if (vert > 0 && transform.position.y <= 0)
            transform.Translate(new Vector3(0, 50 * Time.deltaTime * speed_mod, 0));
        else if (vert < 0 && transform.position.y >= max_y)
            transform.Translate(new Vector3(0, -50 * Time.deltaTime * speed_mod, 0));


        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = Cursor.lockState == CursorLockMode.Confined ? CursorLockMode.None : CursorLockMode.Confined;
	}
}
