using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestObserver : MonoBehaviour
{
    [SerializeField] private Dropdown events;
    [SerializeField] private Button trigger;
    // Start is called before the first frame update
    void Awake()
    {
        events.ClearOptions();
        events.AddOptions(new List<string>(Enum.GetNames(typeof(GameEvent))));
        trigger.onClick.AddListener(() => 
        {
            GameEvent ev = (GameEvent)events.value;
            this.FireEvent(ev);
        });
    }
}
