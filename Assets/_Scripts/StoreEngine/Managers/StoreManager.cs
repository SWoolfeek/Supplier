using System;
using CoreSystems;
using Sirenix.OdinInspector;

namespace StoreEngine
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    public class StoreManager : MonoBehaviour
    {
        [SerializeField] private AllProducts allProducts;
        
        private Dictionary<string, Product> productsDictionary = new Dictionary<string, Product>();
        private SaveProducts _saveProducts = new SaveProducts();

        private void Awake()
        {
            GlobalEventManager.onTick.AddListener(TickEnded);
        }

        public void StartStore()
        {
            foreach (Product product in allProducts.allProducts)
            {
                productsDictionary.Add(product.productName, product);
            }

            foreach (string product in _saveProducts.ModifiedDataLoad())
            {
                if (productsDictionary.ContainsKey(product))
                {
                    _saveProducts.ModifyProduct(productsDictionary[product]);
                }
            }
        }

        private void TickEnded()
        {
            
        }

        [Button]
        public void Save()
        {
            _saveProducts.SaveData();
        }
        
    }
}