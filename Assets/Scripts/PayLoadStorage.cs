using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallWorld
{
    public class PayLoadStorage : MonoBehaviour
    {
        [Tooltip("Type of payload (Const)")]
        public PayLoadType PayLoadType;
        [Tooltip("Max amount of payload (Const)")]
        public uint PayLoadMax;
        [Tooltip("Current amount of payload (Var)")]
        public uint PayLoadCurrent;

        public uint PayLoadNeeded
        {
            get
            {
                return PayLoadMax - PayLoadCurrent;
            }
        }

        public void ReceivePayLoad(UnityEngine.Object sender, uint payLoad)
        {
            PayLoadCurrent = Math.Min(PayLoadCurrent + payLoad, PayLoadMax);
            Debug.LogFormat("Received payload. receiver: {0}, sender: {1}, payload: {2}", name, sender.name, payLoad);
        }

    }
}