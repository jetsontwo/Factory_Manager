using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : Movement{


    public float acceleration, drag, max_vel, vel_deadzone, held_object_offset;
    private Rigidbody2D rb;
    private GameObject holdable_object = null;
    private bool holding = false, facing_left = true;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        setup(rb, acceleration, drag, max_vel, vel_deadzone);
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

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Box"))
            holdable_object = c.gameObject;
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if(!holding)
            if (c.gameObject == holdable_object)
                holdable_object = null;
    }

    void pickup_item(GameObject obj)
    {
        obj.transform.parent = gameObject.transform;
        if (facing_left)
            obj.transform.position = new Vector3(gameObject.transform.position.x - held_object_offset, gameObject.transform.position.y, 0);
        else
            obj.transform.position = new Vector3(gameObject.transform.position.x + held_object_offset, gameObject.transform.position.y, 0);
        holding = true;
    }

    void drop_item(GameObject obj)
    {
        obj.transform.parent = null;
        if (facing_left)
            obj.transform.position -= new Vector3(0.75f, 0, 0);
        else
            obj.transform.position += new Vector3(0.75f, 0, 0);
        holding = false;
        holdable_object = null;
    }

}
