namespace CoreSystems
{
    using UnityEngine;

    public class TickManager : MonoBehaviour
    {
        [SerializeField] private CoreParameters parameters;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void ExecuteTick()
        {
            GlobalEventManager.Tick();
        }
    }
}
