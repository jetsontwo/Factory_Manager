using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Truck_Controller : MonoBehaviour {

    public int spaces_x, spaces_y;
    public Sprite covered, uncovered;
    private SpriteRenderer current;
    private SpriteRenderer load_spot_sprite;
    public float x_space, y_space, max_vel, accel, wait_time;
    public int cur_boxes_index = 0, max_size;
    private GameObject[] items;
    private Vector2 init_location;
    private Rigidbody2D rb;
    private float target_x, original;
    public Money_Controller mc;
    private bool is_at_port = true;
    private int items_inside = 0;

    void Start()
    {
        init_location = new Vector2(transform.parent.position.x + 2.35f, transform.parent.position.y + 0.5f);
        max_size = spaces_x * spaces_y;
        items = new GameObject[max_size];
        rb = transform.parent.GetComponent<Rigidbody2D>();
        current = transform.parent.GetComponent<SpriteRenderer>();
        original = transform.parent.position.x;
        target_x = original + 20;

    }

    public void add_box(int[] location, GameObject box)
    {

        if (current.sprite != uncovered)
            current.sprite = uncovered;

        box.transform.parent = transform.parent;

        box.transform.position = new Vector2(init_location.x - (location[0] * x_space), init_location.y - (location[1] * y_space));
        items_inside++;
        if (items_inside == max_size)
            send();
    }

    public void send()
    {
        current.sprite = covered;
        is_at_port = false;
        StartCoroutine(send_truck());
    }

    IEnumerator send_truck()
    {
        Transform old_parent = transform.parent.parent;
        transform.parent.parent = null;
        load_spot_sprite = GetComponent<SpriteRenderer>();
        while (load_spot_sprite.color.a > 0)
        {
            load_spot_sprite.color = new Color(load_spot_sprite.color.r, load_spot_sprite.color.g, load_spot_sprite.color.b, load_spot_sprite.color.a - 0.02f);
            yield return new WaitForSeconds(0.001f);
        }
        gameObject.SetActive(false);

        while (transform.position.x < target_x)
        {
            if (rb.velocity.magnitude < max_vel)
                rb.velocity += new Vector2(accel * Time.deltaTime, 0);
            yield return new WaitForSeconds(0.1f);
        }
        Vector2 fast = rb.velocity;
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(wait_time);

        /////////////////////////Sell things here/////////////////////////////////////

        mc.add_cash(cur_boxes_index * 10);    // need the box prices here
        cur_boxes_index = 0;
        foreach (GameObject box in items)
            Destroy(box);
        
        rb.velocity = -fast;
        while (transform.parent.position.x > original)
        {
            if(rb.velocity.x < -1f)
                rb.velocity += new Vector2(accel/1.1f * Time.deltaTime, 0);

            yield return new WaitForSeconds(0.1f);
        }
        if (transform.parent.position.x != original)
            transform.parent.position = new Vector3(original, transform.parent.position.y, 0);
        rb.velocity = Vector2.zero;

        gameObject.SetActive(true);
        while (load_spot_sprite.color.a < 1)
        {
            load_spot_sprite.color = new Color(load_spot_sprite.color.r, load_spot_sprite.color.g, load_spot_sprite.color.b, load_spot_sprite.color.a + 0.02f);
            yield return new WaitForSeconds(0.001f);
        }
        transform.parent.parent = old_parent;
        is_at_port = true;
    }

    public bool wait_to_return()
    {
        return is_at_port;
    }


    private void sell_boxes()
    {
        foreach(GameObject obj in items)
        {

        }
    }

    public int[] queue_item(GameObject item)
    {
        Item_ID id = item.GetComponent<Item_ID>();

        if (cur_boxes_index == (spaces_x * spaces_y))
            return new int[] { -1, -1};

        int x_offset = (cur_boxes_index) / spaces_y;
        int y_offset = (cur_boxes_index) % spaces_y;


        items[cur_boxes_index] = item;
        cur_boxes_index++;
        //Need to check that size of item fits in the truck
        return new int[] { x_offset, y_offset };
    }
}
