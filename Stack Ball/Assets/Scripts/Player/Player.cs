using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action OnBreakingPlatform;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerWin;

    public static Player instance;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float maxUpwardSpeed;
    private bool smash, invincible;
    private bool finished, playable;

    public bool Smash { get => smash; set => smash = value; }

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this) 
            Destroy(this.gameObject);
        else
            instance = this;
        invincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsGameOver) 
            playable = false;
        if (playable)
            {
                if (Input.GetMouseButton(0))
                smash = true;
                if (Input.GetMouseButtonUp(0))
                smash = false;
            }    
    }
    private void FixedUpdate()
    {
        if (finished)
            rb.velocity = Vector3.zero;
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
        if (other.gameObject.CompareTag("FinishPlatform")) 
        {
            finished = true;
            UIManager.instance.ShowLevelCompletePanel(true);
        }

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
                if (other.gameObject.CompareTag("Safepart"))
                {
                    other.transform.parent.gameObject.GetComponent<PlatformController>().BreakAllParts();    
                } 
                else if (other.gameObject.CompareTag("UnsafePart")) 
                {
                    
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
