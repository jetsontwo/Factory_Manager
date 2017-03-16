using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker_Movement : MonoBehaviour {

    public Transform box_list, truck_list;
    private Vector3 target = Vector3.zero;
    private Movement move;
    private Worker work;

    void OnEnable()
    {
        if (work == null)
            work = GetComponent<Worker>();
        if (move == null)
            move = GetComponent<Movement>();
        StartCoroutine(enable());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator enable()
    {
        yield return new WaitUntil(move.is_enabled);
        if (work.holding_box())
            StartCoroutine(drop_off_box());
        else
            StartCoroutine(Idle());
    }

    private GameObject find_box()
    {

        GameObject to_return = null;
        foreach (Transform box in box_list)
        {
            if (to_return == null)
                to_return = box.gameObject;
            else if ((transform.position - box.position).magnitude < (transform.position - to_return.transform.position).magnitude)
                to_return = box.gameObject;
        }
        return to_return;
    }

    private GameObject find_zone()
    {
        
        GameObject to_return = null;
        foreach(Transform truck in truck_list)
        {
            Transform drop_zone = truck.FindChild("Drop_Off");
            if (to_return == null)
                to_return = drop_zone.gameObject;
            else if ((transform.position - drop_zone.position).magnitude < (transform.position - to_return.transform.position).magnitude)
                to_return = drop_zone.gameObject;
        }
        return to_return;
    }

    IEnumerator Idle()
    {
        while(true)
        {
            GameObject found_box = find_box();
            if (found_box != null)
            {
                StartCoroutine(walk_to_box(found_box));
                found_box.transform.parent = null;
                break;
            }
            Vector2 next_pos = new Vector2(Random.Range(0f, 8f), Random.Range(0f, -5f));
            StartCoroutine(move.go_to_pos(next_pos));
            yield return new WaitUntil(move.at_pos);
            move.move(0, 0);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    IEnumerator walk_to_box(GameObject box)
    {
        StartCoroutine(move.go_to_pos(box.transform.position));
        yield return new WaitUntil(move.at_pos);
        move.move(0, 0);
        if (box.transform.parent == null)
        {
            work.pickup_item(box);
            StartCoroutine(drop_off_box());
        }
        else
            StartCoroutine(Idle());
    }

    IEnumerator drop_off_box()
    {
        if (work.holding_box())
        {
            GameObject found_truck = find_zone();
            StartCoroutine(move.go_to_pos(found_truck.transform.position));
            yield return new WaitUntil(move.at_pos);
            work.interact_with_object();
        }
        StartCoroutine(Idle());
    }
}
