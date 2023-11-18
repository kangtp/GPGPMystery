using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue info;

    private void Start() {
        Trigger();
    }

    public void Trigger()
    {
        var System = FindObjectOfType<DialogueSystem>();
        System.Begin(info);
    }
}
