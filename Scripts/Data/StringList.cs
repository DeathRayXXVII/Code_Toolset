using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "Single Variables/StringData")]
    public class StringList : ScriptableObject
    {
        public List<string> value;

        public string currentValue;
    
        public List<string> stringListObj;
        public int currentLineNumber;

        public string ReturnCurrentLine()
        {
            return stringListObj[currentLineNumber];
        }

        public void ResetToZero()
        {
            currentLineNumber = 0;
        }
    
    
        public void IncrementLineNumber()
        {
            if (currentLineNumber < stringListObj.Count-1)
            {
                currentLineNumber++;
            }
            else
            {
                currentLineNumber = 0;
            }
        }
        public void UseNextValue()
        {
            currentValue = value[0];
        }
    }
}
