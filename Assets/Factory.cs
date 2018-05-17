using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {
    public Queue<int> unit;
    public Blueprint[] blueprints;
    public float spawnTime, progress, efficiency; //Spawntime typ hur mkt arbete att spawna en unit (resurskostnad/abstrakt värde), progress är hur mycket som är gjort, efficiency factoryns effektivitet
    public GameObject spawnZone;
    Vector3 spawnPos,spawnOffset;

	// Use this for initialization
	void Start () {

        Blueprint tempPrint = new Blueprint();
        tempPrint.cost = 100;
        tempPrint.spawnTime = 5;
        tempPrint.prefab = (GameObject)Resources.Load("R_Unit_Prefab");

        Blueprint[] tempBlueprints = new Blueprint[3];
        tempBlueprints[0] = tempPrint;

        SetBlueprints(tempBlueprints);

        spawnPos = spawnZone.transform.position;
        spawnOffset = spawnZone.transform.localScale;

	}

    // Update is called once per frame
    void Update() {
        if (unit.Count > 0) {
            if (progress >= spawnTime) {
                Collider[] collided = Physics.OverlapBox(spawnPos, spawnOffset, Quaternion.LookRotation(transform.forward), LayerMask.GetMask("Default"));
                if (collided.Length == 0)//too tired for this shit
                {//is spawnzone empty, really should check the size of the unit
                    Spawn();
                    progress = 0;
                    Debug.Log("Phew... just spawned");
                }
                else
                {
                    Debug.Log("SpawnZone blocked! at factory" + this.GetInstanceID() + "first entry is " + collided[0].gameObject);
                    Debug.Log("spawnPos = " + spawnPos + " spawnOffset = " + spawnOffset);
                }
            }
            else
            {
                progress += Time.deltaTime*efficiency;
            }

        }
    }

        void Spawn()
        {
            Instantiate(blueprints[unit.Dequeue()].prefab, spawnPos, Quaternion.LookRotation(Vector3.forward));
            Debug.Log("spawnPos = " + spawnPos + " spawnOffset = " + spawnOffset);
        }

    public void SetBlueprints(Blueprint[] p_blueprints) {
        blueprints = p_blueprints;
        }
    }

