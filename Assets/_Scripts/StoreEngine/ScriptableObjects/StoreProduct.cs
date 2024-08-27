using Sirenix.OdinInspector;

namespace StoreEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using StoreEngine;
    using UnityEngine;

    [CreateAssetMenu (menuName = "ScriptableObjects/Store Engine/Store Product")]
    public class StoreProduct : ScriptableObject
    {
        public Product product;

        // Data.
        public int amount;
        [MinValue(0f)]
        public float outputPerTick;
        [Range(0,2f)]
        public float outputMultiplier;
        [MinValue(0f)]
        public float space;

        // Data for calculation;
        [SerializeField] [HideInInspector]
        private float outputResidual;
    }
}
