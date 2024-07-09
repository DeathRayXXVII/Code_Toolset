using UnityEngine;
using UnityEngine.AI;

public class EnemyTank : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask groundLayer, playerLayer;
    [SerializeField] private float walkPointRange;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float sightRange;
    [SerializeField] private float attackRange;

    private Vector3 walkPoint;
    private bool walkPointSet;
    private bool alreadyAttacked;
    private bool takeDamage;
    
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private BulletData bulletData;
    [SerializeField] private float attackSpeed = 1f;
    private float timer;
    [SerializeField] private bool stationary = false;
    private bool isPatrolling = false;
    private bool isStationary = false;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        if (stationary)
        {
            isStationary = true;
            isPatrolling = false;
        }
        else
        {
            isStationary = false;
            isPatrolling = true;
        }
    }

    void Update()
    {
        var position = transform.position;
        float distanceToTarget = Vector3.Distance(player.position, position);
        timer += Time.deltaTime;

        bool playerInSightRange = Physics.CheckSphere(position, sightRange, playerLayer);
        bool playerInAttackRange = Physics.CheckSphere(position, attackRange, playerLayer);

        if (isPatrolling)
        {
            if (!playerInSightRange && !playerInAttackRange)
            {
                Patroling();
            }
            else if (playerInSightRange && !playerInAttackRange)
            {
                ChasePlayer();
                RotateBarrel();
            }
            else if(playerInSightRange)
            {
                //ChasePlayer();
                if (!CanSeePlayer())
                {
                    AttemptRicochetShot();
                }
                else
                {
                    RotateBarrel();
                    if (timer >= attackSpeed)
                    {
                        AttackPlayer();
                        timer = 0f;
                    }
                }
            }
        }

        if (!isStationary) return;
        if (!playerInSightRange && playerInAttackRange && distanceToTarget <= attackRange)
        {
            if (!CanSeePlayer())
            {
                AttemptRicochetShot();
            }
            else
            {
                RotateBarrel();
            }
        }
        else if (playerInSightRange && !playerInAttackRange && distanceToTarget <= attackRange)
        {
            if (!CanSeePlayer())
            {
                AttemptRicochetShot();
            }
        }
        else if (playerInSightRange && distanceToTarget <= attackRange)
        {
            if (CanSeePlayer())
            {
                RotateBarrel();
            }
            if (timer >= attackSpeed)
            {
                AttackPlayer();
                timer = 0f;
            }
        }



        /*if (distanceToTarget <= attackRange)
        {
            if (!CanSeePlayer())
            {
                
            }
            else
            {
                RotateBarrel();
            }
        }
        else
        {
            if (!isStationary)
            {
                navAgent.SetDestination(target.position);
            }
        }*/
    }
    
    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            navAgent.SetDestination(walkPoint);
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
            walkPointSet = true;
        }
    }
    
    private void ChasePlayer()
    {
        navAgent.SetDestination(player.position);
    }
    
    private void AttackPlayer()
    {
        navAgent.SetDestination(transform.position);

        //transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Attack code here
            // Add shooting or other attack logic here
            Quaternion correctedRotation = fireTransform.rotation * Quaternion.Euler(0, -90, 0);
            Instantiate(bulletData.shellPrefab, fireTransform.position, correctedRotation);
            bulletData.shellPrefab.transform.rotation = correctedRotation;

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    
    private void ResetAttack()
    {
        alreadyAttacked = false;
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
    
    private bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 direction = player.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.transform == player)
            {
                return true;
            }
        }
        return false;
    }
    
    private void AttemptRicochetShot()
    {
        bool shotSuccessful = false;
        float bestDistance = float.MaxValue;
        Vector3 bestRicochetPoint = Vector3.zero;

        // Step 1 & 2: Optimize raycast angles and improve ricochet point selection
        for (int angle = -60; angle <= 60; angle += 5)
        {
            Vector3 scanDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, scanDirection, out hit, 100f))
            {
                Vector3 ricochetDirection = Vector3.Reflect(scanDirection, hit.normal);
                RaycastHit secondHit;
                if (Physics.Raycast(hit.point, ricochetDirection, out secondHit))
                {
                    float distanceToTarget = Vector3.Distance(secondHit.point, player.position);
                    if (secondHit.transform == player && distanceToTarget < bestDistance)
                    {
                        bestDistance = distanceToTarget;
                        bestRicochetPoint = hit.point;
                        shotSuccessful = true;
                    }
                }
            }
        }

        // Step 3 & 4: Calculate ricochet angles more precisely and verify clear path
        if (shotSuccessful)
        {
            AimAtPoint(bestRicochetPoint);
            AttackPlayer();
        }
    }
    
    private void AimAtPoint(Vector3 point)
    {
        Vector3 direction = point - barrelPrefab.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
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
    
    
    
    /*private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private BulletData bulletData;
    [SerializeField] private float attackSpeed = 1f;
    private float timer;
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private bool stationary = false;
    private bool isStationary = false;

    void Awake()
    {
        if (stationary)
        {
            isStationary = true;
        }
        else
        {
            isStationary = false;
        }
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        timer += Time.deltaTime;
        if (distanceToTarget <= attackRange)
        {
            if (!CanSeePlayer())
            {
                AttemptRicochetShot();
            }
            else
            {
                RotateBarrel();
            }
            if (timer >= attackSpeed)
            {
               Fire();
               timer = 0f; 
            }    
        }
        else
        {
            if (!isStationary)
            {
                agent.SetDestination(target.position);
            }
        }
    }
    
    private void RotateBarrel()
    {
        Vector3 targetPosition = target.position;
        var position = barrelPrefab.transform.position;
        targetPosition.y = position.y;
        Vector3 direction = targetPosition - position;
        
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation *= Quaternion.Euler(0, 90, 0);
        barrelPrefab.transform.rotation = Quaternion.Slerp(barrelPrefab.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    
    private bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 direction = target.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.transform == target)
            {
                return true;
            }
        }
        return false;
    }

    private void Fire()
    {
        // Add shooting or other attack logic here
        Quaternion correctedRotation = fireTransform.rotation * Quaternion.Euler(0, -90, 0);
        Instantiate(bulletData.shellPrefab, fireTransform.position, correctedRotation);
        bulletData.shellPrefab.transform.rotation = correctedRotation;
    }
    
        private void AttemptRicochetShot()
    {
        bool shotSuccessful = false;
        float bestDistance = float.MaxValue;
        Vector3 bestRicochetPoint = Vector3.zero;

        // Step 1 & 2: Optimize raycast angles and improve ricochet point selection
        for (int angle = -60; angle <= 60; angle += 5)
        {
            Vector3 scanDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, scanDirection, out hit, 100f))
            {
                Vector3 ricochetDirection = Vector3.Reflect(scanDirection, hit.normal);
                RaycastHit secondHit;
                if (Physics.Raycast(hit.point, ricochetDirection, out secondHit))
                {
                    float distanceToTarget = Vector3.Distance(secondHit.point, target.position);
                    if (secondHit.transform == target && distanceToTarget < bestDistance)
                    {
                        bestDistance = distanceToTarget;
                        bestRicochetPoint = hit.point;
                        shotSuccessful = true;
                    }
                }
            }
        }

        // Step 3 & 4: Calculate ricochet angles more precisely and verify clear path
        if (shotSuccessful)
        {
            AimAtPoint(bestRicochetPoint);
            Fire();
        }
    }

    private void AimAtPoint(Vector3 point)
    {
        Vector3 direction = point - barrelPrefab.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        barrelPrefab.transform.rotation = Quaternion.Slerp(barrelPrefab.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }*/
}