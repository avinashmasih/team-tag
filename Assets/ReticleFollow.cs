using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReticleFollow : MonoBehaviour
{
    private RectTransform reticle;
    private Camera _mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        RectTransform reticle = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sprayScreenPos = Input.mousePosition;

        // Aiming
        PositionReticle(sprayScreenPos);

    }
    private void PositionReticle(Vector3 i_sprayScreenPosition)
    {

        Vector3 sprayScreenPositionCamera = new Vector3(i_sprayScreenPosition.x, i_sprayScreenPosition.y, _mainCamera.nearClipPlane);
        Vector3 reticlePos = _mainCamera.ScreenToWorldPoint(sprayScreenPositionCamera);

        reticle.position = reticlePos;
    }
}
