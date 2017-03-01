using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {


    public float acceleration, drag, max_vel, vel_deadzone;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {

        if (rb.velocity.magnitude < max_vel && Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            rb.velocity += new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        else if (rb.velocity.magnitude > vel_deadzone)
            rb.velocity -= new Vector2(rb.velocity.x / drag, rb.velocity.y / drag);
        else
            rb.velocity = Vector2.zero;
    }
}
