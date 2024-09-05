using System;


namespace StoreEngine
{
#if UNITY_EDITOR
    using UnityEditor;
#endif
    
    using UnityEngine.Events;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Sirenix.OdinInspector;

    public class StoreProduction : MonoBehaviour
    {
        [SerializeField] private AllProducts allProducts;
        [SerializeField] private List<ProductUi> allUiProducts;
        
#if UNITY_EDITOR
        [SerializeField] private GameObject prefabUiProducts;
        [SerializeField] private Transform parentForUiProducts;
        [SerializeField] private PlanerEngine.NewOrderManager newOrderManager;
#endif
        [ReadOnly]
        [SerializeField] private int freeProduction;
        [HideInInspector]
        public int tempFreeProduction;
        private int maxProduction;
        
        // Managers.
        private StoreManager _storeManager;
        private StoreUiManager _storeUi;
        private StoreEventManager _storeEvents;

        // Events.
        [HideInInspector]
        public UnityEvent OnChangesAccepted = new UnityEvent();

        private bool _changes;
        private Product[] _products;
        private SaveProducts _saveProducts;

        public void Initialization(StoreManager storeManager, StoreUiManager storeUi, StoreEventManager storeEvents, Product[] products, SaveProducts saveProducts)
        {
            _storeManager = storeManager;
            _storeUi = storeUi;
            _storeEvents = storeEvents;
            
            _products = products;
            _saveProducts = saveProducts;

            maxProduction = allUiProducts.Count * 100;
            freeProduction = maxProduction;

            foreach (Product product in allProducts.allProducts)
            {
                freeProduction -= product.outputMultiplier;
            }

            tempFreeProduction = freeProduction;

            foreach (ProductUi productUi in allUiProducts)
            {
                productUi.Initialize();
            }
        }
        
        public void StartProduction()
        {
            foreach (Product product in _products)
            {
                product.TickProduction();
                _saveProducts.AddModifiedProduct(product.productName,product.amount,product.outputPerTick,product.outputMultiplier,product.outputResidual, product.outputMultiplierRecalculation, product.startOutputMultiplier, product.targetOutputMultiplier, product.ticksDone, product.ticksTarget, false);
            }
            
            _saveProducts.SaveData();
            _storeEvents.UpdateUiValues();
        }

        public void ChangeProduction(int inputChange)
        {
            tempFreeProduction += inputChange;
            _storeUi.UnsavedChanges(true);
        }

        public void AcceptChanges(bool state)
        {
            _storeUi.UnsavedChanges(false);
            _storeEvents.AcceptChanges(state);
            
            if (state)
            {
                freeProduction = tempFreeProduction;
            }
            else
            {
                tempFreeProduction = freeProduction;
            }
            
        }

        public void ChangedOutputMultiplier(Product product)
        {
            _saveProducts.AddModifiedProduct(product.productName,product.amount, product.outputPerTick,product.outputMultiplier,product.outputResidual, product.outputMultiplierRecalculation, product.startOutputMultiplier, product.targetOutputMultiplier, product.ticksDone, product.ticksTarget);
        }
        
        
        
#if UNITY_EDITOR
        [Button]
        public void AddUiProducts()
        {
            allProducts.UpdateList();
            StoreEventManager storeEvent = GetComponent<StoreEventManager>();
            
            foreach (Product product in allProducts.allProducts)
            {
                //GameObject newProductUi = Instantiate(prefabUiProducts, parentForUiProducts);
                GameObject newProductUi = PrefabUtility.InstantiatePrefab(prefabUiProducts) as GameObject;
                newProductUi.transform.parent = parentForUiProducts;
                newProductUi.transform.localScale = new Vector3(1,1,1);
                newProductUi.GetComponent<ProductUi>().EditorInstantiate(product, storeEvent,this);
            }

            allUiProducts = new List<ProductUi>();
            
            foreach (Transform child in parentForUiProducts)
            {
                allUiProducts.Add(child.GetComponent<ProductUi>());
            }
            
            newOrderManager.AddProductsInOrderCatalogue();
        }
#endif

        private void OnDisable()
        {
            
        }
    }
}
