using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using N.Package.Input.Components;
using N.Package.Input.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExamplePlayer : NInputPlayer<ExamplePlayer.State, ExampleActor>
{
    public ExampleActor[] availableActors;

    public override void OnSpawned(int playerIndex, ExampleActor actor)
    {
        actor.state = state;
    }

    public override ExampleActor OnSelectActor(NInputPlayers playerList, int playerIndex)
    {
        var usedActors = playerList.players.Select(i => i.template.gameObject);
        var nextTemplate = availableActors.FirstOrDefault(i => !usedActors.Contains(i.gameObject));
        return nextTemplate;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        state.movement = value;
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            state.jump = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            state.jump = false;
        }
    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        state.look = value;
    }
    
    [System.Serializable]
    public class State
    {
        public bool jump;
        public Vector2 movement;
        public Vector2 look;
    }
}