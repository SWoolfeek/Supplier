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
        
        protected void Save(ESaveData saveDataType, string jsonData)
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
        
        protected string Load(ESaveData saveDataType)
        {
            string savePath = Path.Combine(_directory,_saveSettings.pathToSave[saveDataType]);
            
            if(File.Exists(savePath))
            {
                string loadedData = File.ReadAllText(savePath);
                
                if (loadedData.Length > 2)
                {
                    return loadedData;
                }
                else
                {
                    return "No save!";
                }
                
            }
            else
            {
                Debug.LogError("Saved file do not exist!");
                return "No save!";
            }
        }

        public void ClearExactSaveFile(ESaveData saveDataType)
        {
            string savePath = Path.Combine(_directory,_saveSettings.pathToSave[saveDataType]);
            
            if (File.Exists(savePath))
            {
                File.WriteAllText(savePath, String.Empty);
            }
        }

        public void ClearAllSaves()
        {
            
        }

        
    }

    public enum ESaveData
    {
        Products = 1
    }
}


