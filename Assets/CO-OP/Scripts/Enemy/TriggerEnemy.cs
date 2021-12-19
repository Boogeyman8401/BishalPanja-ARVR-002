using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Alpha
{
    [RequireComponent(typeof(NavMeshAgent), typeof(SphereCollider))]
    public class TriggerEnemy : MonoBehaviour
    {
        #region Variables
        public float triggerRadius = 2f;
        public float chaseDistance = 2f;
        public float attackDistance = 0.5f;
        public float attackTimer = 2f;

        [Header("Damage Colliders")]
        public Damager leftDamager;
        public Damager rightDamager;


        [SerializeField]
        private float distanceToTarget = 0;
        private NavMeshAgent agent;
        private Animator anim;
        private Transform target;
        private Health targetHealth;
        private Rigidbody rb;
        private Health manager;
        private float counter;
        private bool canAttack = false;
        private bool canChase = true;
        
        #endregion

        #region Builtin Methods
        private void Start()
        {
            InitilializeOnStart();

        }

        private void Update()
        {
            Attack();
            Chase();
            if (manager.IsAlive)
            {
                ChasePlayer();
            }
            else
            {
                target = null;
                targetHealth = null;
            }

            
            
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Player") && manager.IsAlive)
            {
                target = other.transform;
                targetHealth = target.GetComponent<Health>();
                canChase = true;
                agent.isStopped = false;
            }
        }
        #endregion

        #region Custom Methods

        void InitilializeOnStart()
        {
            GetComponent<SphereCollider>().radius = triggerRadius;
            manager = GetComponent<Health>();
            anim = GetComponentInChildren<Animator>();
            agent = GetComponent<NavMeshAgent>();
            rb = GetComponent<Rigidbody>();
            chaseDistance += agent.stoppingDistance;
            attackDistance += agent.stoppingDistance;

        }

        void BotControl()
        {
            if(target)
            {
                transform.LookAt(target);
                distanceToTarget = Vector3.Distance(transform.position, target.position);

                if(distanceToTarget<=attackDistance)
                {
                    canChase = false;
                    if(!canAttack)
                        canAttack = true;
                }

                else if(distanceToTarget<=chaseDistance)
                {
                    canChase = true;
                    agent.isStopped = false;
                }
                else if(distanceToTarget>chaseDistance)
                {
                    canChase = false;
                    target = null;
                }
            }
            else
            {
                distanceToTarget = 0;
                agent.isStopped = true;
                anim.SetBool("Walking", false);
            }
        }

        void ChasePlayer()
        {
            if (target && targetHealth.IsAlive)
            {
                distanceToTarget = Vector3.Distance(transform.position, target.position);
                transform.LookAt(target);
                if (distanceToTarget <= chaseDistance)
                {
                    canChase = true;
                    canAttack = false;
                }
                else
                {
                    canChase = false;
                }

                if (distanceToTarget<=attackDistance )
                {
                    canChase = false;
                    canAttack = true;
                }
                else
                {
                    canAttack = false;
                    canChase = true;
                }

                

                if(distanceToTarget>chaseDistance)
                {
                    canChase = false;
                    canAttack = false;
                    target = null;
                    agent.isStopped = true;
                    anim.SetBool("Walking", false);
                }
            }
            else
            {
                canAttack = false;
            }
        }

        void Attack()
        {
            if (canAttack)
            {
                StartCoroutine(StartAttack());   
            }
            else
            {
                ActivateAndDeactivate(false);
                StopCoroutine(StartAttack());
            }
        }

        IEnumerator StartAttack()
        {
            agent.isStopped = true;
            anim.SetBool("Walking", false);
            yield return new WaitForSeconds(attackTimer);
            if (target)
                ActivateAndDeactivate(true);
                anim.SetTrigger("Attack");
        }

        void Chase()
        {
            StopCoroutine(StartAttack());
            if (canChase)
            {
                if (target )
                {
                    agent.isStopped = false;
                    anim.SetBool("Walking", true);
                    agent.SetDestination(target.position);
                }
            }
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
