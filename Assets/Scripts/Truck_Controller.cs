using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck_Controller : MonoBehaviour {

    private GameObject[] boxes;
    public int spaces_x, spaces_y;
    public float x_space, y_space;
    private int cur_boxes_index = 0;
    private Vector2 init_location;

    void Start()
    {
        boxes = new GameObject[spaces_x * spaces_y];
        init_location = new Vector2(transform.position.x + 2.35f, transform.position.y + 0.5f);
    }

    public void add_box(int size_x, int size_y, GameObject box)
    {
        boxes[cur_boxes_index] = box;
        box.transform.parent = gameObject.transform;

        int x_offset = cur_boxes_index / spaces_y;
        int y_offset = cur_boxes_index % spaces_y;

        print(x_offset + " " + y_offset);
        box.transform.position = new Vector2(init_location.x - (x_offset * x_space), init_location.y - (y_offset * y_space));
        cur_boxes_index++;
    }
}
