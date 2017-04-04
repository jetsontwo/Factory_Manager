using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station_Controller : MonoBehaviour {

    public Dictionary<GameObject, int> recipe = new Dictionary<GameObject, int>(), cur_items = new Dictionary<GameObject, int>();
    

    public void add_item(GameObject item)
    {
        item.transform.parent = transform;
        item.transform.localPosition = Vector2.zero;
        if (cur_items.ContainsKey(item))
            cur_items[item]++;
        else
            cur_items.Add(item, 1);
    }

    public void set_recipe(Dictionary<GameObject, int> new_recipe)
    {
        recipe = new_recipe;
    }

}
