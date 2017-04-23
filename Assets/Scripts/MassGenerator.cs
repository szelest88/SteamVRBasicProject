using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallWorld {
    public class MassGenerator : PayLoadLossyGenerator {
        public MassGenerator() {
			PayLoadType = PayLoadType.Mass;
        }
    }
}