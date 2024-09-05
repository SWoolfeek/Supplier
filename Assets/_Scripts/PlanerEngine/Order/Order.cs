namespace PlanerEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using System;
    using UnityEngine;

    [Serializable]
    public class Order
    {
        public float totalSize;
        public List<Product> products;
        public bool packed;
        public int ticksToPrepare;
    }

    [Serializable]
    public class Product
    {
        public string productName;
        public int amount;
        public float size;
    }
}
