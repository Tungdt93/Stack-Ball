using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private bool smash;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
            smash = true;
        if (Input.GetMouseButtonUp(0))
            smash = false;
    }

    private void FixedUpdate()
    {
        
    }
}
