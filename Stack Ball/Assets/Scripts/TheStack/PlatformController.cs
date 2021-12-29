using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float torque;
    private GameObject[] stackParts;
    private Rigidbody[] rigidbodies;
    private MeshRenderer[] meshRenderers;
    private Collider[] colliders;

    // Start is called before the first frame update
    private void OnEnable() 
    {
        GetAllParts();
        GetAllRigidBodies();
        GetAllMeshRenders();
        GetAllColliders();    
        Player.OnBreakingPlatform += BreakAllParts; 
    }

    private void GetAllParts()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        List<Transform> listTranforms = new List<Transform>(transforms);
        listTranforms.RemoveAt(0);
        transforms = listTranforms.ToArray();
        stackParts = new GameObject[transforms.Length];
        for (int i = 0; i < transforms.Length; i++)
        {
            stackParts[i] = transforms[i].gameObject;
        }
    }

    private void GetAllRigidBodies()
    {
        rigidbodies = new Rigidbody[stackParts.Length];
        for (int i = 0; i < stackParts.Length; i++)
        {
            rigidbodies[i] = stackParts[i].GetComponent<Rigidbody>();
        }
    }

    private void GetAllMeshRenders()
    {
        meshRenderers = new MeshRenderer[stackParts.Length];
        for (int i = 0; i < stackParts.Length; i++)
        {
            meshRenderers[i] = stackParts[i].GetComponent<MeshRenderer>();
        }
    }

    private void GetAllColliders()
    {
        colliders = new Collider[stackParts.Length];
        for (int i = 0; i < stackParts.Length; i++)
        {
            colliders[i] = stackParts[i].GetComponent<Collider>();
        }
    }

    public void BreakAllParts()
    {
        if (transform.parent != null)
        {
            transform.parent = null;
        }

        Vector3 forcePoint = transform.position;
        float platformXPosition = transform.position.x;

        float[] partXPositions = new float[meshRenderers.Length];
        for (int i = 0; i < partXPositions.Length; i++)
        {
            partXPositions[i] = meshRenderers[i].bounds.center.x;      
        }

        Vector3[] subDirections = new Vector3[partXPositions.Length];
        for (int i = 0; i < partXPositions.Length; i++)
        {
            subDirections[i] = (platformXPosition - partXPositions[i] < 0) ? Vector3.right : Vector3.left;
        }

        Vector3[] directions = new Vector3[subDirections.Length];
        for (int i = 0; i < directions.Length; i++)
        {
            directions[i] = subDirections[i].normalized;
        }

        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = false;
            rigidbodies[i].AddForceAtPosition(directions[i] * force, forcePoint, ForceMode.Impulse);
            rigidbodies[i].AddTorque(Vector3.left * torque);
            rigidbodies[i].velocity = Vector3.down;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }

        StartCoroutine(RemovePlatform());
        GameManager.instance.IncreaseScore();
    }

    IEnumerator RemovePlatform()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
