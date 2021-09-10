using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

namespace N.Package.Input.Components
{
    /// <summary>
    /// Add one of these per player.
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    public abstract class NInputPlayer<TState, TActor> : MonoBehaviour where TActor : MonoBehaviour
    {
        public TState state;

        public TActor actor;

        public void Start()
        {
            var globalState = FindObjectOfType<NInputPlayers>();
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