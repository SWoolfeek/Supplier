namespace StoreEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class StoreParameters : ScriptableObject
    {
        public int productionRecoveryTime = 10; // time in ticks.
        public int minProductionRecoveryTime = 3;
    }
}
