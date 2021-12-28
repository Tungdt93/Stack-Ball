using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    private GameObject[] stackParts = null;
    //private GameObject[] stackParts = null;
    private Rigidbody[] rigidbodies;
    private MeshRenderer[] meshRenderers;
    private Collider[] colliders;

    // Start is called before the first frame update
    private void OnEnable() 
    {
        GetAllParts();  
    }

    private void GetAllParts()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
