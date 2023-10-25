using System.Collections.Generic;
using UnityEngine;

public class BrickSpawn : MonoBehaviour
{
    public Vector3DataList spawnPositionsList;
    public GameObject objectPrefab;
    //public List<GameObject> spawnedObjects = new List<GameObject>();
    public GameManager gameManager;
    public MaterialList materialsList;
    public Dictionary<Material, int> materialHealthMap;
    public BrickData brickData;
    public BrickDataList brickDataList;
    public Material material;
    public float unbreackChance = 0.9f;
    public Brick bricks;

    
    public void SpawnBricks()
    {
        // Spawn an object at each position in the list
        foreach (Vector3 spawnPosition in spawnPositionsList.positions)
        {
            
            // Instantiate the object at the position
            GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            
            
            // Add the object to the list
            gameManager.spawnedObjects.Add(spawnedObject);

            // Get a random BrickData object from the list
            int randomIndex = Random.Range(0, brickDataList.brickData.Count);
            BrickData randomBrickData = brickDataList.brickData[randomIndex];
            
            // Add the BrickData object's materials to the Brick object's material list
            Brick brickComponent = spawnedObject.GetComponent<Brick>();
            if (brickComponent != null)
            {
                brickComponent.materials.AddRange(randomBrickData.material);
                brickComponent.health = randomBrickData.health;
                brickComponent.maxHealth = randomBrickData.maxHealth;
                brickComponent.unbreakable = randomBrickData.unbreakable;
            }
            else
            {
                Debug.LogError("The spawned object does not have a Brick component for materials.");
            }
            // Set the Brick object's health to the number of materials in the BrickData object
            //bricks.Start();
            
            // Set the object's material based on the BrickData object
            Renderer rend = spawnedObject.GetComponent<Renderer>();
            /*if (Random.value < unbreackChance)
            {
                brickData.unbreakable = true;
                rend.material = material;
            } */
            if (rend != null)
            {
                rend.material = randomBrickData.material[0];
            }
            else
            {
                Debug.LogError("The spawned object does not have a Renderer component for material.");
            }
        } 
    }
}
