using UnityEngine;

namespace Alpha
{
    public class MediPack : MonoBehaviour
    {
        #region Variables
        public float healthAmount = 50f;
        #endregion

        #region Builtin Methods
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                Health health = other.GetComponent<Health>();
                if(health)
                {
                    if(health.CanAddHealth())
                    {
                        health.AddHealth(healthAmount);
                        Destroy(this.gameObject);
                    }
                }
            }
        }
        #endregion

       
    }
}