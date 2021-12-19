using UnityEngine;
using UnityEngine.AI;

namespace Alpha
{
    public enum AIState
    {
        IDLE, PATROLLING, CHASING, ATTACK, AGGRESSIVECHASE
    };

    [RequireComponent(typeof(NavMeshAgent))]
    public class PatrolEnemy : MonoBehaviour
    {
        [Header("Enemy Properties")]
        public Transform target;
        public float minDistance = 0.5f;
        public float chaseDistance = 1f;
        public float attackDistance = 0.5f;
        public float attackTimer = 1f;

        [Space]
        [Header("WayPoints")]
        public bool useRandomPath = false;
        public Transform[] wapoints;

        [Header("Damage Colliders")]
        public Damager leftDamager;
        public Damager rightDamager;

        [Space]
        [Header("Features")]
        public bool useAlert = false;
        [SerializeField] private bool canAttack = true;
        [SerializeField] private float distanceToPlayer = 0;
        [SerializeField] private AIState state = AIState.PATROLLING;

        private NavMeshAgent agent;
        [SerializeField] private Vector3 targetDestination;
        private Animator anim;
        private Rigidbody rb;
        private Health manager;
        private int indexTracker = -1;
        private float counter = 0;
        #region Builtin Methods
        private void Start()
        {

            InitilializeOnStart();
        }



        private void Update()
        {
            counter += Time.deltaTime;
            if (!target)
            {
                agent.isStopped = true;
                anim.SetBool("Walking", false);
                return;
            }
            if (manager.IsAlive)
            {
                if (useAlert)
                {
                    if (manager.HasShoot)
                    {
                        state = AIState.AGGRESSIVECHASE;
                        useAlert = false;
                    }
                }
                BotMovement();

            }
            else
            {
                agent.isStopped = true;
            }
        }

        #endregion

        #region Custom Methods
        void InitilializeOnStart()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody>();
            targetDestination = GetWaypointPosition();
            minDistance += agent.stoppingDistance;
            chaseDistance += agent.stoppingDistance;
            attackDistance += agent.stoppingDistance;
            manager = GetComponent<Health>();
        }

        public void BotMovement()
        {
            distanceToPlayer = Vector3.Distance(transform.position, target.position);

            switch (state)
            {
                case AIState.IDLE:
                    if (counter > attackTimer)
                    {
                        canAttack = true;
                        state = AIState.PATROLLING;
                    }
                    break;

                case AIState.PATROLLING:

                    agent.SetDestination(targetDestination);
                    float distance = Vector3.Distance(transform.position, targetDestination);
                    //Debug.Log(distance);
                    //useAlert = true;

                    if (distanceToPlayer <= chaseDistance)
                    {
                        state = AIState.CHASING;
                    }
                    else if (distanceToPlayer <= attackDistance)
                    {
                        state = AIState.ATTACK;
                    }

                    if (distance <= minDistance)
                    {
                        targetDestination = GetWaypointPosition();
                    }

                    anim.SetBool("Walking", true);
                    break;

                case AIState.CHASING:
                    //distanceToPlayer = Vector3.Distance(transform.position, target.position);
                    if (distanceToPlayer > chaseDistance)
                    {
                        useAlert = true;
                        state = AIState.PATROLLING;
                    }
                    else if (distanceToPlayer <= attackDistance)
                    {
                        state = AIState.ATTACK;
                    }
                    agent.SetDestination(target.position);
                    anim.SetBool("Walking", true);
                    break;
                case AIState.ATTACK:
                    if (canAttack)
                    {
                        transform.LookAt(target);
                        ActivateAndDeactivate(true);
                        anim.SetTrigger("Attack");
                        canAttack = false;
                        counter = 0;
                    }
                    state = AIState.IDLE;
                    break;
                case AIState.AGGRESSIVECHASE:
                    agent.SetDestination(target.position);
                    anim.SetBool("Walking", true);

                    if (distanceToPlayer <= attackDistance)
                    {
                        state = AIState.ATTACK;
                    }

                    //
                    if (distanceToPlayer > chaseDistance)
                    {
                        useAlert = true;
                        state = AIState.PATROLLING;
                    }

                    break;
                default:
                    break;
            }
        }

        Vector3 GetWaypointPosition()
        {
            if (useRandomPath)
            {
                return GetRandomPosition();
            }
            else
            {
                return GetNextPatrolPoint();
            }
        }

        Vector3 GetNextPatrolPoint()
        {
            Vector3 randPos;
            indexTracker++;
            if (indexTracker >= wapoints.Length)
            {
                indexTracker = 0;
            }

            randPos = wapoints[indexTracker].position;
            return randPos;

        }

        Vector3 GetRandomPosition()
        {
            float x = Random.Range(wapoints[0].position.x, wapoints[1].position.x);
            float z = Random.Range(wapoints[1].position.z, wapoints[2].position.z);

            Vector3 randPos = new Vector3(x, 0, z);
            return randPos;
        }


        void ActivateAndDeactivate(bool damage)
        {
            if (leftDamager)
                leftDamager.canDamage = damage;
            if (rightDamager)
                rightDamager.canDamage = damage;
        }
        #endregion
    }
}
