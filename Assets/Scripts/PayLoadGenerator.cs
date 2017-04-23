using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SmallWorld
{
    public class PayLoadGenerator : MonoBehaviour
    {
        [Tooltip("Type of generated payload (Const)")]
        public PayLoadType PayLoadType;
        [Tooltip("Speed of payload generation (Const)")]
        public uint GeneratingSpeedInPayLoadPerSecond;
        [Tooltip("Receiver of payload (Const)")]
        public PayLoadStorage Receiver;
        [Tooltip("Indicates that Generator generated payload in previous frame")]
        public bool Working;

        void Awake()
        {
            Assert.IsTrue(GeneratingSpeedInPayLoadPerSecond > 0, "GeneratingSpeedInPayLoadPerSecond <= 0");
            Assert.IsTrue(PayLoadType == Receiver.PayLoadType, "this.PayLoadType != Receiver.PayLoadType");
        }

        void Start()
        {
            StartCoroutine(GenerateInternal());
        }

        IEnumerator GenerateInternal()
        {
            while (true)
            {
                Working = Generate();

                yield return new WaitForSeconds(1);
            }
        }

        protected virtual bool Generate()
        {
            bool working;
            if (Receiver.PayLoadNeeded > 0)
            {
                Receiver.ReceivePayLoad(this, GeneratingSpeedInPayLoadPerSecond);
                working = true;
            } else {
                working = false;
            }
            return working;
        }
    }

}