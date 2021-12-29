using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject theStack;
    [SerializeField] private GameObject[] models;
    [SerializeField] private GameObject finishPrefab;
    [SerializeField] private float ySpawnPosition;
    [SerializeField] private float spaceBetweenPlatform;
    [SerializeField] private float rotation;
    [SerializeField] private int numberOfPlatforms;
    [SerializeField] private float rotationChange;
    private int circleModel = 0;
    private int flowerModel = 4;
    private int hexModel = 8;
    private int spikesModel = 12;
    private int squareModel = 16;
    private int selectedModel;

    // Start is called before the first frame update
    void Awake()
    {
        ResetValue();
        GenerateTheStack();
    }

    private void ResetValue()
    {
        selectedModel = 0;
        ySpawnPosition = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ModelSelection() 
    {
        int[] randomModelList = new int [] {circleModel, flowerModel, hexModel, spikesModel, spikesModel};
        int randomIndex = Random.Range(0, randomModelList.Length);
        selectedModel = randomModelList[randomIndex];
    }

     private void LesserModelSelection() 
    {
        int[] randomModelList = new int [] {circleModel};
        int randomIndex = Random.Range(0, randomModelList.Length);
        selectedModel = randomModelList[randomIndex];
    }

    private void GenerateTheStack() 
    {
        LesserModelSelection();
        int randomPrefabIndex = 0;
        GameObject[] platforms = new GameObject[numberOfPlatforms];
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            if (i == 0) 
                randomPrefabIndex = 0;
            else
                randomPrefabIndex = Random.Range(selectedModel, selectedModel + 3 + 1);
            GameObject newPlatform = Instantiate(models[randomPrefabIndex], theStack.transform);
            newPlatform.transform.position = new Vector3(0f, ySpawnPosition, 0f);
            newPlatform.transform.eulerAngles = new Vector3(0f, rotation, 0f);
            //newPlatform.transform.parent = theStack.transform;
            rotation += rotationChange;
            ySpawnPosition -= spaceBetweenPlatform;   
            platforms[i] = newPlatform;  
        }
        GameObject finishPlatform = Instantiate(finishPrefab, theStack.transform);
        finishPlatform.transform.position = new Vector3(0f, ySpawnPosition, 0f);
        //finishPlatform.transform.parent = theStack.transform;
        // GameObject pole = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cylinder), theStack.transform);
        // float subDistance = platforms[0].transform.position.y + finishPlatform.transform.position.y;
        // pole.transform.position = new Vector3(0f, subDistance / 2, 0f);
        // pole.transform.localScale = new Vector3(1f, subDistance, 1f);
        // pole.transform.parent = theStack.transform;
    }
}
