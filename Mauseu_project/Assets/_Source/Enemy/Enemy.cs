using UnityEngine;
using UnityEngine.AI;

namespace EnemyAI
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navAgent;
        [SerializeField] private LayerMask groundLayer, playerLayer;
        [SerializeField] private float walkPointRange;
        [SerializeField] private float timeBetweenAttacks;
        [SerializeField] private float attackRange;
        [SerializeField] private int damage;
        [SerializeField] private MonsterData monster;

        private Vector3 _walkPoint;
        private bool walkPointSet;
        private bool alreadyAttacked;
        private bool takeDamage;
        private bool playerInSightRange;
        private bool playerInAttackRange;
        private Vector3 _playerPos;
        private bool isCatched;


        private void Awake()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
            if (!isCatched)
            {
                if (!playerInSightRange && !playerInAttackRange)
                {
                    Patroling();
                }
                else if (playerInSightRange && !playerInAttackRange)
                {
                    ChasePlayer();
                }
                else if (playerInAttackRange && playerInSightRange)
                {
                    AttackPlayer();
                }
                else if (!playerInSightRange && takeDamage)
                {
                    ChasePlayer();
                }
            }
        }

        public bool IsCatched => isCatched;

        public MonsterData GetData() => monster;

        public void IsPlayerInRange(bool value, Vector3 playrePos)
        {
            playerInSightRange = value;
            _playerPos = playrePos;
        }

        private void Patroling()
        {
            if (!walkPointSet)
            {
                SearchWalkPoint();
            }

            if (walkPointSet)
            {
                navAgent.SetDestination(_walkPoint);
            }

            Vector3 distanceToWalkPoint = transform.position - _walkPoint;

            if (distanceToWalkPoint.magnitude < 1.5f)
            {
                walkPointSet = false;
            }
        }

        private void SearchWalkPoint()
        {
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(_walkPoint, out hit, 100f, NavMesh.AllAreas))
            {
                _walkPoint = hit.position;
            }

            if (Physics.Raycast(_walkPoint, -transform.up, 2f, groundLayer))
            {
                walkPointSet = true;
            }
        }

        private void ChasePlayer()
        {
            navAgent.SetDestination(_playerPos);
            navAgent.isStopped = false;
            if (!navAgent.hasPath && !playerInAttackRange)
            {
                playerInSightRange = false;
            }
        }


        private void AttackPlayer()
        {
            navAgent.SetDestination(transform.position);

            if (!alreadyAttacked)
            {
                transform.LookAt(_playerPos);
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);

                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
                {
                    /*
                    Player get damage
                    */
                }
            }
        }


        private void ResetAttack()
        {
            alreadyAttacked = false;
        }

        public void IsCateched()
        {
            isCatched = true;
            navAgent.ResetPath();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}