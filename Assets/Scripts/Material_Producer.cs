using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material_Producer : MonoBehaviour {

    public GameObject[] material_ids = new GameObject[100];
    int cur_index = 0;

	// Use this for initialization
	
    public GameObject get_random_item()
    {
        float rand_num = Random.Range(0, 100f);
        return Instantiate(material_ids[0]);
    }

    public void add_mat(GameObject mat)
    {
        material_ids[cur_index++] = mat;
    }
}
