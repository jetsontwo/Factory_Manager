using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour{


    private Worker work;
    private Movement move;

    // Use this for initialization
    void Start()
    {
        work = GetComponent<Worker>();
        move = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        move.move(horiz, Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space))
            work.interact_with_object();
    }
}
