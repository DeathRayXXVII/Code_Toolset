using UnityEngine;

[CreateAssetMenu(menuName = "Single Variables/MaterialList")]
public class MaterialList : ScriptableObject
{
    public Material[] materials;

    public Material GetRandomMaterial()
    {
        return materials[Random.Range(0, materials.Length)];
    }
}
