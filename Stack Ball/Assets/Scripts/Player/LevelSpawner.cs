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
    [SerializeField] private float numberOfPlatforms;
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
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            if (i == 0) 
                randomPrefabIndex = 0;
            else
                randomPrefabIndex = Random.Range(selectedModel, selectedModel + 3 + 1);
            var newPlatform = Instantiate(models[randomPrefabIndex]);
            newPlatform.transform.position = new Vector3(0f, ySpawnPosition, 0f);
            newPlatform.transform.eulerAngles = new Vector3(0f, rotation, 0f);
            newPlatform.transform.parent = theStack.transform;
            rotation += rotationChange;
            ySpawnPosition -= spaceBetweenPlatform;     
        }
        var finishPlatform = Instantiate(finishPrefab);
        finishPlatform.transform.position = new Vector3(0f, ySpawnPosition, 0f);
        finishPlatform.transform.parent = theStack.transform;
    }
}
