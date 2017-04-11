using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour {

    public AudioClip pick_up, drop;
    private AudioSource source;
    private bool holding;
    public GameObject holdable_object;
    public Transform item_parent;
    public float held_object_offset;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    //public void pickup_item(GameObject obj)
    //{
    //    source.clip = pick_up;
    //    source.Play();
    //    obj.transform.parent = gameObject.transform;
    //    obj.transform.position = new Vector3(gameObject.transform.position.x + (transform.rotation.eulerAngles.y == 180 ?  held_object_offset : -held_object_offset), gameObject.transform.position.y, 0);
    //    holdable_object = obj;
    //    holding = true;
    //}

    public void drop_item(GameObject obj, GameObject dest, int[] location = null)
    {
        source.clip = drop;
        source.Play();
        Vector3 new_pos = obj.transform.position;
        new_pos += new Vector3((transform.rotation.eulerAngles.y == 180 ? held_object_offset : -held_object_offset), 0, 0);
        if (dest == null)
        {
            obj.transform.position = gameObject.transform.position;
            obj.transform.parent = item_parent;
        }
        else if (dest.transform.tag == "Truck")
                dest.transform.GetComponent<Truck_Controller>().add_box(location, obj);
        else if (dest.tag == "Station")
                dest.transform.GetComponent<Station_Controller>().add_item(obj);
        else
        {
            obj.transform.position = new_pos;
            obj.transform.parent = item_parent;
        }

        holding = false;
        holdable_object = null;
    }

    //public void interact_with_object()
    //{
    //    if (holding)
    //        drop_item(holdable_object);
    //    else if (holdable_object != null)
    //    {
    //        if (holdable_object.transform.parent != null)
    //            if (holdable_object.transform.parent.CompareTag("Worker") || holdable_object.transform.parent.CompareTag("Player"))
    //                return;
    //        pickup_item(holdable_object);
    //    }

    //}

    public bool holding_item()
    {
        return holding;
    }

    //void OnTriggerStay2D(Collider2D c)
    //{
    //    if (c.CompareTag("Item") && !holding)
    //        holdable_object = c.gameObject;
    //}
    //void OnTriggerExit2D(Collider2D c)
    //{
    //    if (!holding)
    //        if (c.gameObject == holdable_object)
    //            holdable_object = null;
    //}
}
