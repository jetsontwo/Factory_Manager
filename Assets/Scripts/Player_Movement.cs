using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : Movement{


    public float acceleration, drag, max_vel, vel_deadzone, held_object_offset;
    private AudioSource source;
    public AudioClip pick_up, drop;
    private GameObject holdable_object = null;
    private bool holding = false, facing_left = true;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        setup(GetComponent<Rigidbody2D>(), acceleration, drag, max_vel, vel_deadzone);
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
        if (c.CompareTag("Box") && !holding)
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
        source.clip = pick_up;
        source.Play();
        obj.transform.parent = gameObject.transform;
        if (facing_left)
            obj.transform.position = new Vector3(gameObject.transform.position.x - held_object_offset, gameObject.transform.position.y, 0);
        else
            obj.transform.position = new Vector3(gameObject.transform.position.x + held_object_offset, gameObject.transform.position.y, 0);
        holding = true;
    }

    void drop_item(GameObject obj)
    {
        source.clip = drop;
        source.Play();
        obj.transform.parent = null;
        Vector3 new_pos = obj.transform.position;
        if (facing_left)
            new_pos -= new Vector3(0.3f, 0, 0);
        else
            new_pos += new Vector3(0.3f, 0, 0);
        RaycastHit2D search = Physics2D.Raycast(new_pos, Vector2.zero, 1f);
        if (search.transform == null)
            obj.transform.position = new_pos;
        else if (search.transform.tag == "Truck")
            search.transform.GetComponent<Truck_Controller>().add_box(1, 1, obj);
        else
        {
            print(search.transform.name);
            obj.transform.position = gameObject.transform.position;
        }
        holding = false;
        holdable_object = null;
    }

}
