namespace StoreEngine
{
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    using System.Linq;



    public class SaveProducts: Saver.Saver
    {
        private Saver.ESaveData _saveType = Saver.ESaveData.Products;
        private Dictionary<string, ProductSaveData> _saveData;
        
        public void AddModifiedProduct(string productName, int amount, float outputPerTick, int outputMultiplier, float outputResidual, bool outputMultiplierRecalculation, int startOutputMultiplier, int targetOutputMultiplier, int ticksDone, int inputTicksTarget, bool withSave = true)
        {
            ProductSaveData inputData = new ProductSaveData(productName, amount, outputPerTick, outputMultiplier,
                outputResidual, outputMultiplierRecalculation, startOutputMultiplier, targetOutputMultiplier, ticksDone,
                inputTicksTarget);
            
            if (_saveData.ContainsKey(productName))
            {
                _saveData[productName] = inputData;
            }
            else
            {
                _saveData.Add(productName, inputData);
            }

            if (withSave)
            {
                SaveData();
            }
        }

        public void SaveData()
        {

            ProductsSaveData saveList = new ProductsSaveData();
            
            foreach (string key in _saveData.Keys)
            {
                saveList.productsSaveData.Add(_saveData[key]);
            }
            
            Save(_saveType, JsonUtility.ToJson(saveList));
        }

        private void LoadData()
        {
            string loadedData = Load(_saveType);
            if (loadedData != "No save!")
            {
                _saveData = new Dictionary<string, ProductSaveData>();
                
                ProductsSaveData data =
                    JsonUtility.FromJson<ProductsSaveData>(loadedData);

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

        public void ModifyProduct(Product product, int minTick)
        {
            if (_saveData.ContainsKey(product.productName))
            {
                ProductSaveData data = _saveData[product.productName];
                if (data.outputMultiplierRecalculation)
                {
                    product.LoadData(data.amount, data.outputPerTick,data.outputMultiplier, data.outputResidual, data.outputMultiplierRecalculation, data.startOutputMultiplier,data.targetOutputMultiplier,data.ticksDone, data.ticksTarget, minTick);
                }
                else
                {
                    product.LoadData(data.amount, data.outputPerTick,data.outputMultiplier, data.outputResidual, data.outputMultiplierRecalculation);
                }
                
                product.amount = _saveData[product.productName].amount;
                product.outputMultiplier = _saveData[product.productName].outputMultiplier;
                product.outputResidual = _saveData[product.productName].outputResidual;
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
        public float outputPerTick;
        public int outputMultiplier;
        public float outputResidual;
        
        public bool outputMultiplierRecalculation;
        public int startOutputMultiplier;
        public int targetOutputMultiplier;
        public int ticksDone;
        public int ticksTarget;

        public ProductSaveData(string inputName, int inputAmount, float inputOutputPerTick, int inputMultiplier, float inputResidual,bool inputOutputMultiplierRecalculation, int inputStartOutputMultiplier = 0, int inputTargetOutputMultiplier = 0, int inputTicksDone = 0, int inputTicksTarget = 0)
        {
            productName = inputName;
            amount = inputAmount;
            outputPerTick = inputOutputPerTick;
            outputMultiplier = inputMultiplier;
            outputResidual = inputResidual;
            outputMultiplierRecalculation = inputOutputMultiplierRecalculation;
            
            if (inputOutputMultiplierRecalculation)
            {
                startOutputMultiplier = inputStartOutputMultiplier;
                targetOutputMultiplier = inputTargetOutputMultiplier;
                ticksDone = inputTicksDone;
                ticksTarget = inputTicksTarget;
            }
        }
    }
}
