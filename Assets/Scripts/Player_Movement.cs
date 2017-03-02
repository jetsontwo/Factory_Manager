using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : Movement{


    public float acceleration, drag, max_vel, vel_deadzone;
    private Rigidbody2D rb;

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

    }

}
