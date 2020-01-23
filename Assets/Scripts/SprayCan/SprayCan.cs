using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public float CurrentFill {
        get => currentFill;
        private set {
            currentFill = value;
            currentFill = Mathf.Clamp(currentFill, 0, MaxCapacity);
            OnFillChanged?.Invoke(currentFill);
        }
    }

    [SerializeField]
    [HideInInspector]
    private bool spraying = false;
    /// <summary>
    /// If the spray can is being fired.
    /// </summary>
    public bool Spraying { get => spraying; }

    public delegate void ColorChanged(Color oldColor, Color newColor);
    public event ColorChanged OnColorChanged;

    public delegate void CurrentFillChanged(float newFill);
    public event CurrentFillChanged OnFillChanged;

    private void Start()
    {
        currentFill = MaxCapacity;
        ChangeColor(color);
        //WiimoteInput.accelerometerIntensityMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (WiimoteInput.isConnected)
        {
            if (!Spraying && WiimoteInput.isShaking())
            {
                Refill();
            }
            else if (WiimoteInput.isSprayButtonPressed && !Mathf.Approximately(0, CurrentFill))
            {
                spraying = true;
                CurrentFill -= SprayRate * Time.deltaTime;
            }
            else if (Spraying)
            {
                spraying = false;
            }

            if (Input.GetButtonDown("Fire2"))
            {
                ChangeColor(Color.green);
            }
        }
        else
        {
            if (!Spraying && !Mathf.Approximately(Input.GetAxis("Refill"), 0))
            {
                Refill();
            }
            else if (Input.GetButton("Spray") && !Mathf.Approximately(0, CurrentFill))
            {
                spraying = true;
                CurrentFill -= SprayRate * Time.deltaTime;
            }
            else if (Spraying)
            {
                spraying = false;
            }

            if (Input.GetButtonDown("Fire2"))
            {
                ChangeColor(Color.green);
            }
        }
    }

    public void ChangeColor(Color _color)
    {
        var oldColor = color;
        color = _color;
        OnColorChanged?.Invoke(oldColor, _color);
    }

    public void Refill()
    {
        CurrentFill += RefillRate;
    }

    public void Refill(float value)
    {
        CurrentFill += value;
    }

    public void RefillMax()
    {
        CurrentFill = MaxCapacity;
    }
}
