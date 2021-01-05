using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Canvas main;
    [SerializeField] private Canvas tertiary;
    [SerializeField] private Slider loading;
     // Start is called before the first frame update
    private void Awake()
    {
        startGameButton.onClick.AddListener(() => startGameButton.FireEvent(GameEvent.OnStartGame));

        this.RegisterListener(GameEvent.OnLoadData, (param) =>
        {
            main.enabled = false;
            tertiary.enabled = true;
            StartCoroutine(LoadData());
            Debug.Log("Loading game data at first ...");
        });

        this.RegisterListener(GameEvent.OnIdleGame, (param) => Debug.Log("Main menu idling ..."));
        this.RegisterListener(GameEvent.OnStartGame, (param) =>
        {
            SceneManager.LoadSceneAsync("InGame");
            Debug.Log("Loading main game ...");
        });
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

        main.enabled = true;
        tertiary.enabled = false;
        this.FireEvent(GameEvent.OnIdleGame);
    }

    public void Start()
    {
        main.enabled = false;
        tertiary.enabled = false;
        this.FireEvent(GameEvent.OnLoadData);
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
