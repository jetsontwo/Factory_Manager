using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place_Boxes : MonoBehaviour {


    public Transform box_parent;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void create_boxes(int num_boxes, GameObject type_of_box)
    {
        for(int i = 0; i < num_boxes; ++i)
        {
            GameObject new_box = Instantiate(type_of_box, box_parent);

        }
    }
}
