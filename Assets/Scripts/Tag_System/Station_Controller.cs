using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station_Controller : MonoBehaviour {

    public Dictionary<GameObject, int> recipe = new Dictionary<GameObject, int>(), cur_items = new Dictionary<GameObject, int>();
    public GameObject worker;
    public Material_Producer mat_list;
    

    void Start()
    {
        Dictionary<GameObject, int> new_recipe = new Dictionary<GameObject, int>();
        new_recipe.Add(mat_list.material_ids[0], 3);
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
        if (cur_items.ContainsKey(item))
            cur_items[item] = cur_items[item]++;
        else
            cur_items.Add(item, 1);
        print(cur_items[item]);
    }

    public void set_recipe(Dictionary<GameObject, int> new_recipe)
    {
        recipe = new_recipe;
    }

    void Update()
    {
        bool can_craft = false;

        //check to make sure there is an active recipe
        if (recipe.Count > 0 && cur_items.Count > 0)
            can_craft = true;
        //if has enough and worker is still assigned there then craft
        foreach (KeyValuePair<GameObject, int> key_val in recipe)
        {
            if (cur_items.ContainsKey(key_val.Key))
            {
                print(cur_items[key_val.Key]);
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
        if (can_craft)
        {
            print("fjsdkal");
            if (worker.GetComponent<Worker_Movement>().assigned_to_gameobject == gameObject)
            {
                StartCoroutine(worker.GetComponent<Worker_Movement>().Craft());
                // subtract items from cur items and delete them from the table
                print("jsdaflijsdalkfjsadlk;");
            }
        }
    }
}
