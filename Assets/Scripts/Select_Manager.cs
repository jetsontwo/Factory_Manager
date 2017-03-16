using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Manager : MonoBehaviour {


    public GameObject cur_manager = null;
    private RaycastHit2D r_hit;
    private Movement cur_move;
    private Camera_Track_Player t_player;
    private Camera_Track_Mouse t_mouse;

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
                    if(cur_manager == null)
                    {
                        cur_manager = r_hit.collider.gameObject;
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
                        cur_manager = r_hit.collider.gameObject;
                        cur_manager.GetComponent<Player_Movement>().enabled = true;
                        cur_manager.GetComponent<Worker_Movement>().enabled = false;
                        cur_manager.GetComponent<BoxCollider2D>().isTrigger = false;
                        cur_move = cur_manager.GetComponent<Movement>();
                        cur_move.acceleration = 20;
                        cur_move.max_vel = 10;
                    }
                    track_player(cur_manager);
                }
            }
            else
            {
                if(cur_manager != null)
                {
                    cur_move.acceleration = 15;
                    cur_move.max_vel = 8;
                    cur_manager.GetComponent<Player_Movement>().enabled = false;
                    cur_manager.GetComponent<Worker_Movement>().enabled = true;
                    cur_manager.GetComponent<BoxCollider2D>().isTrigger = true;
                    cur_manager = null;
                    track_mouse();
                }
            }
        }

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
}
