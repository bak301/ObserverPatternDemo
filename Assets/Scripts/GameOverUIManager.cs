using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIManager : AbstractUIManager
{
    [SerializeField] private Button next;
    [SerializeField] private Button mainMenu;
    // Start is called before the first frame update
    void Awake()
    {
        InitObservers();
    }

    protected override void RegisterListeners()
    {
        this.RegisterListener(GameEvent.OnNext, (param) =>
        {
            SceneManager.LoadSceneAsync("InGame");
            Debug.Log("Next stage ...");
        });

        this.RegisterListener(GameEvent.OnRestart, (param) =>
        {
            SceneManager.LoadSceneAsync("InGame");
            Debug.Log("Restart the game ...");
        });

        this.RegisterListener(GameEvent.OnIdleGame, (param) => 
        {
            SceneManager.LoadSceneAsync("MainMenu");
            Debug.Log("Idling in main menu ...");
        });
    }

    protected override void AddListenersToComponent()
    {
        next.onClick.AddListener(() => next.FireEvent(GameEvent.OnNext));
        mainMenu.onClick.AddListener(() => mainMenu.FireEvent(GameEvent.OnIdleGame));
    }

    //void OnDestroy()
    //{
    //    EventDispatcher.Instance.ClearListeners();
    //}
}
