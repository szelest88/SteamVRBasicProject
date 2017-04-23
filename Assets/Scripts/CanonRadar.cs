using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallWorld
{
    public class CanonRadar : MonoBehaviour
    {
        [Tooltip("Radar current target (Var ReadOnly)")]
        public Health TargetCurrent;

		private bool trackingTarget;

        void Update()
        {
			if (trackingTarget && TargetCurrent == null) {
				TargetCurrent = null;
				trackingTarget = false;
			}
        }

        void OnTriggerEnter(Collider collider)
        {
            if (!trackingTarget)
            {
                TargetCurrent = collider.gameObject.GetComponent<Health>();
				trackingTarget = true;
            }
        }

        void OnTriggerExit(Collider collider)
        {
            if (trackingTarget && TargetCurrent.gameObject.GetInstanceID() == collider.gameObject.GetInstanceID())
            {
                TargetCurrent = null;
				trackingTarget = false;
            }
        }
    }
}