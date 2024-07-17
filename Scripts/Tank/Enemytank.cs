using UnityEngine;
using UnityEngine.AI;

public class EnemyTank : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private BulletData bulletData;
    [SerializeField] private BulletBehavior bB;
    [SerializeField] private float speed;
    [SerializeField] private float walkPointRange;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float roationSpeed;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private bool stationary;
    [SerializeField] private LayerMask groundLayer, playerLayer;
    [SerializeField] private float sightRange;
    [SerializeField] private float attackRange;

    private Vector3 walkPoint;
    private bool walkPointSet;
    private float timer;
    
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        
        if (stationary)
        {
            agent.updatePosition = false;
            //agent.updateRotation = false;
        }
    }
    
    private void Update()
    {
        var position = transform.position;
        float distanceToTarget = Vector3.Distance(player.position, position);
        bool playerInSightRange = Physics.CheckSphere(position, sightRange, playerLayer);
        bool playerInAttackRange = Physics.CheckSphere(position, attackRange, playerLayer);
        if (!stationary)
        {
           if (!playerInSightRange && !playerInAttackRange)
           {
               if (!walkPointSet)
               {
                   SearchWalkPoint();
               }
               else
               {
                   agent.SetDestination(walkPoint);
               }
           }
                   
           if (playerInSightRange && !playerInAttackRange)
           {
               FollowPlayer();
               RotateBarrel();
           }
        }
        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
            RotateBarrel();
        } 
    }
    
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        var position = transform.position;
        walkPoint = new Vector3(position.x + randomX, position.y, position.z + randomZ);
        
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            walkPointSet = true;
        }
    }
    
    private void FollowPlayer()
    {
        agent.SetDestination(player.position);
        //barrelPrefab.transform.LookAt(player);
    }
    
    private void AttackPlayer()
    {
        agent.SetDestination(player.position);
        //barrelPrefab.transform.LookAt(player);
        
        if (Vector3.Distance(transform.position, player.position) <= attackSpeed)
        {
            if (timer >= attackSpeed)
            {
                Vector3 direction = fireTransform.up;
                BulletBehavior bullet = Instantiate(bB, fireTransform.position, fireTransform.rotation);
                bullet.Shoot(direction.normalized);
                timer = 0f;
            }
        }
    }

    private void RotateBarrel()
    {
        Vector3 targetPosition = player.position;
        var position = barrelPrefab.transform.position;
        targetPosition.y = position.y;
        Vector3 direction = targetPosition - position;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation *= Quaternion.Euler(0, 90, 0);
        barrelPrefab.transform.rotation = Quaternion.Slerp(barrelPrefab.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var position = transform.position;
        Gizmos.DrawWireSphere(position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(position, sightRange);
    }
}