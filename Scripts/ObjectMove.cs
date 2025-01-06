using UnityEngine;

namespace Scripts
{
    public class ObjectMove : MonoBehaviour
    {
        public float speed;
        private float moveVelocity;
        private Rigidbody rigidbody;
        public Animator anim;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            //anim = GetComponent<Animation>();
            anim.SetBool("Spin",true);
        }
    

        private void Update()
        {
            rigidbody.linearVelocity = new Vector2(-speed, rigidbody.linearVelocity.y);
            moveVelocity = speed;
        }
    }
}
