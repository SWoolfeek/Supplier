

namespace CoreSystems
{
    using System.Collections;
    using UnityEngine;

    public class TickManager : MonoBehaviour
    {
        [SerializeField] private CoreParameters parameters;

        private bool _ticking;
        private bool _pausedTick;
        private float _timeBetweenTick;
        private float _timeTicked;
        private float _startTime;
        
        private Coroutine _tickCoroutine;

        // Start is called before the first frame update
        void Start()
        {
            _timeBetweenTick = parameters.timeBetweenTicks;
        }

        public void StartTick()
        {
            _ticking = true;
            _tickCoroutine = StartCoroutine(TickCoroutine());
        }
        
        public void StopTick()
        {
            if (_tickCoroutine != null)
            {
                StopCoroutine(_tickCoroutine);
            }
        }
        
        private void UnPauseTick()
        {
            _pausedTick = false;
        }

        private void PauseTick()
        {
            _pausedTick = true;
        }

        private IEnumerator TickCoroutine()
        {
            while (_ticking)
            {
                while (_pausedTick)
                {
                    yield return null;
                }
                
                yield return new WaitForSeconds(_timeBetweenTick);
                
                GlobalEventManager.Tick();
            }
        }
    }
}
