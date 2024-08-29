

using System.Linq;

namespace StoreEngine
{
    using System.Collections.Generic;
    using UnityEngine;
    using System.IO;
    using System;


    public class SaveProducts
    {
        private string _directory = "Saves";
        private Dictionary<string, ProductSaveData> _saveData;
        
        public void AddModifiedProduct(string productName, int amount, float outputMultiplier)
        {
            /*ProductSaveData sd = new ProductSaveData();
            sd.productName = productName;
            sd.amount = amount;
            sd.outputMultiplier = outputMultiplier;*/
            if (_saveData.ContainsKey(productName))
            {
                _saveData[productName] = new ProductSaveData(productName, amount, outputMultiplier);
                /*_saveData[productName] = sd;*/
            }
            else
            {
                _saveData.Add(productName, new ProductSaveData(productName,amount,outputMultiplier));
                /*_saveData.Add(productName, sd);*/
            }
            
            SaveData();
        }

        public void SaveData()
        {
            string savePath = Path.Combine(_directory,"Products.json");
            
            if (File.Exists(savePath))
            {
                File.WriteAllText(savePath, String.Empty);
            }

            ProductsSaveData saveList = new ProductsSaveData();
            
            foreach (string key in _saveData.Keys)
            {
                Debug.Log(key);
                saveList.productsSaveData.Add(_saveData[key]);
                Debug.Log(JsonUtility.ToJson(_saveData[key]));
            }
            
            var file = File.CreateText(savePath);
            Debug.Log(JsonUtility.ToJson(saveList));
            file.WriteLine(JsonUtility.ToJson(saveList));
            
            file.Close();
        }

        private void LoadData()
        {
            string savePath = Path.Combine(_directory,"Products.json");
            _saveData = new Dictionary<string, ProductSaveData>();
            
            if(File.Exists(savePath))
            {
                ProductsSaveData data =
                    JsonUtility.FromJson<ProductsSaveData>(File.ReadAllText(savePath));

                foreach (ProductSaveData saveData in data.productsSaveData)
                {
                    _saveData.Add(saveData.productName, saveData);
                }
            }
            else
            {
                Debug.LogError("Saved products do not exist!");
            }
        }

        public string[] ModifiedDataLoad()
        {
            LoadData();
            return _saveData.Keys.ToArray();
        }

        public void ModifyProduct(Product product)
        {
            if (_saveData.ContainsKey(product.productName))
            {
                product.amount = _saveData[product.productName].amount;
                product.outputMultiplier = _saveData[product.productName].outputMultiplier;
            }
            
        }
    }
    
    
    public class ProductsSaveData
    {
        public List<ProductSaveData> productsSaveData = new List<ProductSaveData>();
    }
    
    [Serializable]
    public class ProductSaveData
    {
        public string productName;
        public int amount;
        public float outputMultiplier;

        public ProductSaveData(string inputName, int inputAmount, float inputMultiplier)
        {
            productName = inputName;
            amount = inputAmount;
            outputMultiplier = inputMultiplier;
        }
    }
}
