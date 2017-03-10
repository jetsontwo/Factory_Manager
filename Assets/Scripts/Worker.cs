using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Movement {

    public AudioClip pick_up, drop;
    public bool facing_left, holding;
    public GameObject holdable_object;



    protected void pickup_item(GameObject obj)
    {
        source.clip = pick_up;
        source.Play();
        obj.transform.parent = gameObject.transform;
        if (facing_left)
            obj.transform.position = new Vector3(gameObject.transform.position.x - held_object_offset, gameObject.transform.position.y, 0);
        else
            obj.transform.position = new Vector3(gameObject.transform.position.x + held_object_offset, gameObject.transform.position.y, 0);
        holding = true;
    }

    protected void drop_item(GameObject obj)
    {
        source.clip = drop;
        source.Play();
        obj.transform.parent = null;
        Vector3 new_pos = obj.transform.position;
        if (facing_left)
            new_pos -= new Vector3(0.3f, 0, 0);
        else
            new_pos += new Vector3(0.3f, 0, 0);
        RaycastHit2D search = Physics2D.Raycast(new_pos, Vector2.zero, 1f);
        if (search.transform == null)
            obj.transform.position = new_pos;
        else if (search.transform.tag == "Out_of_Bounds")
            obj.transform.position = gameObject.transform.position;
        else if (search.transform.tag == "Truck")
            search.transform.GetComponent<Truck_Controller>().add_box(1, 1, obj);
        else
            obj.transform.position = new_pos;

        holding = false;
        holdable_object = null;
    }
}
