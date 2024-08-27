using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace StoreEngine
{
    [CreateAssetMenu (menuName = "ScriptableObjects/Store Engine/Product")]
    public class Product : ScriptableObject
    {
        // Product description.
        [HideLabel, PreviewField(60)]
        [HorizontalGroup("Split", 60)]
        public Texture icon;
        
        [VerticalGroup("Split/Right"), LabelWidth(120)] [DisableInPlayMode]
        public string productName;
        [VerticalGroup("Split/Right"), LabelWidth(120)] [DisableInPlayMode]
        public string description;
        [VerticalGroup("Split/Right"), LabelWidth(120)] [DisableInPlayMode]
        public ProductType productType;
        [VerticalGroup("Split/Right"), LabelWidth(120)] [Range(0,10)] [DisableInPlayMode]
        public int happiness;
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

