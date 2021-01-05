using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreGameUIManager : AbstractUIManager
{
    [SerializeField] private Slider loading;
    private void Awake()
    {
        InitObservers();
    }

    private void Start()
    {
        this.FireEvent(GameEvent.OnLoadData);
    }

    private IEnumerator LoadData()
    {
        float timer = 3;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            loading.value = (3 - timer) / 3;
            yield return null;
        }

        this.FireEvent(GameEvent.OnIdleGame);
    }

    protected override void AddListenersToComponent()
    {
        
    }

    protected override void RegisterListeners()
    {
        this.RegisterListener(GameEvent.OnIdleGame, (param) =>
        {
            SceneManager.LoadSceneAsync("MainMenu");
            Debug.Log("Idling in main menu ...");
        });

        this.RegisterListener(GameEvent.OnLoadData, (param) =>
        {
            StartCoroutine(LoadData());
            Debug.Log("Loading common game data when game opened ...");
        });
    }
}
