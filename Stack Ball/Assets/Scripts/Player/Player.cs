using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public static event Action OnBreakingPlatform;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerWin;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float maxUpwardSpeed;
    [SerializeField] private bool invincible;
    private bool smash;
    private bool finished, playable;

    public bool Smash { get => smash; set => smash = value; }

    // Start is called before the first frame update
    void Awake()
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
        if (!GameManager.instance.IsGameOver && !GameManager.instance.LevelCompleted) 
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
        if (!GameManager.instance.IsGameOver && !GameManager.instance.LevelCompleted) 
        {
            if (other.gameObject.CompareTag("FinishPlatform")) 
            {
                GameManager.instance.LevelCompleted = true;
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
                    if (other.gameObject.CompareTag("SafePart"))
                    {
                        other.transform.parent.gameObject.GetComponent<PlatformController>().BreakAllParts();    
                    } 
                    else if (other.gameObject.CompareTag("UnsafePart")) 
                    {
                        GameManager.instance.IsGameOver = true;
                        this.gameObject.SetActive(false);
                        UIManager.instance.ShowGameOverPanel(true);
                    }         
                }
            }      
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        GameManager.instance.IncreaseScore();
        if (other.TryGetComponent(out PlatformController platform)) {
            platform.Col.enabled = false;
        }
    }

    private void OnCollisionStay(Collision other) 
    {
        if (!smash)
            rb.velocity = new Vector3(0f, maxUpwardSpeed, 0f);
    }
}
