namespace StoreEngine
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using CoreSystems;
    using Sirenix.OdinInspector;


    public class StoreManager : MonoBehaviour
    {
        [SerializeField] private AllProducts allProducts;
        [SerializeField] private StoreParameters storeParameters;
        
        
        [Header("Managers")]
        [SerializeField] private StoreProduction storeProduction;
        [SerializeField] private StoreUiManager storeUi;
        [SerializeField] private StoreEventManager storeEvents;
        
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
                    _saveProducts.ModifyProduct(productsDictionary[product], storeParameters.minProductionRecoveryTime);
                }
            }
            
            storeUi.Initialize(this, storeEvents);
            storeProduction.Initialization(this, storeUi,storeEvents,productsDictionary.Values.ToArray(), _saveProducts);
        }

        private void TickEnded()
        {
            storeProduction.StartProduction();
        }

        [Button]
        public void Save()
        {
            _saveProducts.SaveData();
        }

        private void OnDisable()
        {
            GlobalEventManager.onTick.RemoveListener(TickEnded);
        }
    }
}