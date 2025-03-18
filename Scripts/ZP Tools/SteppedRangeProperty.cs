// - - Created by Zac Peterson
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Attributes should not be in the UNITY_EDITOR assembly directive block
// Custom attribute to create a range with a step value
public class SteppedRangeAttribute : PropertyAttribute
{
    public float min;
    public float max;
    public float step;

    public SteppedRangeAttribute(float rangeMin, float rangeMax, float step)
    {
        min = rangeMin;
        max = rangeMax;
        this.step = step;
    }
}

#if UNITY_EDITOR
// Custom drawer for the SteppedRangeAttribute
[CustomPropertyDrawer(typeof(SteppedRangeAttribute))]
public class SteppedRangeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SteppedRangeAttribute stepAttribute = attribute as SteppedRangeAttribute;

        if (property.propertyType == SerializedPropertyType.Float)
        {
            EditorGUI.Slider(position, property, stepAttribute!.min, stepAttribute.max, label);
            property.floatValue = Mathf.Round(property.floatValue / stepAttribute.step) * stepAttribute.step;
        }
        else if (property.propertyType == SerializedPropertyType.Integer)
        {
            EditorGUI.IntSlider(position, property, (int)stepAttribute!.min, (int)stepAttribute.max, label);
            property.intValue = Mathf.RoundToInt(property.intValue / stepAttribute.step) * (int)stepAttribute.step;
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use Step with float.");
        }
    }
}
#endif