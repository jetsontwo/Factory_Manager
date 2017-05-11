using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OnClickDown : MonoBehaviour {

    public GameObject tag_parent;
    public void Create_Tag(GameObject prefab_tag)
    {
        GameObject cam = (GameObject)Camera.main.gameObject;
        OnClick_Controller occ = cam.GetComponent<OnClick_Controller>();
        print(occ);
        GameObject tag = Instantiate(prefab_tag, tag_parent.transform);
        StartCoroutine(occ.pick_up_tag(tag));
    }
}
