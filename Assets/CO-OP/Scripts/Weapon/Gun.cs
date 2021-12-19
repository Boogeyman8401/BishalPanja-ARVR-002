using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Alpha
{
    public class Gun : MonoBehaviour
    {
        #region Variables
        [Header("Gun Properites")]
        public GameObject muzzlePoint;
        [SerializeField]
        private Transform player;

        //bullet 
        public GameObject bullet;
        public bool useBullet = true;
        public bool useRayCastToDamage = true;
        [Space]
        [Header("Audio")]
        public AudioSource shootAudio;
        public AudioSource reloadAudio;

        //Gun stats
        [Header("Gun Stats")]
        public float gunDamage = 20f;
        public float bulletForce = 10f;
        public float gunRange = 2f;
        public float timeBetweenShooting, reloadTime, timeBetweenShots;
        public int magazineSize, bulletsPerTap;
        public int ammoCapacity = 100;
        public bool allowButtonHold;
        public LayerMask enemyLayer;
        public bool allowInvoke = true;

        //Graphics
        [Header("Graphics")]
        public GameObject muzzleFlash;
        public LineRenderer laserLight;
        public TrailRenderer bulletTrail;
        public GameObject aimRadicle;

        public bool useDebug = false;

        //bools
        bool shooting, readyToShoot, reloading;
        private bool isShooting=false;

        public float shootTimer = 1f;
        private float counter = 0;
        private int ammoNeeded;
        private int bulletsLeft, bulletsShot;
        private bool useLaserLight = true;
        private float effectsDisplayTime = 0.5f;
        private float timer;
        private ParticleSystem flash;
        private LineRenderer gunRenderer;
        private GameObject muzzleFlashInstance;
        private RaycastHit hit;
        private InputManager inputManager;
        private Ray ray;

        public int BulletsLeft { get { return bulletsLeft; } }
        public bool Reloading { get { return reloading; } }
        public bool IsShooting { get { return isShooting; } }
        #endregion

        #region Builtin Methods
        private void Awake()
        {
            
            bulletsLeft = magazineSize;
            readyToShoot = true;
            gunRenderer = GetComponentInChildren<LineRenderer>();
            muzzleFlashInstance = Instantiate(muzzleFlash, transform);
            flash = muzzleFlashInstance.GetComponent<ParticleSystem>();
            inputManager = transform.root.GetComponent<InputManager>();
        }
        private void Update()
        {
            timer += Time.deltaTime;
            GetInput();
            LaserLight();
            if (timer >= timeBetweenShooting * effectsDisplayTime)
            {
                gunRenderer.enabled = false;
            }

            counter += Time.deltaTime;

            if (counter > shootTimer)
            {
                isShooting = false;
            }
        }

        void OnDrawGizmos()
        {
            if (useDebug)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(muzzleFlash.transform.localPosition, player.transform.forward * gunRange);
               
            }
        }
        #endregion

        #region Custom Methods

        private void GetInput()
        {
            
            if (allowButtonHold) shooting = inputManager.Shoot;
            else shooting = inputManager.Shoot;

             
            if (inputManager.Reload && ammoCapacity>0 &&bulletsLeft < magazineSize && !reloading)
            {
                Reload();
            }

            //Reload automatically when trying to shoot without ammo
            if (readyToShoot && ammoCapacity > 0 && shooting && !reloading && bulletsLeft <= 0)
            { 
                Reload();
            }

            
            if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
            {
                bulletsShot = 0;
                isShooting = true;
                useLaserLight = false;
                counter = 0;
                Shoot();
            }
            else
            {
                useLaserLight = true;
            }

        }

        void Shoot()
        {
            readyToShoot = false;
            timer = 0;
            isShooting = true;

            shootAudio.Stop();
            shootAudio.Play();

            //gunRenderer.enabled = true;
            //gunRenderer.SetPosition(0, muzzlePoint.transform.position);
            if (DidRayCastHit())
            {

                //gunRenderer.SetPosition(1, hit.point);
                TrailRenderer trail = Instantiate(bulletTrail, muzzlePoint.transform.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit));
                if (useRayCastToDamage)
                {
                    DealDamage();
                }

            }
            else
            {
                //gunRenderer.SetPosition(1, ray.origin + ray.direction * gunRange);
            }


            if (useBullet)
            {
                GameObject currentBullet = Instantiate(bullet, muzzlePoint.transform.position, Quaternion.identity); //store instantiated bullet in currentBullet
                currentBullet.GetComponent<Bullet>().ApplyForce(player.transform.forward);
            }


            
            if (muzzleFlash != null)
            {
                muzzleFlashInstance.transform.position = muzzlePoint.transform.position;
                flash.Play();
            }


            bulletsLeft--;
            bulletsShot++;

            
            if (allowInvoke)
            {
                Invoke("ResetShot", timeBetweenShooting);
                allowInvoke = false;
            }

            
            if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
                Invoke("Shoot", timeBetweenShots);
        }

        void ResetShot()
        {
            
            readyToShoot = true;
            allowInvoke = true;
        }

        void Reload()
        {
            counter = 0;
            useLaserLight = false;
            reloading = true;
            Invoke("ReloadFinished", reloadTime); 
            reloadAudio.Play();
            isShooting = false;
        }

        void ReloadFinished()
        {
            if (ammoCapacity >= magazineSize)
            {
                ammoNeeded = magazineSize - bulletsLeft;
                ammoCapacity -= ammoNeeded;

                bulletsLeft += ammoNeeded;
            }
            else if(ammoCapacity>0)
            {
                ammoNeeded = magazineSize - bulletsLeft;

                if(ammoNeeded<ammoCapacity)
                {
                    ammoCapacity -= ammoNeeded;
                    bulletsLeft += ammoNeeded;
                }
                else
                {
                    bulletsLeft += ammoCapacity;
                    ammoCapacity = 0;
                }
            }
            
            reloading = false;
            reloadAudio.Stop();
            useLaserLight = true;
        }
        
        
        void LaserLight()
        {
            if(useLaserLight)
            {
                laserLight.enabled = true;
                
                laserLight.SetPosition(0, muzzlePoint.transform.position);
                if (DidRayCastHit())
                {
                    aimRadicle.SetActive(true);
                    laserLight.SetPosition(1, hit.point);
                    aimRadicle.transform.position = hit.point;
                    //aimRadicle.transform.rotation = Quaternion.LookRotation(hit.normal,Vector3.right);
                    //aimRadicle.transform.LookAt(player.transform);
                    
                }
                else
                {
                    laserLight.SetPosition(1, ray.origin + ray.direction * gunRange);
                    //aimRadicle.transform.rotation = Quaternion.Euler(-player.transform.forward);
                    aimRadicle.SetActive(false);
                }
            }
            else
            {
                laserLight.enabled = false;
                
            }
        }

        bool DidRayCastHit()
        {
            ray = new Ray(muzzlePoint.transform.position, player.transform.forward);
            if (Physics.Raycast(ray, out hit, gunRange, enemyLayer.value,QueryTriggerInteraction.Ignore))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        void DealDamage()
        {
            Health healthManager = hit.transform.GetComponent<Health>();
            
            if(healthManager)
            {
                healthManager.TakeDamage(gunDamage, hit.normal,hit.point, bulletForce);
            }

        }

        IEnumerator SpawnTrail(TrailRenderer trail,RaycastHit hit)
        {
            float time = 0;
            Vector3 startPosition = trail.transform.position;

            while (time < 1)
            {
                trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
                time += Time.deltaTime / trail.time;

                yield return null;
            }

            trail.transform.position = hit.point;
            Destroy(trail.gameObject, trail.time);
        }

        #endregion
    }
}
