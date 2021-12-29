using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject platformPrefab;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            var platform = Instantiate(platformPrefab); 
            platform.transform.position = new Vector3(0f, i - 0.1f, 0f);
            platform.transform.parent = transform.parent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
