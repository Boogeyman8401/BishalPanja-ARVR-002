using UnityEngine;
using UnityEngine.UI;

namespace Alpha
{
    public class AmmoUI : MonoBehaviour
    {
        #region Variables
        public Text ammoCounterImage;
        public Gun gun1;
        private string ammoCount="";
        #endregion

        #region Builtin Methods
        private void Update()
        {
            UpdateAmmo();
        }
        #endregion

        #region Custom Methods
        void UpdateAmmo()
        {
            if(gun1)
            {
                ammoCount = gun1.BulletsLeft.ToString() + "/" + gun1.magazineSize.ToString() + "   :" + gun1.ammoCapacity.ToString();
            }

            if(ammoCounterImage)
            {
                ammoCounterImage.text = ammoCount;
            }
        }
        #endregion
    }
}