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

            var directionToTarget = (target.transform.position - transform.position).normalized;
            GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(directionToTarget);
            GetComponent<Rigidbody>().velocity = Speed * directionToTarget;
        }

        void Start()
        {

        }

        void Update()
        {
            if (Target)
            {
                var directionToTarget = (Target.transform.position - transform.position).normalized;
                GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(directionToTarget);

                GetComponent<Rigidbody>().AddForce(Speed * directionToTarget, ForceMode.Impulse);
                // GetComponent<Rigidbody>().velocity = Speed * directionToTarget;
            } else {
				Destroy(gameObject);
			}
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