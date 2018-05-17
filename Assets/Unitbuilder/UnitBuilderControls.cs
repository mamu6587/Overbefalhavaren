﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBuilderControls : MonoBehaviour
{
    //Vector3 mouseMove;
    Transform c_transform;
    LayerMask componentLayer;
    public UnitBuilderController UBC;

    public float sensX = 100.0f;
    public float sensY = 100.0f;
    // Use this for initialization''

    float rotationY = 0.0f;
    float rotationX = 0.0f;

    void Start()
    {
    c_transform = Camera.main.transform;
    componentLayer = UBC.componentLayer;

    rotationX = c_transform.rotation.eulerAngles.x;
    rotationY = c_transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode))
        {

       }
       */
        //WASD
        if (Input.GetKey(KeyCode.W))
        {
            c_transform.position += c_transform.forward * 5.0f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            c_transform.position += -c_transform.forward * 5.0f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            c_transform.position += -c_transform.right * 5.0f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            c_transform.position += c_transform.right * 5.0f * Time.deltaTime;
        }

        //Mouse
        if (Input.GetKey(KeyCode.LeftControl))
        {
            rotationX -= Input.GetAxis("Mouse Y") * sensX * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse X") * sensY * Time.deltaTime;

            c_transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); ;
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, componentLayer))
            {
                UBC.PlaceBlock(hitInfo);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {   
                UBC.CycleSelection();
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); ;
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, componentLayer))
            {
                UBC.PlaceBlock(hitInfo);
            }
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); ;
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, UBC.componentLayer))
                {
                    Debug.Log("name: " + hitInfo.transform.name);

                    UBC.DeleteBlock(hitInfo);
                }
        }

        //Save/Load
        if (Input.GetKey(KeyCode.F5))
        {
            UBC.SaveModel();
        }

        if (Input.GetKey(KeyCode.F9))
        {
            UBC.LoadModel();
        }
    }
}



