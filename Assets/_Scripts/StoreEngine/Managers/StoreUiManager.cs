namespace StoreEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class StoreUiManager : MonoBehaviour
    {
        [Header("Ui Elements")]
        [SerializeField] private GameObject storeChangesButtons;
        
        // Mangers.
        private StoreManager _storeManager;
        private StoreEventManager _storeEvents;
        
        
        public void Initialize(StoreManager storeManager, StoreEventManager storeEvents)
        {
            _storeManager = storeManager;
            _storeEvents = storeEvents;
        }

        public void UnsavedChanges(bool inputState)
        {
            storeChangesButtons.SetActive(inputState);
        }
        
    }
}
