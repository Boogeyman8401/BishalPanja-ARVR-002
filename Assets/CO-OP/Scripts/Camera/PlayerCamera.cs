using UnityEngine;

namespace Alpha
{
    public class PlayerCamera : MonoBehaviour
    {
        public Transform target;
        private Vector3 offset;

        private void Start()
        {
            if (target)
                offset = transform.position - target.position;
        }

        private void LateUpdate()
        {
            if (target)
                transform.position = target.position + offset;

        }
    }
}
