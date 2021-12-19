using UnityEngine;

namespace Alpha
{
    public class DestroyOverTime : MonoBehaviour
    {
        #region Variables
        public float timer = 2f;
        #endregion

        #region Builtin Methods
        void Start()
        {
            Destroy(this.gameObject, timer);
        }
        #endregion


    }
}
