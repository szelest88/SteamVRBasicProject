using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallWorld
{
    public class Bullet : MonoBehaviour
    {
        [Tooltip("Speed of bullet (Const)")]
        public float Speed;
        [Tooltip("Damage inflicted by bullet to target. (Const)")]
        public Damage Damage;

        [Tooltip("Target (Var ReadOnly)")]
        public Health Target;

        public void SeekTarget(Health target)
        {
            Target = target;

            // TODO: Seek target better
            var directionToTarget = (target.transform.position - transform.position).normalized;
            GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(directionToTarget);
            GetComponent<Rigidbody>().velocity = Speed * directionToTarget;
        }

        void Start()
        {

        }

        void OnCollisionEnter(Collision collision)
        {
            var targetHit = collision.gameObject.GetInstanceID() == Target.gameObject.GetInstanceID();
            if (targetHit)
            {
                Target.ApplyDamage(Damage);
                Destroy(gameObject);
            }
        }
    }
}