using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    List<IObserver> observers;


    public void Init()
    {
        observers = new List<IObserver>();
    }

    public void Subscribe(IObserver observer)
    {
        observers.Add(observer);
    }
    public void CancelSubscribe(IObserver observer)
    {
        observers.Remove(observer);
    }
    public void CallObserver()
    {
        foreach (IObserver observer in observers)
        {
            observer.UpdateInform();
        }
    }
    public abstract void UpdateInform();
}   
