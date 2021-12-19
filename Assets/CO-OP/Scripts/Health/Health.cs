using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace Alpha
{
    public class Health : MonoBehaviour
    {
        #region Variables
        [Header("Health Properties")]
        public float maxHealth = 100f;
        public float timeToDestroy = 2f;

        [Header("Effects")]
        public GameObject mainBody = null;
        public GameObject separateBody = null;
        public bool useGoreEffect = false;
        public GameObject hitEffect;
        public GameObject deathExplosion;

        [Header("Unity Events")]
        public UnityEvent onHealthChange = new UnityEvent();

        private float currentHealth;
        private Rigidbody rb;
        private bool isAlive = true;
        private RagdollHandler ragdollHandler;
        #endregion

        #region Properties
        public bool IsAlive { get { return isAlive; } }
        public bool HasShoot { get; private set; }
        #endregion


        #region Builtin Methods
        private void Awake()
        {
            currentHealth = maxHealth;
            rb = GetComponent<Rigidbody>();
            ragdollHandler = GetComponent<RagdollHandler>();
        }
        #endregion

        #region Custom Methods

        void CheckHealth()
        {
            if (currentHealth <= 0f)
            {
                if (deathExplosion && isAlive)
                {
                    isAlive = false;
                    Instantiate(deathExplosion, transform.position, Quaternion.identity);
                }
                Destroy(this.gameObject, 5f);
            }
        }

        public void TakeDamage(float damageAmount)
        {

            currentHealth -= damageAmount;
            onHealthChange?.Invoke();

            if (hitEffect)
                Instantiate(hitEffect, transform.position + new Vector3(0,2,0), hitEffect.transform.rotation) ;
            if (currentHealth <= 0)
            {
                isAlive = false;
                Destroy(this.gameObject, timeToDestroy);
                currentHealth = 0;
                if (deathExplosion)
                    Instantiate(deathExplosion, transform.position, Quaternion.identity);

            }

        }

        public void TakeDamage(float damageAmount, Vector3 hitDirection, Vector3 hitPoint, float force = 20f)
        {

            currentHealth -= damageAmount;
            onHealthChange?.Invoke();
            if (hitEffect)
                Instantiate(hitEffect, hitPoint, hitEffect.transform.rotation);


            HasShoot = true;


            if (currentHealth <= 0 && isAlive)
            {
                isAlive = false;
                HasShoot = false;
                currentHealth = 0;


                if (rb)
                {
                    rb.isKinematic = false;
                    rb.AddForceAtPosition(-Vector3.up * force, -hitDirection);
                }

                if (ragdollHandler)
                {
                    ragdollHandler.EnableRagdoll();
                }
                else if (useGoreEffect)
                {
                    GoreEffects(hitPoint);
                }
                else
                {
                    Destroy(this.gameObject, timeToDestroy);
                }


                StartCoroutine(Deactivate());

            }


        }

        IEnumerator Deactivate()
        {
            yield return new WaitForSeconds(1f);
            if (rb)
            {
                if (!rb.isKinematic)
                    rb.isKinematic = true;
            }

            yield return new WaitForSeconds(2f);

            if (useGoreEffect)
            {
                Destroy(this.gameObject, timeToDestroy);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        void GoreEffects(Vector3 point)
        {
            if (mainBody)
            {
                mainBody.SetActive(false);
            }
            Instantiate(deathExplosion, point, Quaternion.identity);
            if (separateBody)
            {
                separateBody.SetActive(true);
            }
            else
            {
                Destroy(this.gameObject);
            }

        }

        public void AddHealth(float amount)
        {
            currentHealth += amount;
            onHealthChange?.Invoke();
            
            if (currentHealth>=maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

        public bool CanAddHealth()
        {
            if(currentHealth<maxHealth)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }
}
