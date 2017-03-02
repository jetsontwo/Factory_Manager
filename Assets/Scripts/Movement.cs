using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private float acceleration, drag, max_vel, vel_deadzone;
    private Rigidbody2D rb;
    private float horiz, vert;

    // Use this for initialization
    public void setup(Rigidbody2D rb_input, float accel_input, float drag_input, float max_vel_input, float vel_deadzone_input)
    {
        rb = rb_input;
        acceleration = accel_input;
        drag = drag_input;
        max_vel = max_vel_input;
        vel_deadzone = vel_deadzone_input;
        horiz = 0; vert = 0;
    }

    // Update is called once per frame
    public void move()
    {


        //Swingy movement
        //if (rb.velocity.magnitude < max_vel && horiz != 0 || vert != 0)
        //{
        //    rb.velocity += new Vector2(horiz, 0) * Time.deltaTime;
        //    rb.velocity += new Vector2(0, vert) * Time.deltaTime;
        //}
        //if(rb.velocity.magnitude < max_vel && horiz != 0)
        //{
        //    rb.velocity +=
        //}
        //else if (rb.velocity.magnitude > vel_deadzone)
        //    rb.velocity -= new Vector2(rb.velocity.x / drag, rb.velocity.y / drag) * Time.deltaTime;
        //else
        //    rb.velocity = Vector2.zero;


        if (rb.velocity.magnitude < max_vel && horiz != 0)
            rb.velocity = new Vector2(horiz, rb.velocity.y) ;
        else if (rb.velocity.x < vel_deadzone && rb.velocity.x > -vel_deadzone)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else if (rb.velocity.x != 0 && horiz == 0)
            rb.velocity -= new Vector2(horiz / drag, 0);

        if (rb.velocity.magnitude < max_vel && vert != 0)
            rb.velocity = new Vector2(rb.velocity.x, vert);
        else if (rb.velocity.y < vel_deadzone && rb.velocity.y > -vel_deadzone)
            rb.velocity = new Vector2(rb.velocity.x, 0);
        else if (rb.velocity.y != 0 && vert == 0)
            rb.velocity -= new Vector2(0, vert / drag);
        
    }

    public void set_speed(float horizontal, float vertical)
    {
        horiz = horizontal * acceleration * Time.deltaTime; vert = vertical * acceleration * Time.deltaTime;
    }
}
