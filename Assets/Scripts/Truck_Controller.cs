using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck_Controller : MonoBehaviour {

    private GameObject[] boxes;
    public int spaces_x, spaces_y;
    private int cur_boxes_index = 0;

    void Start()
    {
        boxes = new GameObject[spaces_x * spaces_y];
    }

    public void add_box(int size_x, int size_y, GameObject box)
    {
        boxes[cur_boxes_index] = box;
        box.transform.parent = gameObject.transform;
        box.transform.position = new Vector3();
    }
}
