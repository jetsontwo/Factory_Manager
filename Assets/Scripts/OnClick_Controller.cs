using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript;

public class OnClick_Controller : MonoBehaviour {


    public GameObject cur_manager = null;
    private RaycastHit2D r_hit;
    private Movement cur_move;
    private Camera_Track_Player t_player;
    private Camera_Track_Mouse t_mouse;
    private Worker_Stats ws;

	// Use this for initialization
	void Start () {
        t_player = GetComponent<Camera_Track_Player>();
        t_mouse = GetComponent<Camera_Track_Mouse>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            r_hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(r_hit)
            {
                if (r_hit.collider.gameObject.tag == "Worker")
                {
                    StartCoroutine(pick_up_worker(r_hit.collider.gameObject));
                }
                else if(r_hit.collider.gameObject.tag == "Control_Tag")
                {
                    StartCoroutine(pick_up_tag(r_hit.collider.gameObject));
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (ws != null)
            {
                ws.close_stats();
                ws = null;
                track_mouse();
            }
        }

    }

    IEnumerator pick_up_worker(GameObject worker)
    {
        //set_manager(r_hit.collider.gameObject);
        //if (cur_manager != null)
        //    deselect_manager();
        ws = r_hit.collider.GetComponent<Worker_Stats>();
        ws.show_stats();
        //track_player(cur_manager);
        //track_player(r_hit.collider.gameObject);
        IEnumerator enumer = attach_to_mouse(worker);
        StartCoroutine(enumer);
        yield return new WaitUntil(() => !Input.GetMouseButton(0));
        StopCoroutine(enumer);
        ws.close_stats();

        get_new_assignment(worker);
    }

    IEnumerator attach_to_mouse(GameObject worker)
    {
        while(true)
        {
            worker.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

            // Do floppy physics here to worker moves around and flops around as the player drags them
            yield return new WaitForSeconds(0.01f);
        }
    }


    private void get_new_assignment(GameObject worker)
    {
        RaycastHit2D rh2_hit = Physics2D.Raycast(worker.transform.position, Vector2.zero, 0f, ~(1 << 10));
        string to_return = "None";
        GameObject new_assignment_gameobject = null;
        if(rh2_hit.collider != null)
        {
            if (rh2_hit.collider.tag == "Trash")
                to_return = "Trash";
            else if (rh2_hit.collider.tag == "Station")
            {
                to_return = "Station";   //////// MAKE THE WORKER APPEAR BEHIND THE STATION
                rh2_hit.collider.GetComponent<Station_Controller>().change_worker(worker);
                new_assignment_gameobject = rh2_hit.collider.gameObject;
            }
            else if (rh2_hit.collider.tag == "Storage")
            {
                to_return = "Storage";
                new_assignment_gameobject = rh2_hit.collider.gameObject;
            }
        }
        worker.GetComponent<Worker_Movement>().change_assignment(to_return, new_assignment_gameobject);
    }



    public IEnumerator pick_up_tag(GameObject control_tag)
    {

        IEnumerator enumer = attach_to_mouse(control_tag);
        StartCoroutine(enumer);
        yield return new WaitUntil(() => !Input.GetMouseButton(0));
        StopCoroutine(enumer);

        Selection.activeGameObject = null;
        get_new_destination(control_tag);
        //show item name and all the possible things it can be turned into???

    }

    private void get_new_destination(GameObject control_tag)
    {
        RaycastHit2D rh2_hit = Physics2D.Raycast(control_tag.transform.position, Vector2.zero, 0f, ~(1 << 10));
        GameObject new_destination = null;
        print(rh2_hit.collider);
        if (rh2_hit.collider != null)
        {
            if (rh2_hit.collider.tag == "Truck" || rh2_hit.collider.tag == "Station")
                new_destination = rh2_hit.collider.gameObject;               
            
            //else if (rh2_hit.collider.tag == "Storage")
            //{
            //    to_return = "Storage";
            //    new_assignment_gameobject = rh2_hit.collider.gameObject;
            //}
        }
        if (new_destination != null)
        {
            control_tag.transform.position = new Vector2(new_destination.transform.position.x, new_destination.transform.position.y + 0.5f);
            control_tag.GetComponent<Tag_Controller>().destination = new_destination;

        }
        else
            Destroy(control_tag);
    }









    void track_mouse()
    {
        t_player.enabled = false;
        t_mouse.enabled = true;
    }

    void track_player(GameObject player)
    {
        t_mouse.enabled = false;
        t_player.enabled = true;
        t_player.thing_to_track = player;
    }
    
    public void set_manager(GameObject manager)
    {
        if (cur_manager == null)
        {
            cur_manager = manager;
            cur_manager.GetComponent<Player_Movement>().enabled = true;
            cur_manager.GetComponent<Worker_Movement>().enabled = false;
            cur_manager.GetComponent<BoxCollider2D>().isTrigger = false;
            cur_move = cur_manager.GetComponent<Movement>();
            cur_move.acceleration = 20;
            cur_move.max_vel = 10;
        }
        else
        {
            cur_move.acceleration = 15;
            cur_move.max_vel = 8;
            cur_manager.GetComponent<Player_Movement>().enabled = false;
            cur_manager.GetComponent<Worker_Movement>().enabled = true;
            cur_manager.GetComponent<BoxCollider2D>().isTrigger = true;
            cur_manager = manager;
            cur_manager.GetComponent<Player_Movement>().enabled = true;
            cur_manager.GetComponent<Worker_Movement>().enabled = false;
            cur_manager.GetComponent<BoxCollider2D>().isTrigger = false;
            cur_move = cur_manager.GetComponent<Movement>();
            cur_move.acceleration = 20;
            cur_move.max_vel = 10;
        }
    }

    public void deselect_manager()
    {
        cur_move.acceleration = 15;
        cur_move.max_vel = 8;
        cur_manager.GetComponent<Player_Movement>().enabled = false;
        cur_manager.GetComponent<Worker_Movement>().enabled = true;
        cur_manager.GetComponent<BoxCollider2D>().isTrigger = true;
        cur_manager = null;
    }
}
