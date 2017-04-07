using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker_Movement : MonoBehaviour {

    public Transform trash_loc, truck_list;
    private Vector3 target = Vector3.zero;
    private Movement move;
    private Worker work;
    private Worker_Stats stats;
    public string assignment = "None";
    public GameObject assigned_to_gameobject = null;

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
            StartCoroutine(drop_off_item(work.holdable_object));
        else
        {
            if (assignment == "None")
                StartCoroutine(Wander());
            else if (assignment == "Storage")
                StartCoroutine(search_for_request());
            else if (assignment == "Trash")
                StartCoroutine(Go_to_Trash());
            else if (assignment == "Station")
                StartCoroutine(Craft());
        }
    }

    



    ////////////////////////////////////////////////  ASSIGNED TO TRASH   ////////////////////////////////////////////////

    IEnumerator Go_to_Trash()
    {
        if (work.holding_item())
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(drop_off_item(work.holdable_object));
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

        StartCoroutine(drop_off_item(new_mat));

    }

    ////////////////////////////////////////////////  UNASSIGNED   ////////////////////////////////////////////////


    IEnumerator Wander()
    {
        ////////////////////////Need to fix so player can stop it at any time (use stopallcoroutines when the player picks them up)
        while(true)
        {
            Vector2 next_pos = new Vector2(Random.Range(0f, 8f), Random.Range(0f, -5f));
            if(!Physics2D.Raycast(next_pos, Vector2.zero))
            {
                StartCoroutine(move.go_to_pos(next_pos));
                yield return new WaitUntil(move.at_pos);
                move.move(0, 0);
                yield return new WaitForSeconds(Random.Range(1f, 3f));
            }
        }
    }

    ////////////////////////////////////////////////  ASSIGNED TO STORAGE   ////////////////////////////////////////////////



    IEnumerator search_for_request()
    {
        yield return new WaitForSeconds(1f);
    }


    ////////////////////////////////////////////////  ASSIGNED TO STATION   ////////////////////////////////////////////////

    public IEnumerator Craft()
    {
        while (true)
        {
            Station_Controller sc = assigned_to_gameobject.GetComponent<Station_Controller>();
            yield return new WaitUntil(sc.can_craft);
            //Play animation for crafting here/////////////////////////////////////////////////////////////////////
            yield return new WaitForSeconds(5f - (0.5f * stats.assembly));
            sc.create_finished_item();
        }        
    }



    /////////////////////////////////////////////////   HELPER FUNCTIONS    ///////////////////////////////////////////////////



    //private GameObject find_zone()
    //{
    //    ///////////////////////////////NEED TO OVERHAUL ONCE TAGS ARE IN PLACE TO FIND DESINATION OF TAG/////////////////////////////////////


    //    GameObject to_return = null;
    //    foreach (Transform truck in truck_list)
    //    {
    //        Transform drop_zone = truck.FindChild("Drop_Off");
    //        if (to_return == null)
    //            to_return = drop_zone.gameObject;
    //        else if ((transform.position - drop_zone.position).magnitude < (transform.position - to_return.transform.position).magnitude)
    //            to_return = drop_zone.gameObject;
    //    }
    //    if (to_return != null)
    //    {
    //        Truck_Controller tc = to_return.transform.parent.GetComponent<Truck_Controller>();
    //        if (tc.cur_boxes_index + 1 >= tc.max_size)
    //            to_return = null;
    //    }            //////////////////////////////////////////////////////////////////WRITE SYSTEM SO NUMBER OF BOXES TRUCK HAS INCREASES ONCE WORKER SEES THEM
    //    return to_return;
    //}

    IEnumerator drop_off_item(GameObject item)
    {
        GameObject tag_dest = trash_loc.GetComponent<Material_Producer>().get_tag_dest(item.GetComponent<Item_ID>().ID);

        if (tag_dest != null)
        {
            StartCoroutine(move.go_to_pos(tag_dest.transform.position));
            yield return new WaitUntil(move.at_pos);
            work.drop_item(item, tag_dest.gameObject);
            StartCoroutine(Go_to_Trash());
        }
        else
        {
            ////////// SHOW QUESTION MARK THINKING BUBBLE
        }
    }


    public void change_assignment(string new_assignment, GameObject new_assignment_gameobject = null)
    {
        StopAllCoroutines();
        assignment = new_assignment;
        if(assignment != "Station")
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "Workers";
            GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        if (assignment == "Station" || assignment == "Storage")
            assigned_to_gameobject = new_assignment_gameobject;
        else
            assigned_to_gameobject = null;
        move.move(0, 0);
        StartCoroutine(enable());
    }
}
