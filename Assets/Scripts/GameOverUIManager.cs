using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] private Button next;
    // Start is called before the first frame update
    void Awake()
    {
        next.onClick.AddListener(() => next.FireEvent(GameEvent.OnNext));

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
    }

    //void OnDestroy()
    //{
    //    EventDispatcher.Instance.ClearListeners();
    //}
}
