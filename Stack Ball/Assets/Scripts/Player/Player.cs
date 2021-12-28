using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float maxUpwardSpeed;
    private bool smash, invincible;

    // Start is called before the first frame update
    void Start()
    {
        invincible = false;
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
        if (Input.GetMouseButton(0)) 
        {
            smash = true;
            rb.velocity = new Vector3(0f, speed, 0f) * Time.deltaTime;
        }        
        if (rb.velocity.y > maxUpwardSpeed)
            rb.velocity = new Vector3(rb.velocity.x, maxUpwardSpeed, rb.velocity.z);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (!smash)
            rb.velocity = new Vector3(0f,maxUpwardSpeed, 0f);
        else 
        {
            if (invincible) 
            {
                if (other.gameObject.CompareTag("SafePart") || other.gameObject.CompareTag("UnsafePart")) 
                {
                    Destroy(other.transform.parent.gameObject);
                } 
            }
            else 
            {
                if (other.gameObject.CompareTag("SafePart")) 
                {
                    other.transform.parent.gameObject.GetComponent<PlatformController>().BreakAllParts();
                }
                else if (other.gameObject.CompareTag("UnsafePart"))
                 {
                    Debug.Log("U Die");
                }   
            }
           
        }
    }

    private void OnCollisionStay(Collision other) 
    {
        if (!smash)
            rb.velocity = new Vector3(0f,maxUpwardSpeed, 0f);
    }
}
