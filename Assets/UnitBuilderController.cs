using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//kolla att cubes funkar som de ska, åt båda hållen.

public class UnitBuilderController : MonoBehaviour {

    public UnitController unit;
    public List<GameObject> bluePrints = new List<GameObject>();
    public int selection = 0;


    public int blocksPlaced = 1;
    public Vector3[] blocks;

    public LayerMask componentLayer;

    // Use this for initialization
    void Start() {
        //PlaceBlock();
        componentLayer = LayerMask.GetMask("Component");

    }

    public void PlaceBlock(RaycastHit p_hitInfo)
    {
        Vector3 dir = p_hitInfo.normal;        
        Vector3 spawnPos = p_hitInfo.transform.position + (Vector3.Scale(p_hitInfo.collider.bounds.extents, dir));// + offset;

        GameObject parentBlock = p_hitInfo.transform.parent.transform.gameObject;
        
        GameObject newBlock = Instantiate(bluePrints[selection], spawnPos, Quaternion.LookRotation(dir),
           parentBlock.transform);

        parentBlock.GetComponent<CompCube>().AssignNeighbour(0, newBlock);
        newBlock.GetComponent<CompCube>().AssignNeighbour(5, parentBlock);


        Debug.Log("Writing through array call parent.cubes[0] = " + parentBlock.GetComponent<CompCube>().cubes[0].name);
        Debug.Log("Writing through array call through parent array newBlock.cubes[0] = " + parentBlock.GetComponent<CompCube>().cubes[0].GetComponent<CompCube>().cubes[5].name);

        Debug.Log("name of element: " + p_hitInfo.transform.parent.gameObject.GetComponent<CompCube>().cubes[0].GetComponentInParent<CompCube>().cubes[5].name);

        //unit.cubeArray.Add(newBlock);

    }

    //Gamla PlaceBlock där vi upptäckte arrayproblemet.
    public void OldPlaceBlock(RaycastHit p_hitInfo)
    {

        Vector3 dir = p_hitInfo.normal;
        //Vector3 offset = p_hitInfo.point - p_hitInfo.transform.position;
        Vector3 spawnPos = p_hitInfo.transform.position + (Vector3.Scale(p_hitInfo.collider.bounds.extents, dir));// + offset;

        //kolla vilken sida; skulle vara najs med en switch men kommer inte på hur :p OOOOOOOOOOOOOOOOOOOOOOO
        //så det blir stupid solution
        int side = 0;
        int oppositeSide = 5;
        if (dir == Vector3.forward)
        {
            side = 1;
            oppositeSide = 3;
        }
        if (dir == -Vector3.forward)
        {
            side = 3;
            oppositeSide = 1;
        }
        if (dir == -Vector3.up)
        {
            side = 5;
            oppositeSide = 0;
        }
        if (dir == Vector3.right)
        {
            side = 4;
            oppositeSide = 2;
        }

        if (dir == -Vector3.right)
        {
            side = 2;
            oppositeSide = 4;
        }
        
        //Något slags fuckery här, med att den nya kubens array inte representeras korrekt i inspektorn? visar inte att den har sin förälder i arrayen.
        GameObject newBlock = Instantiate(bluePrints[selection], spawnPos, Quaternion.LookRotation(dir),
            p_hitInfo.transform.parent.transform.parent.transform);
        GameObject oldBlock = p_hitInfo.collider.transform.parent.gameObject;

        unit.cubeArray.Add(newBlock);
        CompCube block = p_hitInfo.collider.gameObject.GetComponentInParent(typeof(CompCube)) as CompCube;
        CompCube newBlockAsComp = newBlock.GetComponent(typeof(CompCube)) as CompCube;

        block.cubes[side] = newBlock;
        newBlockAsComp.cubes[oppositeSide] = oldBlock;
        //Debug.Log("Is block in sides? newblock: " + newBlockAsComp.cubes[oppositeSide] + " oldblock " + block.cubes[side]);
        for (int i = 0; i < (p_hitInfo.transform.gameObject.GetComponentInParent(typeof(CompCube)) as CompCube).cubes.Length; i++)
        {
            Debug.Log("träffade kubens cubes["+i+"] = " + (p_hitInfo.transform.gameObject.GetComponentInParent(typeof(CompCube)) as CompCube).cubes[i]);
        }

        //om sidan är tagen redan hos vårat träffade object (typ att man klickar GENOM saker!?) så får vi itne spawna något nytt.
        if((p_hitInfo.transform.gameObject.GetComponentInParent(typeof(CompCube)) as CompCube).cubes[side] != null)
        {
            Debug.Log("Hände det?");
            return;
        }

        //GameObject newBlock = Instantiate(bluePrints[selection], spawnPos, Quaternion.LookRotation(dir), p_hitInfo.transform.parent.transform.parent.transform);
        //GameObject oldBlock = p_hitInfo.transform.parent.gameObject;


        //unit.cubeArray.Add(newBlock);

        //CompCube block = p_hitInfo.transform.gameObject.GetComponentInParent(typeof(CompCube)) as CompCube;
        //block.cubes[side] = newBlock;
        Debug.Log("namn på träff" + p_hitInfo.transform.name);
        (p_hitInfo.transform.gameObject.GetComponentInParent(typeof(CompCube)) as CompCube).cubes[side] = newBlock;


        //Debug.Log("oldBlock: " + oldBlock.name);
        (newBlock.GetComponentInParent(typeof(CompCube)) as CompCube).cubes[oppositeSide] = oldBlock;
       // Debug.Log("newBlock name" + newBlock.name);
        newBlock.GetComponentInChildren<Renderer>().material.color = Color.blue;
        
    


        //newBlockAsComp.cubes[oppositeSide] = oldBlock;

        //Debug.Log("Block was placed on sides" + side + " and " + oppositeSide);
        //Debug.Log("newBlocks cubes[5]" + (newBlock.GetComponentInParent(typeof(CompCube)) as CompCube).cubes[oppositeSide])
        //Debug.Log("Units nr of blocks: " + unit.cubeArray.Length.ToString());


        //Testar att referera till cuben som nite syns i arrayen. 
        //(newBlock.GetComponentInParent(typeof(CompCube)) as CompCube).cubes[oppositeSide].transform.Translate(new Vector3(2, 2, 2));

    }

    public void CycleSelection( ){
        selection++;
        if (selection > bluePrints.Count -1)
            selection = 0;
        Debug.Log("selection = " + selection);
    }

    public void DeleteBlock(RaycastHit p_hitInfo) {
        Destroy(p_hitInfo.transform.gameObject);
    }

    public void SaveModel()
    {

    }

    public void LoadModel()
    {

    }

    // Update is called once per frame
    void Update () {
       
    }
}
