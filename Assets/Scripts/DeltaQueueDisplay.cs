using UnityEngine;
using UnityEngine.UI;

public class DeltaQueueDisplay : MonoBehaviour
{
    public Text objectiveText;
    public string finishedText = "Done!";

    public DeltaQueue queue;

    public void Awake()
    {
        // Show the new items
        queue.OnClockAdvanced.AddListener(SetText);
        queue.OnClockCompleted.AddListener(SetTextFinished);
    }

    public void SetText(string text)
    {
        objectiveText.text = text;
    }

    private void SetTextFinished()
    {
        SetText(finishedText);
    }
}
