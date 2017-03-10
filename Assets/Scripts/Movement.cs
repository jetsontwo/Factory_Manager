using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float acceleration, drag, max_vel, vel_deadzone, held_object_offset;
    private Rigidbody2D rb;
    public AudioSource source;
    private float horiz = 0, vert = 0;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
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
