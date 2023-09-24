using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Single Variables/Vector3DataList")]
public class Vector3DataList : ScriptableObject
{
    public List<vector3Data> vector3Dlist;
    public List<vector3Data> positionList = new List<vector3Data>();

}
