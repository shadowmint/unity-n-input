using UnityEngine;
using UnityEngine.InputSystem;

namespace N.Package.Input.Components
{
    /// <summary>
    ///     Add one of these per player.
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    public abstract class NInputPlayer<TState, TActor> : MonoBehaviour where TActor : MonoBehaviour
    {
        public TState state;

        public TActor actor;

        private bool _initialized;

        public void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

            var globalState = FindFirstObjectByType<NInputPlayers>();
            if (globalState == null)
            {
                Debug.LogWarning("No NInputPlayers on scene; NInputPlayer is disabled");
                return;
            }

            var inputs = GetComponent<PlayerInput>();
            globalState.Register(this, inputs);
        }

        public abstract TActor OnSelectActor(NInputPlayers playerList, int playerIndex);

        public abstract void OnSpawned(int playerIndex, TActor actor);
    }
}