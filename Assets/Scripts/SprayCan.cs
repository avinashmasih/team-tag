using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SprayCan : MonoBehaviour
{
    /// <summary>
    /// The current color of the spray can.
    /// </summary>
    [Tooltip("The current color being sprayed.")]
    public Color color;
    
    /// <summary>
    /// How fast the can drains.
    /// </summary>
    [Tooltip("How fast the can drains.")]
    public float SprayRate = 20;

    [Tooltip("How much the can refills with each call to Refill().")]
    public float RefillRate = 250;

    /// <summary>
    /// How much paint the can has available.
    /// </summary>
    [Tooltip("How much paint the can has available.")]
    public float MaxCapacity = 1000;

    [SerializeField]
    [HideInInspector]
    private float currentFill;
    /// <summary>
    /// How much paint is currently in the can.
    /// </summary>
    public float CurrentFill => currentFill;

    [SerializeField]
    [HideInInspector]
    private bool spraying = false;
    /// <summary>
    /// If the spray can is being fired.
    /// </summary>
    public bool Spraying { get => spraying; }

    private void Start()
    {
        currentFill = MaxCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Spray") && !Mathf.Approximately(0, currentFill))
        {
            spraying = true;
            currentFill -= SprayRate * Time.deltaTime;
            currentFill = Mathf.Max(0, currentFill);
            return;
        }
        else if (Spraying)
        {
            spraying = false;
        }

        if (!Mathf.Approximately(Input.GetAxis("Refill"), 0)) Refill();
    }

    public void ChangeColor(Color _color)
    {
        color = _color;
    }

    public void Refill()
    {
        currentFill = Mathf.Min(MaxCapacity, currentFill + RefillRate);
    }

    public void RefillMax()
    {
        currentFill = MaxCapacity;
    }
}
