using UnityEngine;
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

    // Start is called before the first frame update
    void Start()
    {
        var timePerRelay = SecondsPerTag + tagDelay;
        timeRemaining = RelayCount * timePerRelay;
        InvokeRepeating("ShowTag", SecondsPerTag, timePerRelay);
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0.0f)
            CancelInvoke("ShowTag");
    }

    void ShowTag()
    {
        relayImage.enabled = true;
        Invoke("HideTag", tagDelay);
    }

    void HideTag()
    {
        relayImage.enabled = false;
    }

}
