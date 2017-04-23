using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallWorld
{
    public class CanonRadar : MonoBehaviour
    {
        [Tooltip("Radar current target (Var ReadOnly)")]
        public Health TargetCurrent;

        void OnCollisionEnter(Collision collision)
        {
            if (TargetCurrent == null)
            {
                TargetCurrent = collision.gameObject.GetComponent<Health>();
            }
        }

        void OnCollisionExit(Collision collision)
        {
            if (TargetCurrent != null && TargetCurrent.gameObject.GetInstanceID() == collision.gameObject.GetInstanceID())
            {
                TargetCurrent = null;
            }
        }
    }
}