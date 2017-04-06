using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material_Producer : MonoBehaviour {

    public GameObject[] material_ids = new GameObject[100];
    public Tag_Controller[] tags = new Tag_Controller[100];
    int cur_index = 0, cur_index2 = 0;

	// Use this for initialization
	
    public GameObject get_random_item()
    {
        //float rand_num = Random.Range(0, 100f);
        GameObject item = material_ids[0];
        return Instantiate(item);
    }
    
    public GameObject get_tag_dest(int id)
    {
        GameObject to_return = null;
        foreach(Tag_Controller tc in tags)
        {
            if (tc.id_of_object == id)
            {
                to_return = tc.destination;
                break;
            }
        }
        return to_return;
    }

    public void add_mat(GameObject mat)
    {
        material_ids[cur_index++] = mat;
    }

    public void add_tag(Tag_Controller tc)
    {
        tags[cur_index2++] = tc;
    }
}
