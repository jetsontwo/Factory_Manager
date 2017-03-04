using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : Worker{


    public float acceleration, drag, max_vel, vel_deadzone, held_object_offset;

    // Use this for initialization
    void Start()
    {
        setup(acceleration, drag, max_vel, vel_deadzone, held_object_offset);
    }

    // Update is called once per frame
    void Update () {
        float horiz = Input.GetAxis("Horizontal");
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

        set_speed(horiz, Input.GetAxis("Vertical"));
        move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (holding)
                drop_item(holdable_object);
            else if (holdable_object != null)
                pickup_item(holdable_object);
        }


    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.CompareTag("Box") && !holding)
            holdable_object = c.gameObject;
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if(!holding)
            if (c.gameObject == holdable_object)
                holdable_object = null;
    }

    

}
