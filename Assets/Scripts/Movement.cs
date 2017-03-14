using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float acceleration, drag, max_vel, vel_deadzone;
    private Rigidbody2D rb;
    private AudioSource source;
    private float horiz = 0, vert = 0;
    private bool at_given_pos = false;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    public void move(float horizontal, float vertical)
    {
        horiz = horizontal * acceleration * Time.deltaTime;
        vert = vertical * acceleration * Time.deltaTime;

        if (horiz > 0)
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        
        else if (horiz < 0)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        

        if (horiz != 0)
            rb.velocity = new Vector2(max_vel * horiz, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
        if (vert != 0)
            rb.velocity = new Vector2(rb.velocity.x, max_vel * vert);
        else
            rb.velocity = new Vector2(rb.velocity.x, 0);    
    }

    public IEnumerator go_to_pos(Vector2 next_pos)
    {
        at_given_pos = false;
        while (true)
        {
            int move_x = 0, move_y = 0;
            float dif_x = Mathf.Abs(next_pos.x - transform.position.x), dif_y = Mathf.Abs(next_pos.y - transform.position.y);
            if (dif_x < 0.2 && dif_y < 0.2)
                break;
            if (dif_x >= 0.2)
                move_x = next_pos.x > transform.position.x ? 1 : -1;
            if (dif_y >= 0.2)
                move_y = next_pos.y > transform.position.y ? 1 : -1;
            move(move_x, move_y);
            yield return new WaitForSeconds(0.1f);
        }
        at_given_pos = true;
    }

    public bool at_pos()
    {
        return at_given_pos;
    }
}
