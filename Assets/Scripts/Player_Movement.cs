using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : Movement{


    public float acceleration, drag, max_vel, vel_deadzone;
    private Rigidbody2D rb;
    private GameObject holdable_object = null;
    private bool holding = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        setup(rb, acceleration, drag, max_vel, vel_deadzone);
    }

    // Update is called once per frame
    void Update () {
        set_speed(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
        print("jasdfklj");
    }

    void pickup_item(GameObject obj)
    {
        obj.transform.parent = gameObject.transform;
        obj.transform.position = new Vector3(gameObject.transform.position.x - 0.2f, gameObject.transform.position.y, 0);
        holding = true;
    }

    void drop_item(GameObject obj)
    {
        obj.transform.parent = null;
        holding = false;
        holdable_object = null;
    }



}
