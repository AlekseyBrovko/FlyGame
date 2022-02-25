using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] private CameraFollow mouseFlight;
    [SerializeField] private RectTransform boresight;
    [SerializeField] private RectTransform mousePos;
    [SerializeField] private Camera playerCam;

    private void Update()
    {
        UpdateGraphics(mouseFlight);
    }

    private void UpdateGraphics(CameraFollow controller)
    {
        if (boresight != null)
        {
            boresight.position = playerCam.WorldToScreenPoint(controller.BoresightPos);
            boresight.gameObject.SetActive(boresight.position.z > 1f);
        }

        if (mousePos != null)
        {
            mousePos.position = playerCam.WorldToScreenPoint(controller.MouseAimPos);
            mousePos.gameObject.SetActive(mousePos.position.z > 1f);
        }
    }
}
