using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveDisplay : MonoBehaviour
{
    public Text objectiveText;

    public string finishedText = "Done!";

    public DeltaQueue queue;

    public void Start()
    {
        queue.OnClockAdvanced.AddListener(SetText);
        queue.OnClockCompleted.AddListener(SetTextFinished);
    }

    public void SetText(string text)
    {
        objectiveText.text = text;
    }

    private void SetTextFinished()
    {
        objectiveText.text = finishedText;
    }
}
