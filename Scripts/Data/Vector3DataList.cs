using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "Single Variables/Vector3DataList")]
    public class Vector3DataList : ScriptableObject
    {
        public List<vector3Data> vector3Dlist;
        public List<vector3Data> positionList = new List<vector3Data>();
    
        public Vector3[] positions;
        public Vector3 GetRandomPosition()
        {
            return positions[Random.Range(0, positions.Length)];
        }

    }
}
