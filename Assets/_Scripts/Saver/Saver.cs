namespace Saver
{
    using UnityEngine;
    using System;
    using System.IO;
    using StoreEngine;


    public class Saver
    {
        private SaveSettings _saveSettings = new SaveSettings();
        private string _directory = "Saves";
        
        public void SaveData(ESaveData saveDataType, string jsonData)
        {
            string savePath = Path.Combine(_directory,_saveSettings.pathToSave[saveDataType]);
            
            if (File.Exists(savePath))
            {
                File.WriteAllText(savePath, String.Empty);
            }

            ProductsSaveData saveList = new ProductsSaveData();
            
            var file = File.CreateText(savePath);
            file.WriteLine(jsonData);
            
            file.Close();
        }
        
        private string LoadData(ESaveData saveDataType)
        {
            string savePath = Path.Combine(_directory,_saveSettings.pathToSave[saveDataType]);
            
            if(File.Exists(savePath))
            {
                return File.ReadAllText(savePath);
            }
            else
            {
                Debug.LogError("Saved products do not exist!");
                return "NoSave!";
            }
        }

        
    }

    public enum ESaveData
    {
        Products = 1
    }
}


