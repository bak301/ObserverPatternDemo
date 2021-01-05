using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIManager : AbstractUIManager
{
    [SerializeField] private Button startGameButton;
     // Start is called before the first frame update
    private void Awake()
    {
        InitObservers();
    }

    protected override void RegisterListeners()
    {
        this.RegisterListener(GameEvent.OnStartGame, (param) =>
        {
            SceneManager.LoadSceneAsync("InGame");
            Debug.Log("Loading main game ...");
        });
    }

    protected override void AddListenersToComponent()
    {
        startGameButton.onClick.AddListener(() => startGameButton.FireEvent(GameEvent.OnStartGame));
    }

    //void OnDestroy()
    //{
    //    EventDispatcher.Instance.ClearListeners(); 
    //}

/* bug if enable OnDestroy() to clear Listeners: Some objects were not cleaned up when closing the scene.
(Did you spawn new GameObjects from OnDestroy?)
The following scene GameObjects were found:
Singleton - EventDispatcher*/

}
