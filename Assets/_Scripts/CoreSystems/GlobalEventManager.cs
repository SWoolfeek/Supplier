namespace CoreSystems
{
    using UnityEngine;
    using UnityEngine.Events;
    
    public class GlobalEventManager : MonoBehaviour
    {
        static public UnityEvent onTick = new UnityEvent();

        static public void Tick()
        {
            onTick.Invoke();
        }

    }
}

