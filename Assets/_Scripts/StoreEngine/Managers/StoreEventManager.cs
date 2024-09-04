namespace StoreEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class StoreEventManager : MonoBehaviour
    {
        // Events.
        [HideInInspector] public ChangesAcceptEvent OnChangesAccepted = new ChangesAcceptEvent();
        [HideInInspector] public UnityEvent OnUpdateUiValues = new UnityEvent();

        public void AcceptChanges(bool state)
        {
            OnChangesAccepted.Invoke(state);
        }

        public void UpdateUiValues()
        {
            OnUpdateUiValues.Invoke();
        }
    }
    
    public class ChangesAcceptEvent: UnityEvent<bool>{}
}
