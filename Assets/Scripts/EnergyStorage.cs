using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallWorld {
    public class EnergyStorage : PayLoadStorage {
        public EnergyStorage() {
            PayLoadType = PayLoadType.Energy;
        }
    }
}