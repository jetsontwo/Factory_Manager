using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker_Movement : MonoBehaviour {

    public Transform trash_loc, truck_list;
    private Vector3 target = Vector3.zero;
    private Movement move;
    private Worker work;
    private Worker_Stats stats;

    void OnEnable()
    {
        if (work == null)
            work = GetComponent<Worker>();
        if (move == null)
            move = GetComponent<Movement>();
        if (stats == null)
            stats = GetComponent<Worker_Stats>();
        StartCoroutine(enable());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator enable()
    {
        yield return new WaitUntil(move.is_enabled);
        if (work.holding_item())
            StartCoroutine(drop_off_item());
        else
            StartCoroutine(Go_to_Trash());
    }

    private GameObject find_zone()
    {
        ///////////////////////////////NEED TO OVERHAUL ONCE TAGS ARE IN PLACE TO FIND DESINATION OF TAG/////////////////////////////////////

        
        GameObject to_return = null;
        foreach(Transform truck in truck_list)
        {
            Transform drop_zone = truck.FindChild("Drop_Off");
            if (to_return == null)
                to_return = drop_zone.gameObject;
            else if ((transform.position - drop_zone.position).magnitude < (transform.position - to_return.transform.position).magnitude)
                to_return = drop_zone.gameObject;
        }
        if(to_return != null)
        {
            Truck_Controller tc = to_return.transform.parent.GetComponent<Truck_Controller>();
            if (tc.cur_boxes_index + 1 >= tc.max_size)
                to_return = null;
        }            //////////////////////////////////////////////////////////////////WRITE SYSTEM SO NUMBER OF BOXES TRUCK HAS INCREASES ONCE WORKER SEES THEM
        return to_return;
    }



    ////////////////////////////////////////////////  ASSIGNED TO TRASH   ////////////////////////////////////////////////

    IEnumerator Go_to_Trash()
    {
        if (work.holding_item())
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(drop_off_item());
        }
        else
        {
            StartCoroutine(move.go_to_pos(new Vector2(trash_loc.position.x + Random.Range(-1f,1f), trash_loc.position.y + Random.Range(-1f, 1f))));
            yield return new WaitUntil(move.at_pos);
            move.move(0, 0);
            StartCoroutine(search_trash());
        }
    }

    IEnumerator search_trash()
    {

        ////PLAY SEARCHING ANIMATION
        yield return new WaitForSeconds(4 - (float) (stats.scavenging * 0.25));
        //STOP SEARCHING ANIMATION
        //CHECK TO SEE IF NEW ITEM, IF SO THEN SHOW THE NOTIFICATION
        GameObject new_mat = trash_loc.GetComponent<Material_Producer>().get_random_item();
        new_mat.transform.parent = transform;
        new_mat.transform.localPosition = new Vector2(0, 0);

        Transform tag_dest = trash_loc.GetComponent<Material_Producer>().get_tag_dest(new_mat.GetComponent<Item_ID>().ID);

        if (tag_dest != null)
        {
            StartCoroutine(move.go_to_pos(tag_dest.GetComponent<Tag_Controller>().destination.transform.position));
            yield return new WaitUntil(move.at_pos);
            work.drop_item(new_mat);
            StartCoroutine(Go_to_Trash());
        }
        else
        {
            ////////// SHOW QUESTION MARK THINKING BUBBLE
        }

    }

    ////////////////////////////////////////////////  UNASSIGNED   ////////////////////////////////////////////////


    IEnumerator Unasigned()
    {
        ////////////////////////Need to fix so player can stop it at any time (use stopallcoroutines when the player picks them up)
        Vector2 next_pos = new Vector2(Random.Range(0f, 8f), Random.Range(0f, -5f));
        StartCoroutine(move.go_to_pos(next_pos));
        yield return new WaitUntil(move.at_pos);
        move.move(0, 0);
        yield return new WaitForSeconds(Random.Range(1f, 3f));
    }

    ////////////////////////////////////////////////  ASSIGNED TO STORAGE   ////////////////////////////////////////////////



    IEnumerator search_for_request()
    {
        yield return new WaitForSeconds(1f);
    }
    //IEnumerator walk_to_box(GameObject box)
    //{
    //    StartCoroutine(move.go_to_pos(box.transform.position));
    //    yield return new WaitUntil(move.at_pos);
    //    move.move(0, 0);
    //    if (box.transform.parent == null)
    //    {
    //        work.pickup_item(box);
    //        StartCoroutine(drop_off_item());
    //    }
    //    else
    //        StartCoroutine(Go_to_Trash());
    //}

    IEnumerator drop_off_item()
    {
        if (work.holding_item())
        {
            GameObject found_truck = find_zone();
            if (found_truck != null)
            {
                StartCoroutine(move.go_to_pos(found_truck.transform.position));
                yield return new WaitUntil(move.at_pos);
                work.interact_with_object();
            }
        }
        StartCoroutine(Go_to_Trash());
    }
}
