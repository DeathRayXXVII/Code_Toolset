using System.Collections.Generic;
using Scripts.Data;
using UnityEngine;

public class BrickSpawn : MonoBehaviour
{
    public Vector3DataList spawnPositionsList;
    public GameObject objectPrefab;
    public GameManager gameManager;
    public Dictionary<Material, int> materialHealthMap;
    public BrickDataList brickDataList;
    public float unbreakableChance = 0.03f;
    float skipChance = 0.2f;

    
    public void SpawnBricks()
{
    // Spawn an object at each position in the list
    foreach (Vector3 spawnPosition in spawnPositionsList.positions)
    {
        // Skip this position with a certain probability
        if (Random.value < skipChance)
        {
            continue;
        }

        // Instantiate the object at the position
        GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.Euler(0,90,0));

        // Add the object to the list
        gameManager.spawnedObjects.Add(spawnedObject);

        // Get a random BrickData object from the list
        int randomIndex = Random.Range(0, brickDataList.brickData.Count);
        BrickData randomBrickData = brickDataList.brickData[randomIndex];
        
        Renderer rend = spawnedObject.GetComponent<Renderer>();
        // Add the BrickData object's materials to the Brick object's material list
        Brick brickComponent = spawnedObject.GetComponent<Brick>();
        
        if (brickComponent != null)
        {
            // Set the object as unbreakable with a certain probability
            if (Random.value < unbreakableChance)
            {
                brickComponent.unbreakable = true;
                rend.material = randomBrickData.unbreakableMaterial;
            }
            else
            {
                brickComponent.materials.AddRange(randomBrickData.material);
                brickComponent.health = randomBrickData.health;
                brickComponent.maxHealth = randomBrickData.maxHealth;
                brickComponent.unbreakable = randomBrickData.unbreakable;
                rend.material = randomBrickData.material[0];
            }
        }
        else
        {
            Debug.LogError("The spawned object does not have a Brick component for materials.");
        }

        // Set the object's material based on the BrickData object
        if (rend != null && brickComponent.unbreakable)
        {
            rend.material = randomBrickData.unbreakableMaterial;
        }
        else if (rend != null && brickComponent.unbreakable == false)
        {
            rend.material = randomBrickData.material[0];
        }
        else
        {
            Debug.LogError("The spawned object does not have a Renderer component for material.");
        }
    }
}
    public void DeactivateBricks()
    {
        foreach (GameObject obj in gameManager.spawnedObjects)
        {
            obj.SetActive(false);
        }
    }
    public void ActivateBricks()
    {
        foreach (GameObject obj in gameManager.spawnedObjects)
        {
            obj.SetActive(true);
        }
    }
    public void DestroyBricks()
    {
        foreach (GameObject obj in gameManager.spawnedObjects)
        {
            Destroy(obj);
        }
        gameManager.spawnedObjects.Clear();
    }
}