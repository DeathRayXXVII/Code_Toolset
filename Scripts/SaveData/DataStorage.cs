using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(fileName = "DataStorage", menuName = "Utilities/Data Storage Object")]
    public class DataStorage : ScriptableObject
    {
        public ScriptableObject data;
        public List<ScriptableObject> listData;

        private void GetOnRaise()
        {
            GetData(data);
        }
    
        private void SetOnRaise()
        {
            SetData(data);
        }
        
        public void DeleteDataList()
        {
            foreach (var obj in listData)
            {
                PlayerPrefs.DeleteKey(obj.name);
            }
        }

        public void SetListData()
        {
            foreach (var obj in listData)
            {
                SetData(obj);
            }
        }

        public void GetListData()
        {
            foreach (var obj in listData)
            {
                GetData(obj);
            }
        }
    
        public void SetData(ScriptableObject obj)
        {
            if (obj == null) return;
            PlayerPrefs.SetString(obj.name, JsonUtility.ToJson(obj));
        }
    
        public void SetData(MonoBehaviour obj)
        {
            if (obj == null) return;
            PlayerPrefs.SetString(obj.name, JsonUtility.ToJson(obj));
        }

        public void SetData()
        {
            if (data == null) return;
            PlayerPrefs.SetString(data.name, JsonUtility.ToJson(data));
        }
    
        public void GetData(ScriptableObject obj)
        {
            if (obj == null) return;
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(obj.name)))
                JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(obj.name), obj);
        }
    
        public void GetData(MonoBehaviour obj)
        {
            if (obj == null) return;
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(obj.name)))
                JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(obj.name), obj);
        }

        public void GetData()
        {
            if (data == null) return;
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(data.name)))
                JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(data.name), data);
        }
    }
}
