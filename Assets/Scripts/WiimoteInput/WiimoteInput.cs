using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using WiimoteApi;

public class WiimoteInput : MonoBehaviour
{
    [Header("Screen Parameters")]
    public static int screenWidth = 1920;
    public static int screenHeight = 1080;

    [Header("Shaking Parameters")]
    public static float tolerance = 0.7f;
    public static float accelerometerIntensityMultiplier = 100f;
    /// <summary>
    /// Returns true if analog is pressed, false otherwise.
    /// </summary>
    public static bool isSprayButtonPressed
    {
        get { return _remote.Button.b; }
    }

    private static bool prevButtonDown;
    private static bool currButtonDown;

    public static bool isSprayButtonDownThisFrame()
    {
        return (!prevButtonDown && currButtonDown);
    }

    private static Wiimote _remote;

    public static bool isConnected;

    // Start is called before the first frame update
    void Start()
    {
        //Find and stor Wiimote ref in variable
        WiimoteManager.FindWiimotes();
        if (WiimoteManager.Wiimotes.Count != 0)
        {
            _remote = WiimoteManager.Wiimotes[0];
            _remote.SendPlayerLED(false, false, false, false);

            isConnected = true;

            //Setup IR Camera
            if (_remote.SetupIRCamera(IRDataType.BASIC))
            {
                Debug.Log("IR Setup Successful!");
            }
            else
            {
                Debug.Log("IR Setup Failed!");
            }
        }
        else
        {
            isConnected = false;

            Debug.Log("Wiimote not connected!");
        }
    }


    /// <summary>
    /// Returns the pixel coordinates of the screen, pointed by the Wiimote.
    /// </summary>
    public static Vector2 GetPointerPosition()
    {
        int ret;
        float[] position;
        do
        {
            //Get sensor value from remote
            position = _remote.Ir.GetPointingPosition();

            //Wiimote considers origin as bottom left so invert y coordinates
            position[1] = position[1];

            //Scale it with respect to the screen resolution
            position[0] *= screenWidth;
            position[1] *= screenHeight;

            //Clamp it to avoid going out of the screen and mess up the screenToWorld function
            position[0] = Mathf.Clamp(position[0], 0, screenWidth);
            position[1] = Mathf.Clamp(position[1], 0, screenHeight);
            ret = _remote.ReadWiimoteData();

        } while (ret > 0);

        return new Vector2(position[0], position[1]);
    }

    /// <summary>
    /// Approximates accelerator value. Will return true if value goes beyond a threshold
    /// </summary>
    public static bool isShaking()
    {
        int ret;
        bool isShaking = false;
        do
        {
            float[] accelData = _remote.Accel.GetCalibratedAccelData();
            float sum = 0.0f;
            for (int i = 0; i < accelData.Length; ++i)
            {
                accelData[i] *= accelerometerIntensityMultiplier;
                accelData[i] = Mathf.Clamp(accelData[i], -accelerometerIntensityMultiplier, accelerometerIntensityMultiplier);
                sum += accelData[i];
            }

            if ((sum / 3.0f) > (tolerance * accelerometerIntensityMultiplier) || (sum / 3.0f) < (tolerance * -accelerometerIntensityMultiplier))
            {
                isShaking = true;
                //_remote.RumbleOn = true; // Enabled Rumble
                //_remote.SendStatusInfoRequest(); // Requests Status Report, encodes Rumble into input report
                //Thread.Sleep(500); // Wait 0.5s
                //_remote.RumbleOn = false; // Disabled Rumble
                //_remote.SendStatusInfoRequest(); // Requests Status Report, encodes Rumble into input report
            }
            ret = _remote.ReadWiimoteData();
        } while (ret > 0);

        return isShaking;
    }

    private void Update()
    {
        if (isConnected)
        {
            prevButtonDown = currButtonDown;
            currButtonDown = _remote.Button.b;
        }
        
    }
}
