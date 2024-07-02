using Scripts.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Scripts
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header ("Player Attack")]
        public InputActionReference attackControl;
        public GameObject weapon;
        public FloatData damage;
        public float attackRange;
        public float attackRate;
        private float lastAttackTime;
        public LayerMask enemyLayer;
        public EnemyHealth enemyHealth;
        private AudioSource source;
        public AudioClip marker;
        private Animator animator;
        private PlayerController playerController;
        public UnityEvent attackEvent;

        void Start()
        {
            weapon.SetActive(false);
            source = GetComponent<AudioSource>();
            playerController = GetComponent<PlayerController>();
            animator = GetComponentInParent<Animator>();
        }
        private void Update()
        {
            if (attackControl.action.triggered && playerController.isGrounded)
            {
                animator.SetTrigger("IsAttacking");
                weapon.SetActive(true);
                attackEvent.Invoke();
            }
        }
        public void Attack()
        {
            if (Time.time - lastAttackTime > attackRate)
            {
                lastAttackTime = Time.time;
                source.PlayOneShot(marker);
                Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
                foreach (Collider enemy in hitEnemies)
                {
                    Debug.Log("Attack");
                    enemyHealth = enemy.gameObject.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(damage);
                    }
                }
            }
            weapon.SetActive(false);
        }
    }
}

