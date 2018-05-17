using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour {

    public GameObject motherCube;

    public int team; // 0 för blå, 1 för röd.

    private NavMeshAgent agent;

    public List<GameObject> cubeArray = new List<GameObject>();
    public List<GameObject> weapons;

    public float speed = 5.0f;

    public Vector3 direction;

    private float colliderTimer = 3.0f;
    
    // Use this for initialization
    void Start () {
       
        direction = new Vector3(0, 0, 1);
        agent = GetComponent<NavMeshAgent>();

        OrganizeChildren();
        GenerateUnitCollider();

        //lite temporär testkod för att sätta en destination när vi spawnat klart. 

        Debug.Log("Unit of Team " + team);

        Vector3 targetPos = new Vector3(0,0,0);
        if (team == 0)
        {
            targetPos = new Vector3(0,2,-91);
        }
        else if(team == 1)
        {
            targetPos = new Vector3(0, 2, 90);
        }
        
        agent.SetDestination(targetPos);
        
	}
	
    //Ska organisera alla bygg-block vi består utav (våra CompCubes/GameObjects) i vår lista.
    //Kan sedan utökas kanske för att beräkna en omslutande collider kanske.
    public void OrganizeChildren()
    {
        //hämtar ju bara ut våra kuber, inte exempelvis torn.
        CompCube[] children = this.transform.GetComponentsInChildren<CompCube>();

        for (int i = 0; i < children.Length; i++)
        {
            //This should add the cube that the compcube script is attached to.
            cubeArray.Add(children[i].gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
    }

    public void GenerateUnitCollider()
    {
        CompCube[] children = this.transform.GetComponentsInChildren<CompCube>();

        BoxCollider newCollider = this.gameObject.GetComponent<BoxCollider>();
        newCollider.center = this.transform.position;

        Vector3 minVec = new Vector3(9999, 9999, 9999);
        Vector3 maxVec = new Vector3(-9999, -9999, -9999);

        Vector3 sizeOffset = new Vector3(0,0,0);
        Vector3 localPosOfChild = new Vector3(0, 0, 0);

        //hämtar ut en enklare ref till parent.
        GameObject parent_mc = children[0].gameObject;
        //börjar på 1 för att skippa mothercube pga hon räknas med från början i collidern (men detta behövs ändras senare)
        for (int i = 1; i < children.Length; i++)
        {
            //En kubs origo är z=0. 
            //    * * *
            //    * * X
            //    * * * 
            //om man ser på den från sidan. alltså inte i mitten utav den som man väntar sig. 

            Transform tempRefToChild = children[i].transform;
            
            localPosOfChild = tempRefToChild.transform.localPosition + new Vector3(0,0,tempRefToChild.GetComponentInChildren<BoxCollider>().bounds.extents.z);

            sizeOffset = tempRefToChild.GetComponentInChildren<BoxCollider>().bounds.extents;
            //sizeOffset.z = 0; // vi nollställer pga Cube-GOn har sin origo på z = 0 istället för i mitten av sig själv. exempelvis 0.5, 0.5, 0.5
            //Men vi vill inte kolla på positionen, vi vill kolla på de absolut LÄGSTA eller HÖGSTA punkterna och då måste vi lägga till för objectets collision. 
            Vector3 posToCompare = localPosOfChild - sizeOffset;
            //kolla om vi har hittat en ny lägsta.. positoin!?

            if (posToCompare.x < minVec.x)
            {
                minVec.x = posToCompare.x;
            }
            if (posToCompare.y < minVec.y)
            {
                minVec.y = posToCompare.y;
            }
            if (posToCompare.z < minVec.z)
            {
                minVec.z = posToCompare.z;
            }
            //kolla om vi hittat en största position
            //sizeOffset.z = tempRefToChild.GetComponentInChildren<BoxCollider>().bounds.extents.z * 2;
            posToCompare = localPosOfChild + sizeOffset;
            if (posToCompare.x > maxVec.x)
            {
                maxVec.x = posToCompare.x;
            }
            if (posToCompare.y > maxVec.y)
            {
                maxVec.y = posToCompare.y;
            }
            if (posToCompare.z > maxVec.z)
            {
                maxVec.z = posToCompare.z;
            }

            //Den gamla kåden som användes för tester. 
            //Vector3 childpos = children[i].transform.position;
            ////Offsettas med barnets storlek(eller halva storlek?) gånger en riktningsvector baserat på barnets position i förhållande till Units center vilket är mothercubes center. '
            //Vector3 offsetDirection = children[i].transform.position - parent_mc.transform.position;
            //Debug.Log("offsetDir = " + offsetDirection);

            //childpos = childpos + offsetDirection;
            //Debug.Log("childpos after offset: " + childpos);

            
            ////newCollider.bounds.Expand(new Vector3(1,1,1));
            ////this.gameObject.GetComponent<BoxCollider>().bounds.Expand(new Vector3(1, 1, 1));
            //newCollider.size += offsetDirection;
            
            //Debug.Log("GO.collider.bounds: " + this.gameObject.GetComponent<BoxCollider>().bounds);
        }//End of children for loop


        

        //Debug.Log("sizeOffset = " + sizeOffset);
        //Debug.Log("minVec = " + minVec + " maxVec = " + maxVec);
        Bounds newBounds = new Bounds();
        newBounds.SetMinMax(minVec, maxVec);
        newCollider.center = newBounds.center;
        newCollider.size= newBounds.size;

    }

	// Update is called once per frame
	void Update () {
        
		
	}

    private void FixedUpdate()
    {
       
        
    }
}
