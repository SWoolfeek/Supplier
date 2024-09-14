namespace CoreSystems
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameUiManager : MonoBehaviour
    {

        [Header("Tabs")] 
        [SerializeField] private GameObject storeTab;
        [SerializeField] private GameObject planerTab;

        private Dictionary<Tabs, GameObject> tabs;
        private Tabs _openTab;
        
        // Start is called before the first frame update
        public void Initialization()
        {
            tabs = new Dictionary<Tabs, GameObject>()
            {
                { Tabs.Store, storeTab },
                { Tabs.Planer, planerTab }
            };
            
            _openTab = Tabs.Store;
            tabs[_openTab].SetActive(true);
        }

        public void OpenTab(int tabNumb)
        {
            Tabs tab = (Tabs)tabNumb;
            
            if (tab != _openTab)
            {
                tabs[_openTab].SetActive(false);
                _openTab = tab;
                tabs[tab].SetActive(true);
            }
        }
        
        public enum Tabs
        {
            Store = 0,
            Planer = 1
        }
    }
}
