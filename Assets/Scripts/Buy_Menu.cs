using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buy_Menu : MonoBehaviour {

    private Camera_Track_Mouse m_track;  //0
    private Camera_Track_Player p_track; //1
    private int track_type = -1;
    public GameObject menu;

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
                menu.SetActive(true);
                populate_menu();
            }
            else
            {
                if (track_type == 0)
                    m_track.enabled = true;
                else if (track_type == 1)
                    p_track.enabled = true;
                track_type = -1;
                menu.SetActive(false);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menu.activeSelf)
            {
                if (track_type == 0)
                    m_track.enabled = true;
                else if (track_type == 1)
                    p_track.enabled = true;
                track_type = -1;
                menu.SetActive(false);
            }
        }
	}


    private void populate_menu()
    {
        
    }
}
