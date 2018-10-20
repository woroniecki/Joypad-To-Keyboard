using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineMaster : MonoBehaviour {

    static CoroutineMaster instance;
    static WaitForEndOfFrame endOfFrame;

    private void Awake()
    {
        instance = this;
        endOfFrame = new WaitForEndOfFrame();
    }

    public static void ExecuteAfterNFrames(int amount, DelegateMethod delegat)
    {
        instance.StartCoroutine(RunAfterNFrames(amount, delegat));
    }

    static IEnumerator RunAfterNFrames(int amount, DelegateMethod delegat)
    {
        for(int i = 0; i < amount; i++)
            yield return endOfFrame;
        delegat();
    }
}
