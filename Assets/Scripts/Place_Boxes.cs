using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place_Boxes : MonoBehaviour {


    public Transform box_parent;
    public GameObject box = null;
    public float wait_time;

    void Start()
    {
        //for testing
        //StartCoroutine(create_boxes(30, box));
    }


    public IEnumerator create_boxes(int num_boxes, GameObject type_of_box)
    {
        box = type_of_box;
        for(int i = 0; i < num_boxes; ++i)
        {
            GameObject new_box = Instantiate(type_of_box, transform.position, Quaternion.identity, box_parent);
            new_box.transform.position += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            yield return new WaitForSeconds(wait_time);
        }
        box = null;
    }
}
