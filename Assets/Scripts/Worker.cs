using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour {

    public AudioClip pick_up, drop;
    private AudioSource source;
    private bool holding;
    private GameObject holdable_object;
    public Transform boxes_parent;
    public float held_object_offset;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void pickup_item(GameObject obj)
    {
        source.clip = pick_up;
        source.Play();
        obj.transform.parent = gameObject.transform;
        obj.transform.position = new Vector3(gameObject.transform.position.x + (transform.rotation.eulerAngles.y == 180 ?  held_object_offset : -held_object_offset), gameObject.transform.position.y, 0);
        holdable_object = obj;
        holding = true;
    }

    public void drop_item(GameObject obj)
    {
        source.clip = drop;
        source.Play();
        obj.transform.parent = null;
        Vector3 new_pos = obj.transform.position;
        new_pos += new Vector3((transform.rotation.eulerAngles.y == 180 ? held_object_offset : -held_object_offset), 0, 0);
        RaycastHit2D search = Physics2D.Raycast(new_pos, Vector2.zero, 1f);
        if (search.transform == null)
        {
            obj.transform.position = new_pos;
            obj.transform.parent = boxes_parent;
        }
        else if (search.transform.tag == "Out_of_Bounds")
        {
            obj.transform.position = gameObject.transform.position;
            obj.transform.parent = boxes_parent;
        }
        else if (search.transform.tag == "Truck")
            search.transform.GetComponent<Truck_Controller>().add_box(1, 1, obj);
        else
        {
            obj.transform.position = new_pos;
            obj.transform.parent = boxes_parent;
        }

        holding = false;
        holdable_object = null;
    }

    public void interact_with_object()
    {
        if (holding)
            drop_item(holdable_object);
        else if (holdable_object != null)
        {
            if (holdable_object.transform.parent != null)
                if (holdable_object.transform.parent.CompareTag("Worker") || holdable_object.transform.parent.CompareTag("Player"))
                    return;
            pickup_item(holdable_object);
        }
            
    }

    public bool holding_box()
    {
        return holding;
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.CompareTag("Box") && !holding)
            holdable_object = c.gameObject;
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (!holding)
            if (c.gameObject == holdable_object)
                holdable_object = null;
    }
}
