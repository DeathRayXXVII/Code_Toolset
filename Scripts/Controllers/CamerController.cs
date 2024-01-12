using UnityEngine;

namespace Scripts.Controllers
{
    public class CamerController : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        public float zoomSpeed = 4f;
        public float minZoom = 5f;
        public float maxZoom = 15f;
        public float yawSpeed = 100f;
        private float currentZoom = 10f;
        public float currentInput = 0f;


        private void Update()
        {
            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
            currentInput -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
        }

        private void LateUpdate()
        {
            //transform.position = target.position - offset * currentZoom;
            //transform.LookAt(target.position);
            //transform.RotateAround(target.position, Vector3.up, currentInput);

            transform.rotation = target.rotation;
            Vector3 desiredPosition = target.position - (target.rotation * offset * currentZoom);
            transform.position = desiredPosition;
            transform.RotateAround(target.position, Vector3.up, currentInput);
        }
    }
}
