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

    public enum Necessity
    {
        Luxury = 0,
        Normal = 1,
        Necessary = 2
    }

    public enum ProductType
    {
        Food = 0,
        Material = 1
    }
}

