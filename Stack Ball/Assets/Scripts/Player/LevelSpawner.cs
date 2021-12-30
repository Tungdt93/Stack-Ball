using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelSpawner : MonoBehaviour
{
    public static LevelSpawner instance;
    [SerializeField] private GameObject theStack;
    [SerializeField] private GameObject[] models;
    [SerializeField] private GameObject finishPrefab;
    [SerializeField] private float ySpawnPosition;
    [SerializeField] private float spaceBetweenPlatform;
    [SerializeField] private float rotation;
    [SerializeField] private float rotationChange;
    private int numberOfPlatforms = 20;
    private int circleModel = 0;
    private int flowerModel = 4;
    private int hexModel = 8;
    private int spikesModel = 12;
    private int squareModel = 16;
    private int selectedModel;

    public int NumberOfPlatforms { get => numberOfPlatforms; set => numberOfPlatforms = value; }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this) 
            Destroy(this.gameObject);
        else
        {
            instance = this;
        }
            
        GenerateTheStack();
    }

    public void ResetValue()
    {
        foreach (Transform platform in theStack.transform)
        {
            Destroy(platform.gameObject);
        }
        selectedModel = 0;
        ySpawnPosition = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ModelSelection() 
    {
        int[] randomModelList = new int [] {circleModel, flowerModel, hexModel, spikesModel, spikesModel};
        int randomIndex = Random.Range(0, randomModelList.Length);
        selectedModel = randomModelList[randomIndex];
    }

     public void LesserModelSelection() 
    {
        int[] randomModelList = new int [] {circleModel, flowerModel, hexModel};
        int randomIndex = Random.Range(0, randomModelList.Length);
        selectedModel = randomModelList[randomIndex];
    }

    public void GenerateTheStack() 
    {
        numberOfPlatforms = GameManager.instance.NumberOfPlatforms;
        ResetValue();
        LesserModelSelection();
        int randomPrefabIndex = 0;
        GameObject[] platforms = new GameObject[numberOfPlatforms];
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            if (i == 0) 
                randomPrefabIndex = selectedModel;
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
    }
}
