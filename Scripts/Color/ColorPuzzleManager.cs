using System.Collections.Generic;
using Scripts;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class ColorPuzzleManager : MonoBehaviour
{
    public List<Transform> objectList;
    public List<GameObject> matchList;
    public List<Transform> positionList;
    public UnityEvent AllObjectsMatched;
    

    public void CheckAllObjectsMatched()
    {
        foreach (var obj in matchList)
        {
            MatchBehaviour matchBehaviour = obj.GetComponent<MatchBehaviour>();
            if (matchBehaviour != null && !matchBehaviour.isMatched)
            {
                return;
            }
        }

        // If we've made it this far, all objects have a match
        AllObjectsMatched?.Invoke();
    }

    private void Start()
    {
        SetObjectPositions();
    }

    public void SetObjectPositions()
    {
        // Check for list size mismatch
        if (objectList.Count != positionList.Count)
        {
            Debug.LogError("Size of objectList and positionList do not match");
            return;
        }

        // Fisher-Yates shuffle for the position list
        for (int i = positionList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (positionList[i], positionList[randomIndex]) = (positionList[randomIndex], positionList[i]);
        }

        // Assign each object a unique position from the shuffled list
        for (int i = 0; i < objectList.Count; i++)
        {
            objectList[i].position = positionList[i].position;
        }
    }
}