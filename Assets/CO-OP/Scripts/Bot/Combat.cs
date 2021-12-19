using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    public class Combat : MonoBehaviour
    {
        #region Variables
        [Header("Bot Scence")]
        public float detectRadius = 10f;
        public Transform sensor;
        public LayerMask detectionLayer;
        public LayerMask checkLayer;
        public bool debug;

        [Header("Gun")]
        public bool canShoot = true;
        public float shootRange = 10f;
        public float damageAmount = 10;
        public float bulletForce = 10;
        public float timeBetweenShots = 0.2f;
        public Vector3 offset;
        public Transform shootPoint;
        public GameObject muzzleFlash;
        private GameObject muzzleFlashInstance;
        [SerializeField] private ParticleSystem fire;
        [SerializeField] private bool readyToShoot = true;
        private AudioSource fireAudio;

        [Header("Timers")]
        public float combatTimer = 5f;
        private float combatCounter = 0;


        [Header("Features")]
        [SerializeField] private bool combatMode = true;
        [SerializeField] private bool killTarget = false;
        public float distanceToKillTarget = 5f;

        [SerializeField] private List<Transform> nearByEnemies = new List<Transform>();
        [SerializeField] private Transform target=null;
        private Robot robot;
        private Collider[] enemies;
        private RaycastHit hit;
        #endregion

        #region Builtin Methods

        private void Start()
        {

            if (muzzleFlash)
            {
                muzzleFlashInstance = Instantiate(muzzleFlash, shootPoint);
                muzzleFlashInstance.transform.localScale = new Vector3(50, 50, 50);
                fire = muzzleFlashInstance.GetComponent<ParticleSystem>();
            }
            robot = GetComponent<Robot>();
            if (!fireAudio)
            {
                fireAudio = GetComponentInChildren<AudioSource>();
            }
        }

        private void Update()
        {
            if (combatMode)
            {
                PerimeterDetection();
                CanSee();

                

                if (canShoot)
                {
                    Debug.DrawRay(shootPoint.position, transform.forward * detectRadius, Color.cyan);

                    if (nearByEnemies != null && target==null)
                    {
                        target = GetClosestEnemy(nearByEnemies);
                    }
                    CheckTarget();
                    if (readyToShoot && target)
                    {
                        Health targetHealth = target.GetComponent<Health>();
                        if(targetHealth)
                        {
                            if (targetHealth.IsAlive)
                                Shoot();
                            else
                            {
                                combatMode = false;
                                target = null;
                            }
                        }
                    }

                    
                }
            }
            else
            {
                combatCounter += Time.deltaTime;
                if (combatCounter >= combatTimer)
                {
                    combatMode = true;
                    combatCounter = 0;

                    if (robot)
                    {
                        robot.canFollow = true;
                    }
                }
            }
        }
        private void OnDrawGizmos()
        {
            if (debug)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(sensor.position + transform.forward, detectRadius);
            }

        }
        #endregion


        #region Custom Methods
        void PerimeterDetection()
        {
            enemies = Physics.OverlapSphere(sensor.position, detectRadius, detectionLayer, QueryTriggerInteraction.Ignore);

            if (enemies != null)
            {
                nearByEnemies.Clear();

                foreach (Collider hit in enemies)
                {

                    Transform temp = null;
                    temp = hit.transform;

                    if (nearByEnemies.Contains(temp))
                    {
                        return;
                    }
                    else
                    {
                        nearByEnemies.Add(temp);
                    }

                }
            }
        }


        void CanSee()
        {
            if (nearByEnemies == null) return;

            Transform[] transforms = nearByEnemies.ToArray();
            foreach (Transform temp in transforms)
            {
                Vector3 direction = temp.position - sensor.position;
                
                if (Physics.Raycast(sensor.position, direction, out hit, detectRadius, checkLayer))
                {

                    if (hit.collider.CompareTag("Enemy"))
                    {
                       // Debug.DrawRay(sensor.position, direction * hit.distance, Color.green);

                    }
                    else
                    {
                        RemoveTarget(temp);
                        //Debug.DrawRay(sensor.position, direction * hit.distance, Color.red);
                    }
                }
            }
        }

        public void RemoveTarget(Transform obj)
        {
            if (nearByEnemies != null)
                nearByEnemies.Remove(obj);
        }


        void Shoot()
        {
            readyToShoot = false;
            //canShoot = false;
            //combatCounter = 0;
            if (target)
            {
                transform.LookAt(target);
                Vector3 direction = target.position - shootPoint.position;
                direction += offset;
                RaycastHit hitInfo;
               
                if (Physics.Raycast(shootPoint.position, direction,out hitInfo, shootRange, detectionLayer,QueryTriggerInteraction.Ignore))
                {
                   // Debug.DrawRay(shootPoint.position, direction * hitInfo.distance, Color.green);
                    
                    muzzleFlashInstance.transform.position = shootPoint.position;
                    if (fire.isPlaying)
                        fire.Stop();

                    fire.Play();

                    if (fireAudio.isPlaying) 
                            fireAudio.Stop();

                    fireAudio.Play();

                    DamageEnemy(hitInfo);

                }
                else
                {
                    //Debug.DrawRay(shootPoint.position, direction * shootRange, Color.red);
                }

            }

                Invoke("ResetShot", timeBetweenShots);
        }

        void ResetShot()
        {
            readyToShoot = true;
        }


        Transform GetClosestEnemy(List<Transform> enemies)
        {
           Transform bestTarget = null;

            if (enemies.Count > 0)
            {
                float closestDistance = detectRadius;
                Vector3 currentPosition = transform.position;
                foreach (Transform potentialTarget in enemies)
                {

                    float distanceToTarget = Vector3.Distance(currentPosition, potentialTarget.position);
                    if (distanceToTarget < closestDistance)
                    {
                        closestDistance = distanceToTarget;
                        bestTarget = potentialTarget.transform;
                    }
                }
            }

            return bestTarget;
        }




        void DamageEnemy(RaycastHit hitInfo)
        {
            Health healthManager = hitInfo.collider.GetComponent<Health>();
            if(healthManager)
            {
                //healthManager.TakeDamage(damageAmount);
                healthManager.TakeDamage(damageAmount, hitInfo.normal, hitInfo.point, bulletForce);
            }
        }

        void CheckTarget()
        {
            if(target)
            {
                if(killTarget)
                {
                    if(robot)
                    {
                        robot.canFollow = false;
                    }
                }
                else
                {
                    float distance = Vector3.Distance(transform.position, target.position);
                    Debug.Log(distance);
                    if(distance>=distanceToKillTarget)
                    {
                        target = null;
                    }

                }
            }
            else
            {
                if (robot)
                {
                    robot.canFollow = true;
                }
            }
        }
        #endregion


    }
}