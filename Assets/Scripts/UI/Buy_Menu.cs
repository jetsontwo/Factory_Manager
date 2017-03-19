using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buy_Menu : MonoBehaviour {

    private Camera_Track_Mouse m_track;  //0
    private Camera_Track_Player p_track; //1
    private int track_type = -1;
    public GameObject menu;
    private GameObject deliv_zone, box_to_place;
    public Sprite highlight;
    private SpriteRenderer changed_sprite;

	// Use this for initialization
	void Start () {
        m_track = GetComponent<Camera_Track_Mouse>();
        p_track = GetComponent<Camera_Track_Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.B))
        {
            if(!menu.activeSelf)
            {
                disable_Camera();
                menu.SetActive(true);
                populate_menu();
            }
            else
            {
                enable_Camera();
                menu.SetActive(false);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menu.activeSelf)
            {
                enable_Camera();
                menu.SetActive(false);
            }
        }
	}


    private void populate_menu()
    {
        
    }
        
    public void set_box(GameObject box)
    {
        box_to_place = box;
    }

    public void create_order(int num_boxes)
    {
        StartCoroutine(Set_Zone(num_boxes));

    }

    private IEnumerator Set_Zone(int num_boxes)
    {
        RaycastHit2D r_hit;
        menu.SetActive(false);
        enable_Camera();
        Sprite before = null;
        while(true)
        {
            r_hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)), Vector2.zero);
            if(r_hit)
            {
                if(r_hit.collider.gameObject.tag == "Delivery")
                {
                    if(r_hit.collider.GetComponent<Place_Boxes>().box == null)
                    {
                        if(changed_sprite == null)
                        {
                            changed_sprite = r_hit.collider.GetComponent<SpriteRenderer>();
                            before = changed_sprite.sprite;
                            print(before.name);
                        }
                        changed_sprite.sprite = highlight;
                        if (Input.GetMouseButtonDown(0))
                        {
                            deliv_zone = r_hit.collider.gameObject;
                            changed_sprite.sprite = before;
                            before = null;
                            changed_sprite = null;
                            break;
                        }
                    }

                }
                else
                {
                    if (before != null)
                    {
                        changed_sprite.sprite = before;
                        before = null;
                        changed_sprite = null;
                    }
                }
            }
            else
            {
                if (before != null)
                {
                    changed_sprite.sprite = before;
                    before = null;
                    changed_sprite = null;
                }
            }
            yield return new WaitForSeconds(0.001f);
        }
        if (box_to_place != null)
            StartCoroutine(deliv_zone.GetComponent<Place_Boxes>().create_boxes(num_boxes, box_to_place));
        
        box_to_place = null;
    }

    private void disable_Camera()
    {
        if (m_track.enabled)
        {
            m_track.enabled = false;
            track_type = 0;
        }
        else if (p_track.enabled)
        {
            p_track.enabled = false;
            track_type = 1;
        }
    }

    private void enable_Camera()
    {
        if (track_type == 0)
            m_track.enabled = true;
        else if (track_type == 1)
            p_track.enabled = true;
        track_type = -1;
    }
}
