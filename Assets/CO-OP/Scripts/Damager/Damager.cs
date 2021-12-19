using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    public class Damager : MonoBehaviour
    {
        public float damage = 25;
        public bool canDamage = false;

        private void OnTriggerExit(Collider other)
        {
            if (canDamage)
            {
                if (other.CompareTag("Player"))
                {
                    Health healthManager;
                    
                    if (other.TryGetComponent(out healthManager))
                    {
                        healthManager.TakeDamage(damage);
                        canDamage = false;
                    }

                }
            }
        }
    }
}
