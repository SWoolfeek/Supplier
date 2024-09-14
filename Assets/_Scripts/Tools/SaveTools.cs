namespace Tools
{
    
#if UNITY_EDITOR
    using UnityEngine;
    using UnityEditor;


    public class SaveTools
    {
        private static Saver.Saver _saver = new Saver.Saver();

        [MenuItem("Tools/Save Tools/Clear Saves/All Saves")]
        public static void ClearAllSaves()
        {
            Debug.Log("This method is not ready!");
        }

        [MenuItem("Tools/Save Tools/Clear Saves/Products")]
        public static void ClearProducts()
        {
            _saver.ClearExactSaveFile(Saver.ESaveData.Products);
        }
    }
#endif
}
