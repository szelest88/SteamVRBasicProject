using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallWorld
{
    public class Transporter : MonoBehaviour
    {
        [Tooltip("PayLoad type carried by this Transporter (Const)")]
        public PayLoadType PayLoadType;
        [Tooltip("Amount of PayLoad transporter will carry (Const)")]
        public uint PayLoadCapacity;
        [Tooltip("Speed of transporter (Const)")]
        public float Speed;
        [Tooltip("Transporter destination (Var Readonly)")]
        public PayLoadStorage Receiver;

        public void SendToReceiverWithPayLoad(PayLoadStorage receiver)
        {
            Receiver = receiver;

            var directionToTarget = (receiver.transform.position - this.transform.position).normalized;
            Debug.LogFormat("Receiver pos: {0}, transporter pos: {1}", receiver.transform.position, this.transform.position);

            GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(directionToTarget);
            GetComponent<Rigidbody>().velocity = Speed * directionToTarget;
        }

        void OnCollisionEnter(Collision collision)
        {
            bool collidedWithReceiver = (collision.gameObject.GetInstanceID() == Receiver.gameObject.GetInstanceID());
            if (collidedWithReceiver)
            {
                Receiver.ReceivePayLoad(this, PayLoadCapacity);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarningFormat("Transporter encountered unexpected collision. transporter: {0}, other: {1}", name, collision.gameObject.name);
            }
        }
    }
}
