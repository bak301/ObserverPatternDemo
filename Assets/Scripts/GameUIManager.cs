using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{

    private void Awake()
    {
        RegisterListeners();
    }

    private void RegisterListeners()
    {
        this.RegisterListener(GameEvent.OnLoadData, (param) => Debug.Log("Loading pre game data ..."));
        this.RegisterListener(GameEvent.OnIdleGame, (param) => Debug.Log("Idle in main menu ..."));
        this.RegisterListener(GameEvent.OnLoadGame, (param) => Debug.Log("Loading game stage ..."));
        this.RegisterListener(GameEvent.OnInitGame, (param) => Debug.Log("Init characters and enemies ..."));
        this.RegisterListener(GameEvent.OnReadyGame, (param) => Debug.Log("Relax for 3 seconds ..."));
        this.RegisterListener(GameEvent.OnPlay, (param) => Debug.Log("Play the game ..."));
        this.RegisterListener(GameEvent.OnPause, (param) => Debug.Log("Game paused ..."));
        this.RegisterListener(GameEvent.OnComplete, (param) => Debug.Log("Game finished ..."));
        this.RegisterListener(GameEvent.OnNext, (param) => Debug.Log("Next stage ..."));
        this.RegisterListener(GameEvent.OnRestart, (param) => Debug.Log("Restart stage ..."));
    }
}
