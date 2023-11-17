using UnityEngine;

namespace Scripts
{
    public class Billboard : MonoBehaviour
    {
        public Transform cam;
        private void LateUpdate()
        {
            transform.LookAt(transform.position + cam.forward);
        }
    }
}
