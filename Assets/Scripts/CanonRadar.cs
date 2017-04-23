using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallWorld
{
    public class CanonRadar : MonoBehaviour
    {
        [Tooltip("Radar current target (Var ReadOnly)")]
        public Health TargetCurrent;

        void OnTriggerEnter(Collider collider)
        {
            if (TargetCurrent == null)
            {
                TargetCurrent = collider.gameObject.GetComponent<Health>();
            }
        }

        void OnTriggerExit(Collider collider)
        {
            if (TargetCurrent != null && TargetCurrent.gameObject.GetInstanceID() == collider.gameObject.GetInstanceID())
            {
                TargetCurrent = null;
            }
        }
    }
}