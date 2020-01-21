using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RelayTimer : MonoBehaviour
{
    [Tooltip("The number of seconds per relay.")]
    public float SecondsPerTag = 20.0f;

    [Tooltip("The number of rounds in the relay.")]
    public int RelayCount = 5;

    [Tooltip("How long the tag image should be on screen.")]
    public float tagDelay = 2.0f;

    [HideInInspector]
    public float timeRemaining;

    [Tooltip("The image to display when it's time to rotate")]
    public Image relayImage;

    public Text text;

    public class RoundFinishedEvent : UnityEvent {}
    [Tooltip("Fires when all relays are finished.")]
    public RoundFinishedEvent OnRoundFinished;

    public class RelayFinishedEvent : UnityEvent { }
    [Tooltip("Fires when each relay is finished.")]
    public RelayFinishedEvent OnRelayFinished;

    // Start is called before the first frame update
    void Start()
    {
        var timePerRelay = SecondsPerTag + tagDelay;
        timeRemaining = RelayCount * timePerRelay;
        InvokeRepeating("ShowTag", SecondsPerTag, timePerRelay);
        InvokeRepeating("Tick", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0.0f)
        {
            CancelInvoke();
            OnRoundFinished?.Invoke();
        }
    }

    void ShowTag()
    {
        OnRelayFinished?.Invoke();
        relayImage.enabled = true;
        Invoke("HideTag", tagDelay);
    }

    void HideTag()
    {
        relayImage.enabled = false;
    }

    void Tick()
    {
        text.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60}";
    }

}
