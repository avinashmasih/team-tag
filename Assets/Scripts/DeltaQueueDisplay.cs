using UnityEngine;
using UnityEngine.UI;

public class DeltaQueueDisplay : MonoBehaviour
{
    public Text objectiveText;
    public string finishedText = "Done!";

    public Text countdown;
    public Image delayImage;

    public DeltaQueue queue;

    public void Start()
    {
        // Show the new items
        queue.OnClockAdvanced.AddListener(SetText);
        queue.OnClockCompleted.AddListener(SetTextFinished);
        
        // Timer
        queue.OnSecondElapsed += Tick;

        // Show image when things are changing
        queue.OnWaitTimeBegan += ShowImage;
        queue.OnWaitTimeEnded += HideImage;
    }

    private void ShowImage()
    {
        if (delayImage) delayImage.enabled = true;
    }

    private void HideImage()
    {
        if (delayImage) delayImage.enabled = false;
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
        SetText(finishedText);
    }
}
