using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallWorld
{
    public class Enemy : MonoBehaviour
    {
        [Tooltip("Damage inflicted to target on collision (Const)")]
		public Damage Damage;
        [Tooltip("Speed of enemy (Const)")]
        public float Speed;

        [Tooltip("Target (Var Readonly)")]
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

        void OnTriggerEnter(Collider collider)
        {
            // var targetHit = collider.gameObject.GetInstanceID() == Target.gameObject.GetInstanceID();
            // if (targetHit)
            // {
            //     Target.ApplyDamage(Damage);
            //     Destroy(gameObject);
            // }

			Health health = collider.gameObject.GetComponent<Health>();
			if (health) {
				health.ApplyDamage(Damage);
				Destroy(gameObject);
			}
        }
    }
}