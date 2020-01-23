using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReticleFollow : MonoBehaviour
{
    private RectTransform ReticleTransform;
    private Camera _mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        ReticleTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sprayScreenPos;

        if (WiimoteInput.isConnected)
        {
            Vector2 wiiPointerPos = WiimoteInput.GetPointerPosition();
            ReticleTransform.position = new Vector3(wiiPointerPos.x, wiiPointerPos.y, 0);

        }
        else
        {
            ReticleTransform.position = Input.mousePosition;
        }

        


        // Aiming
        //PositionReticle(sprayScreenPos);

    }
    private void PositionReticle(Vector3 i_sprayScreenPosition)
    {

        Vector3 sprayScreenPositionCamera = new Vector3(i_sprayScreenPosition.x, i_sprayScreenPosition.y, _mainCamera.nearClipPlane);
        Vector3 reticlePos = _mainCamera.ScreenToWorldPoint(sprayScreenPositionCamera);

        ReticleTransform.position = reticlePos;
    }
}
