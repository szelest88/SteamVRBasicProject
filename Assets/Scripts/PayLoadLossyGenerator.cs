using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SmallWorld
{
    public class PayLoadLossyGenerator : PayLoadGenerator
    {
        public uint GeneratingCostInDependentPayLoad;
        public PayLoadStorage DependentPayLoadStorage;

        protected override bool Generate()
        {
            var enoughtAmountOfDependantPayLoad = DependentPayLoadStorage.PayLoadCurrent >= GeneratingCostInDependentPayLoad;
            if (enoughtAmountOfDependantPayLoad)
            {
                var generated = base.Generate();
                if (generated)
                {
                    DependentPayLoadStorage.PayLoadCurrent -= GeneratingCostInDependentPayLoad;
                }
                return generated;
            }
            else
            {
                return false;
            }
        }
    }

}