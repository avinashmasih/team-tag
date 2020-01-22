using UnityEngine;
using UnityEngine.UI;

public class DeltaQueueDisplay : MonoBehaviour
{
    public Text objectiveText;

    public string finishedText = "Done!";

    public Text countdown;

    public DeltaQueue queue;

    public void Start()
    {
        queue.OnClockAdvanced.AddListener(SetText);
        queue.OnClockCompleted.AddListener(SetTextFinished);
        queue.OnSecondElapsed += Tick;
    }

    private void Tick(float timeRemaining)
    {
        if (!countdown) return;
        int minutesLeft = (int)timeRemaining / 60;
        int secondsLeft = (int)timeRemaining % 60;
        countdown.text = minutesLeft <= 0 ?
            secondsLeft.ToString() :
            $"{minutesLeft}:{secondsLeft}";
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
