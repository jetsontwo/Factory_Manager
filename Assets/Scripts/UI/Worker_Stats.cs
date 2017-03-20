using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Worker_Stats : MonoBehaviour {

    public string worker_name, age, quote;
    public GameObject stats_panel;
    public Select_Manager sm;

    public void show_stats()
    {
        stats_panel.SetActive(true);
        foreach(Transform t in stats_panel.transform)
        {
            if (t.name == "Name")
                t.GetComponent<Text>().text = "Name: " + worker_name;
            else if (t.name == "Age")
                t.GetComponent<Text>().text = "Age: " + age;
            else if (t.name == "Quote")
                t.GetComponent<Text>().text = quote;
            else if(t.name == "Take_Control")
                t.GetComponent<Button>().onClick.AddListener(()=> { sm.set_manager(gameObject); });
        }
    }

    public void close_stats()
    {
        stats_panel.SetActive(false);
    }
}
