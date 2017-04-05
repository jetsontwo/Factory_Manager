using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Worker_Stats : MonoBehaviour {

    public string worker_name, age, quote;
    public int scavenging, transportation, assembly;
    public GameObject stats_panel;

    void OnEnable()
    {

        ///////////MAKE INTO A NORMAL CURVE SO HIGHER STATS ARE LESS LIKELY
        scavenging = Random.Range(1, 5);
        transportation = Random.Range(1, 5);
        assembly = Random.Range(1, 5);
    }

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
        }
    }

    public void close_stats()
    {
        stats_panel.SetActive(false);
    }
}
