namespace StoreEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class StoreProduction : MonoBehaviour
    {
        private Product[] _products;
        private SaveProducts _saveProducts;

        public void Initialization(Product[] products, SaveProducts saveProducts)
        {
            _products = products;
            _saveProducts = saveProducts;
        }
        
        public void StartProduction()
        {
            //Debug.Log("Start production.");
            float time = Time.time;
            foreach (Product product in _products)
            {
                product.TickProduction();
                _saveProducts.AddModifiedProduct(product.productName,product.amount,product.outputMultiplier,product.outputResidual, false);
            }
            
            _saveProducts.SaveData();
            time = Time.time - time;
            Debug.Log("End production. " + time);
        }
    }
}
