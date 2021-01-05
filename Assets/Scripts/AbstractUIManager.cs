using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUIManager : MonoBehaviour
{
    protected abstract void AddListenersToComponent();
    protected abstract void RegisterListeners();

    protected void InitObservers()
    {
        AddListenersToComponent();
        RegisterListeners();
    }
}
