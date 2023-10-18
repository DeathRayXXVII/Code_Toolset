using System.Collections.Generic;
using UnityEngine;

public class BrickSpawn : MonoBehaviour
{
    public Vector3DataList spawnPositionsList;
    public GameObject objectPrefab;
    public MaterialList materialsList;
    public Dictionary<Material, int> materialHealthMap;

    //public void BrickSpawner()
    void Start()
    {
        // Spawn an object at each position in the list
        foreach (Vector3 spawnPosition in spawnPositionsList.positions)
        {
            // Instantiate the object at the position
            GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            //Instantiate(objectPrefab[num], spawnPosition, Quaternion.identity);

            // Get a random material from the list
            Material material = materialsList.GetRandomMaterial();

            // Set the object's material
            Renderer rend = objectPrefab.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material = material;
            }

            // Set the object's health based on the material
            int health;
            if (materialHealthMap.TryGetValue(material, out health))
            {
                Brick healthComponent = spawnedObject.GetComponent<Brick>();
                if (healthComponent != null)
                {
                    healthComponent.SetHealth(health);
                }
            }
        }
    }
}
