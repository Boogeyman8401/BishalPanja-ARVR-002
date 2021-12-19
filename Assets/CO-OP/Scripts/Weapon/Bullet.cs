using UnityEngine;
namespace Alpha
{
    public class Bullet : MonoBehaviour
    {
        #region Variables
        [Header("Bullet Properties")]
        public float bulletSpeed = 10f;


        private Rigidbody rb;
        #endregion

        #region Builtin Methods
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
        }
        #endregion

        #region Custom Methods
        public void ApplyForce(Vector3 direction)
        {
            rb.AddForce(direction * bulletSpeed, ForceMode.Impulse);
        }
        #endregion
    }
}