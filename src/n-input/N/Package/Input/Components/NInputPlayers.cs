using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace N.Package.Input.Components
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class NInputPlayers : MonoBehaviour
    {
        public List<Player> players;

        public Action OnPlayersChanged { get; set; }
        
        [System.Serializable]
        public struct Player
        {
            public int playerId;
            public GameObject player;
            public GameObject actor;
            public GameObject template;
        }

        public void Register<TState, TActor>(NInputPlayer<TState, TActor> inputHandler, PlayerInput inputSource) where TActor : MonoBehaviour
        {
            var player = new Player();
            try
            {
                inputHandler.transform.name = $"{nameof(NInputPlayers)}.Player.{inputSource.playerIndex}";
                player.playerId = inputSource.playerIndex;
                player.player = inputHandler.gameObject;
                var template = inputHandler.OnSelectActor(this, inputSource.playerIndex);
                player.template = template.gameObject;
                if (player.template == null)
                {
                    Debug.LogWarning($"Player {inputSource.playerIndex} failed to provide a valid actor prefab. Not spawned.");
                    return;
                }

                var actor = Instantiate(template);
                actor.transform.name = $"{nameof(NInputPlayers)}.Actor.{inputSource.playerIndex}";
                player.actor = actor.gameObject;
                inputHandler.actor = actor;
                inputHandler.OnSpawned(inputSource.playerIndex, actor);
            }
            catch (Exception error)
            {
                Debug.LogError($"Player {inputSource.playerIndex}: error spawning player");
                Debug.LogException(error);
                return;
            }

            players.Add(player);
            OnPlayersChanged?.Invoke();
        }
    }
}