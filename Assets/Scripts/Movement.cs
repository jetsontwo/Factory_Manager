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
        if (horiz != 0)
            rb.velocity = new Vector2(max_vel * horiz, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
        if (vert != 0)
            rb.velocity = new Vector2(rb.velocity.x, max_vel * vert);
        else
            rb.velocity = new Vector2(rb.velocity.x, 0);    
    }

    public void set_speed(float horizontal, float vertical)
    {
        horiz = horizontal * acceleration * Time.deltaTime; vert = vertical * acceleration * Time.deltaTime;
    }
}
