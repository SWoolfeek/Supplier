namespace Tools
{
#if UNITY_EDITOR
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;


    
    public class SaveTools
    {
        private Saver.Saver _saver = new Saver.Saver();

        [MenuItem("Tools/Save Tools/Clear Saves/All Saves")]
        public void ClearAllSaves()
        {
            Debug.Log("This method is not ready!");
        }

        [MenuItem("Tools/Save Tools/Clear Saves/Products")]
        public void ClearProducts()
        {
            _saver.ClearExactSaveFile(Saver.ESaveData.Products);
        }
    }
#endif
}
