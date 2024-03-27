using UnityEngine;

[CreateAssetMenu (fileName = "Lighting Data", menuName = "Single Variables/Lighting Data")]
public class LightingData : ScriptableObject
{
    public Gradient ambientColor;
    public Gradient directionalColor;
    public Gradient fogColor;
}
