using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompCube : MonoBehaviour {

    public CompCube motherCube;
    public int ID; 

    public GameObject[] cubes = new GameObject[6]; //definiera vilken plats vilken orientering (0 uppåt, 1 framåt därefter moturs 5 nedåt)


    public void AssignNeighbour(int p_pos, GameObject p_neighbour)
    {
        cubes[p_pos] = p_neighbour;
    }

	// Use this for initialization
	void Awake () {

        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i] = null;
        }

    }

    public CompCube GetMotherCube() {
        return this.motherCube;
    }

    public void SetMotherCube(CompCube p_motherCube) {
        this.motherCube = p_motherCube;
    }

    // Update is called once per frame
    void Update () {
		
	}

    
}
