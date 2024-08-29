using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace StoreEngine
{
    public class Product : ScriptableObject
    {
        // Product description.
        [BoxGroup("Properties")]
        [HideLabel, PreviewField(60)]
        public Texture icon;
        
        [BoxGroup("Properties")] [DisableInPlayMode]
        public string productName;
        [BoxGroup("Properties")] [TextArea(4, 14)]
        public string Description;
        [BoxGroup("Properties")] [DisableInPlayMode]
        public string description;
        [BoxGroup("Properties")] [ReadOnly]
        public ProductType productType;
        [BoxGroup("Properties")] [Range(0,10)] [DisableInPlayMode]
        public int happiness;
        
        // Store data.
        [BoxGroup("Store data")]
        [MinValue(0)]
        public int amount;
        [BoxGroup("Store data")]
        [MinValue(0f)]
        public float outputPerTick;
        [BoxGroup("Store data")]
        [Range(0,2f)]
        public float outputMultiplier;
        [BoxGroup("Store data")]
        [MinValue(0f)]
        public float space;

        // Data for calculation;
        [SerializeField] [HideInInspector]
        private float outputResidual;
    }

    public enum ProductType
    {
        Food = 0,
        Weapon = 1,
        Сlothes = 2,
        Transport = 3,
        Materials = 4,
        Others = 10
    }
}

