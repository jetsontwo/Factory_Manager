using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money_Controller : MonoBehaviour {

    public Text money_text;
    private float cur_money;
	// Use this for initialization
	void Start () {
        cur_money = 100;
        money_text.text = "Cash: $" + cur_money;
	}

    void Update()
    {
        money_text.text = "Cash: $" + cur_money;
    }

    public void add_cash(float amount)
    {
        cur_money += amount;
    }
    public void remove_cash(float amount)
    {
        cur_money -= amount;
    }
}
