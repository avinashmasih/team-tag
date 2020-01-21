#if DEBUG
#define SPRAY_DEBUG
#endif

using UnityEngine;
using UnityEngine.UI;

[System.Obsolete("Use SprayPaint component instead.")]
[RequireComponent(typeof(SprayCan))]
public class Spray : MonoBehaviour
{
    public Vector3         SprayLocation { get => sprayLocation; }
    public RectTransform ReticleTransform;

    private Camera          mainCamera;
    private SprayCan        sprayCan;
    private Vector3         sprayLocation;



    // Start is called before the first frame update
    void Start()
    {
        transform.position    = Vector3.zero;
        mainCamera            = Camera.main;
        Cursor.visible        = false;
        sprayCan              = GetComponent<SprayCan>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 sprayScreenPos = Input.mousePosition;

        // Aiming
        PositionReticle(sprayScreenPos);

        // Get the final location of the spray if the button is pressed
        if (sprayCan.Spraying)
        {
            CastSpray(sprayScreenPos, out sprayLocation);
        }
    }


    // Position reticle in the world space in front of the camera
    private void PositionReticle(Vector3 i_sprayScreenPosition)
    {

        ReticleTransform.anchoredPosition = i_sprayScreenPosition;
    }


    // Get the final world space location of the spray
    private void CastSpray(Vector3 i_sprayScreenPos, out Vector3 o_canvasPoint)
    {
        Ray         sprayLine = mainCamera.ScreenPointToRay(i_sprayScreenPos);
        RaycastHit  sprayHit;

        if (Physics.Raycast(sprayLine.origin, sprayLine.direction.normalized, out sprayHit, Mathf.Infinity))
        {

#if SPRAY_DEBUG
            Debug.DrawRay(sprayLine.origin, sprayLine.direction.normalized * sprayHit.distance, Color.red);
#endif

            o_canvasPoint = sprayHit.point;
        }
        else
        {
            o_canvasPoint = Vector3.zero;
        }
    }
}
