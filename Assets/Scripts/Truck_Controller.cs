using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck_Controller : MonoBehaviour {

    public int spaces_x, spaces_y;
    public Sprite covered, uncovered;
    private SpriteRenderer current;
    private SpriteRenderer load_spot_sprite;
    public GameObject load_spot;
    public float x_space, y_space, max_vel, accel, wait_time;
    private int cur_boxes_index = 0;
    private GameObject[] boxes;
    private Vector2 init_location;
    private Rigidbody2D rb;
    private float target_x, original;


    void Start()
    {
        init_location = new Vector2(transform.position.x + 2.35f, transform.position.y + 0.5f);
        boxes = new GameObject[spaces_x * spaces_y];
        rb = GetComponent<Rigidbody2D>();
        current = GetComponent<SpriteRenderer>();
        original = transform.position.x;
        target_x = original + 20;

    }

    public void add_box(int size_x, int size_y, GameObject box)
    {
        if (current.sprite != uncovered)
            current.sprite = uncovered;
        if (cur_boxes_index < (spaces_x * spaces_y))
        {
            box.transform.parent = gameObject.transform;

            int x_offset = cur_boxes_index / spaces_y;
            int y_offset = cur_boxes_index % spaces_y;

            box.transform.position = new Vector2(init_location.x - (x_offset * x_space), init_location.y - (y_offset * y_space));
            cur_boxes_index++;
        }
        else
            send();
    }

    public void send()
    {
        current.sprite = covered;
        StartCoroutine(send_truck());
    }

    IEnumerator send_truck()
    {
        load_spot_sprite = load_spot.GetComponent<SpriteRenderer>();
        while (load_spot_sprite.color.a > 0)
        {
            load_spot_sprite.color = new Color(load_spot_sprite.color.r, load_spot_sprite.color.g, load_spot_sprite.color.b, load_spot_sprite.color.a - 0.02f);
            yield return new WaitForSeconds(0.001f);
        }
        load_spot.SetActive(false);

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
        
        rb.velocity = -fast;
        while (transform.position.x > original)
        {
            if(rb.velocity.magnitude > 0.1)
                rb.velocity += new Vector2(accel/1.1f * Time.deltaTime, 0);

            yield return new WaitForSeconds(0.1f);
        }
        if (transform.position.x != original)
            transform.position = new Vector3(original, transform.position.y, 0);
        rb.velocity = Vector2.zero;

        while (load_spot_sprite.color.a < 255)
        {
            load_spot_sprite.color = new Color(load_spot_sprite.color.r, load_spot_sprite.color.g, load_spot_sprite.color.b, load_spot_sprite.color.a + 0.02f);
            yield return new WaitForSeconds(0.001f);
        }
        load_spot.SetActive(true);
    }


    private void sell_boxes()
    {
        foreach(GameObject obj in boxes)
        {

        }
    }
}
