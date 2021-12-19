using UnityEngine;
using UnityEngine.AI;

namespace Alpha
{
    public class Robot : MonoBehaviour
    {
        #region Variables
        [Header("Follow Properties")]
        public Transform player;
        public bool canFollow = true;
        public float followDistance = 2f;

        private float distanceToPlayer;
        private NavMeshAgent agent;
        private Animator anim;
        #endregion

        #region Builtin Methods
        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (canFollow)
            {
                FollowTarget();
            }
            else
            {
                agent.isStopped = false;
                anim.SetBool("Walking", false);
            }
        }
        #endregion

        #region Custom Methods
        void FollowTarget()
        {

            distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if(distanceToPlayer>=followDistance)
            {
                agent.SetDestination(player.position);
                anim.SetBool("Walking", true);
            }
            else
            {
                agent.isStopped = false;
                anim.SetBool("Walking", false); 
            }
            
        }
        #endregion

    }
}