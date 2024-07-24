using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTank : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private BulletBehavior bB;
    [SerializeField] private float pathUpdateDelay = 0.2f;
    [SerializeField] private float walkPointRange;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private bool stationary;
    [SerializeField] private LayerMask groundLayer, playerLayer;
    [SerializeField] private float sightRange;
    [SerializeField] private float attackRange;

    private float pathUpdateDeadline;
    private float stoppingDistance;
    private bool isStationary = false;
    private Vector3 walkPoint;
    private bool walkPointSet;
    private float timer;
    private bool alreadyAttacked;
    private Vector3 direction;
    private bool isRotating;
    
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        
        if (stationary)
        {
            isStationary = true;
        }
        else
        {
            isStationary = false;
        }
    }
    
    private void Update()
    {
        var position = transform.position;
        bool playerInSightRange = Physics.CheckSphere(position, sightRange, playerLayer);
        bool playerInAttackRange = Physics.CheckSphere(position, attackRange, playerLayer);
        if (!isStationary)
        {
            Patrolling();
            if (playerInSightRange && playerInAttackRange)
            { 
                RotateBarrel();
            }
        }
        else
        {

            if (!CanSeePlayer())
            {
               FindPlayer();
            }
            else
            {
                RotateBarrel();
            }
            
        }
        if (CanSeePlayer())
        {
            isRotating = false;
            //UpdatePath();
                        
            if (timer <= attackSpeed)
            {
                AttackPlayer();
                timer = 0f;
            }
        }
    }
    
    private void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
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
            walkPointSet =  true;
        }
    }
    
    private void FollowPlayer()
    {
        agent.SetDestination(player.position);
    }
    
    private void FindPlayer()
    {
        const int maxReflections = 1;
        int reflections = 0;

        var transform1 = fireTransform.transform;
        Vector3 rayDirection = transform1.up;
        Vector3 nextStartPosition = transform1.position;
        RaycastHit hit;
        while (reflections <= maxReflections)
        {
            if (Physics.Raycast(nextStartPosition, rayDirection, out hit))
            {
                Debug.DrawRay(nextStartPosition, rayDirection * hit.distance, reflections == 0 ? Color.red : Color.green);
                nextStartPosition = hit.point;

                if (hit.transform == player)
                {
                    // If the ray hits the player, perform the desired action
                    isRotating = false;
                    //StopCoroutine(RotateTimer());
                    AttackPlayer();
                    break;
                }
                else
                {
                    // Calculate the reflection vector
                    rayDirection = Vector3.Reflect(rayDirection, hit.normal);
                    reflections++;
                }
            }
            else
            {
                // If the ray does not hit anything, stop the loop
                break;
            }
        }

        if (reflections > maxReflections)
        {
            // If the ray reflected twice without hitting the player, rotate the barrel
            StartCoroutine(RotateTimer());
        }
    }
    
    private void UpdatePath()
    {
        if(Time.time >= pathUpdateDeadline)
        {
            Debug.Log("Updating Path");
            pathUpdateDeadline = Time.time + pathUpdateDelay;
            agent.SetDestination(player.position);
        }
    }
    
    private void AttackPlayer()
    {
        //barrelPrefab.transform.LookAt(player);
        if (!alreadyAttacked)
        {
            direction = fireTransform.forward;
            BulletBehavior bullet = Instantiate(bB, fireTransform.position, fireTransform.rotation);
            bullet.Shoot(direction.normalized);
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
    private IEnumerator RotateTimer()
    {
        yield return new WaitForSeconds(5f);
        RandomRotateBarrel();
    }

    private void RotateBarrel()
    {
        Vector3 targetPosition = player.position;
        var position = barrelPrefab.transform.position;
        targetPosition.y = position.y;
        direction = targetPosition - position;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation *= Quaternion.Euler(0, 90, 0);
        barrelPrefab.transform.rotation = Quaternion.Slerp(barrelPrefab.transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
    }

    private void RandomRotateBarrel()
    {
        if (!isRotating)
        {
            float randomAngle = Random.Range(0f, 360f);
            Quaternion newRotation = Quaternion.Euler(0f, randomAngle, 0f);
            StartCoroutine(SmoothRotate(newRotation));
        }
    }

    private IEnumerator SmoothRotate(Quaternion targetRotation)
    {
        isRotating = true;
        float elapsedTime = 0f;
        float duration = 5f; // Adjust duration as needed for smoother transitions
        Quaternion startRotation = barrelPrefab.transform.rotation;

        while (elapsedTime < duration)
        {
            barrelPrefab.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        barrelPrefab.transform.rotation = targetRotation;
        isRotating = false;
    }
    
    private bool CanSeePlayer()
    {
        direction = player.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out var hit))
        {
            if (hit.transform == player)
            {
                return true;
            }
        }
        return false;
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