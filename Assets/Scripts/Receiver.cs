using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallWorld
{
    public class Receiver : MonoBehaviour
    {
        public uint PayLoadMax;
        public uint PayLoadCurrent;

        public uint PayLoadNeeded
        {
            get
            {
                return PayLoadMax - PayLoadCurrent;
            }
        }

        public void ReceivePayLoad(Transporter transporter, uint payLoad)
        {
            PayLoadCurrent = Math.Min(PayLoadCurrent + payLoad, PayLoadMax);
            Debug.LogFormat("Received payload. receiver: {0}, transporter: {1}, payload: {2}", name, transporter.name, payLoad);
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}