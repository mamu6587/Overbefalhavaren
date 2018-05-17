using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyModeController : MonoBehaviour {
    public LayerMask TerrainUnitGui;
    public List<GameObject> selected;
    public int team;


	// Use this for initialization
	void Start () {
        selected = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Select(RaycastHit p_hitinfo) {
        GameObject hit = p_hitinfo.collider.gameObject;
        switch (hit.gameObject.tag)
        {
            case "unit":
                selected.Add(hit);
                Debug.Log("Select HIT!" + hit + "Selection is now" + selected.ToString())   ;
                break;
        }
        Debug.Log("Select HIT!" + hit);
        
    }
    


    public void Order(RaycastHit p_hitinfo)
    {
        GameObject hit = p_hitinfo.collider.gameObject;
        Debug.Log("Order HIT!" + hit);
    }

    public void Delete()
    {
        //foreach selected suicide
    }

    public void Save()
    {

    }

    public void Load()
    {

    }
}
