using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station_Controller : MonoBehaviour {

    public Dictionary<int, int> recipe = new Dictionary<int, int>(), cur_items = new Dictionary<int, int>();
    private GameObject worker;
    public Material_Producer mat_list;
    private bool has_items;
    

    void Start()
    {
        Dictionary<int, int> new_recipe = new Dictionary<int, int>();
        new_recipe.Add(1, 3);
        set_recipe(new_recipe);
    }

    public void change_worker(GameObject new_worker)
    {
        if(new_worker == null)
        {
            worker = null;
            return;
        }
        if(worker != null)
            worker.GetComponent<Worker_Movement>().change_assignment("None");
        worker = new_worker;
        worker.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        worker.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        worker.GetComponent<SpriteRenderer>().sortingOrder = -10;
        
    }

    public void add_item(GameObject item)
    {
        item.transform.parent = transform;
        item.transform.localPosition = Vector2.zero;
        int item_id = item.GetComponent<Item_ID>().ID;
        if (cur_items.ContainsKey(item_id))
            cur_items[item_id]++;
        else
            cur_items.Add(item_id, 1);
        has_items = check_can_craft();
    }

    public void set_recipe(Dictionary<int, int> new_recipe)
    {
        recipe = new_recipe;
    }

    public bool can_craft()
    {
        return has_items;
    }


    private bool check_can_craft()
    {
        bool can_craft = true;

        //check to make sure there is an active recipe
        if (recipe.Count == 0)
            return false;
        //if has enough

        foreach (KeyValuePair<int, int> key_val in recipe)
        {
            if (cur_items.ContainsKey(key_val.Key))
            {
                if (cur_items[key_val.Key] < key_val.Value)
                {
                    can_craft = false;
                    break;
                }
            }
            else
            {
                can_craft = false;
                break;
            }
        }
        return can_craft;
    }
}
