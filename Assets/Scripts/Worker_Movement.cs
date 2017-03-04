using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker_Movement : Worker {

    public float acceleration, drag, max_vel, vel_deadzone, held_object_offset;
    public Transform box_list, truck_list;
    private bool should_interact = true, need_target = true;
    private Vector3 target = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        setup(acceleration, drag, max_vel, vel_deadzone, held_object_offset);
        
    }

    // Update is called once per frame
    void Update () {
        float horiz = 0, vert = 0;
        if (!holding)
        {
            if(need_target)
                target = find_box();
            if ((transform.position - target).magnitude < 0.1)
            {
                should_interact = true;
                need_target = true;
            }
            else
            {
                horiz = target.x > transform.position.x ? 1 : -1;
                vert = target.y > transform.position.y ? 1 : -1;
            }

        }
        else
        {
            if(need_target)
                target = find_zone();
            if ((transform.position - target).magnitude < 0.1)
            {
                should_interact = true;
                need_target = true;
            }
            else
            {
                horiz = target.x > transform.position.x ? 1 : -1;
                vert = target.y > transform.position.y ? 1 : -1;
            }
        }

        if (horiz > 0)
        {
            facing_left = false;
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else if (horiz < 0)
        {
            facing_left = true;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        set_speed(horiz, vert);
        move();

        if (holdable_object != null && should_interact)
        {
            if (holding)
                drop_item(holdable_object);
            else if (holdable_object != null)
            {
                pickup_item(holdable_object);
                should_interact = false;
            }
        }
    }

    private Vector3 find_box()
    {
        need_target = false;
        if (holdable_object != null)
            return Vector3.zero;

        Vector3 to_return = new Vector3(999, 999, 999);
        foreach(Transform box in box_list)
        {
            if ((transform.position - box.position).magnitude < (transform.position - to_return).magnitude)
                to_return = box.position;
        }
        if (to_return.x == 999)
            return new Vector3(Random.Range(2, 6), Random.Range(-2, -4));
        return to_return;
    }

    private Vector3 find_zone()
    {
        need_target = false;
        
        Vector3 to_return = new Vector3(999, 999, 999);
        foreach(Transform truck in truck_list)
        {
            Transform drop_zone = truck.FindChild("Drop_Off");
            if ((transform.position - drop_zone.position).magnitude < (transform.position - to_return).magnitude)
                to_return = drop_zone.position;
        }
        if (to_return.x == 999)
            return new Vector3(Random.Range(2, 6), Random.Range(-2, -4));
        return to_return;
    }



    void OnTriggerStay2D(Collider2D c)
    {
        if (c.CompareTag("Box") && !holding)
            holdable_object = c.gameObject;
        else if (c.CompareTag("Truck") && holding)
            should_interact = true;
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (!holding)
            if (c.gameObject == holdable_object)
                holdable_object = null;
    }
}
