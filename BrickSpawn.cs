using System.Collections.Generic;
using UnityEngine;

public class BrickSpawn : MonoBehaviour
{
    public Vector3DataList spawnPositionsList;
    public GameObject objectPrefab;
    public MaterialList materialsList;
    public Dictionary<Material, int> materialHealthMap;

    
    public void SpawnBricks()
    {
        // Spawn an object at each position in the list
        foreach (Vector3 spawnPosition in spawnPositionsList.positions)
        {
            // Instantiate the object at the position
            GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

            // Get a random material from the list
            Material material = materialsList.GetRandomMaterial();

            // Set the object's material
            Renderer rend = spawnedObject.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material = material;
            }
            

            // Set the object's health based on the material
             if (materialHealthMap.TryGetValue(material, out int health))
            {
                Brick healthComponent = spawnedObject.GetComponent<Brick>();
                if (healthComponent == null)
                {
                    Debug.LogError("The spawned object does not have a Brick component for health.");
                }
                else
                {
                    healthComponent.SetHealth(health);
                    Debug.LogError("Health set to: " + health);
                }
            }
            else
            {
                Debug.LogError("No health value found for the material: " + material.name);
            }
        }
    }
}
