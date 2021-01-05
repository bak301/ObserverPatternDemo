using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;

public class InGameUIManager : AbstractUIManager
{
    [SerializeField] private Button pause;
    [SerializeField] private Button gameOver;
    [SerializeField] private Text countdown;
    [SerializeField] private Slider loading;
    [SerializeField] private Canvas main;
    [SerializeField] private Canvas secondary;
    [SerializeField] private Canvas tertiary;

    // Start is called before the first frame update
    void Awake()
    {
        InitObservers();
    }

    protected override void RegisterListeners()
    {
        this.RegisterListener(GameEvent.OnStartGame, (param) =>
        {
            this.FireEvent(GameEvent.OnInitGame);
            Debug.Log("Loading main game ...");
        });

        this.RegisterListener(GameEvent.OnInitGame, (param) =>
        {
            main.enabled = false;
            secondary.enabled = false;
            tertiary.enabled = true;

            StartCoroutine(InitDelay());
            Debug.Log("Init player and enemy ...");
        });

        this.RegisterListener(GameEvent.OnReadyGame, (param) =>
        {
            main.enabled = false;
            secondary.enabled = true;
            tertiary.enabled = false;
            StartCoroutine(ReadyCountdown());
            Debug.Log("Chilling and relax for a few seconds please ...");
        });

        this.RegisterListener(GameEvent.OnPlay, (param) =>
        {
            gameOver.interactable = true;
            Time.timeScale = 1;
            Debug.Log("Playing game ...");
        });

        this.RegisterListener(GameEvent.OnPause, (param) =>
        {
            gameOver.interactable = false;
            Time.timeScale = 0;
            Debug.Log("Game paused. Take a cup of coffee");
        });

        this.RegisterListener(GameEvent.OnComplete, (param) =>
        {
            SceneManager.LoadSceneAsync("GameOver");
            Debug.Log("Game completed. Congratulation.");
        });
    }

    protected override void AddListenersToComponent()
    {
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name == "InGame")
                this.FireEvent(GameEvent.OnInitGame);
        };

        pause.onClick.AddListener(() =>
        {
            bool isGameNotPaused = gameOver.IsInteractable();
            GameEvent @event = isGameNotPaused ? GameEvent.OnPause : GameEvent.OnPlay;
            pause.FireEvent(@event);
        });

        gameOver.onClick.AddListener(() => gameOver.FireEvent(GameEvent.OnComplete));

    }

    private IEnumerator InitDelay()
    {
        float timer = 3;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            loading.value = (3 - timer) / 3;
            yield return null;
        }

        tertiary.enabled = false;
        
        this.FireEvent(GameEvent.OnReadyGame);
    }

    private IEnumerator ReadyCountdown()
    {
        float timer = 3;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            countdown.text = Mathf.Ceil(timer).ToString();
            yield return null;
        }

        main.enabled = true;
        secondary.enabled = false;
        tertiary.enabled = false;
        this.FireEvent(GameEvent.OnPlay);
    }

    //private void OnDestroy()
    //{
    //    EventDispatcher.Instance.ClearListeners();
    //}
}
