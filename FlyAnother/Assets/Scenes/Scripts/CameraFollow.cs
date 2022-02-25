using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerCam;
    [SerializeField] private Transform mouseAim;

    [SerializeField] private float sensivity = 3;
    [SerializeField] private float turnSpeed = 2;
    [SerializeField] private float limitY = 80; //спорная вещь, в зависимости от геймплея можно убрать

    [SerializeField] private Vector3 offset = new Vector3 (0, 1, -10);
    
    private float mouseX;
    private float mouseY;
    private float hudDistance = 500f;
    private bool isMouseAimFrozen = false;
    private Vector3 frozenDirection = Vector3.forward;

    private void Update()
    {
        /*Vector3 upVec = (Mathf.Abs(mouseAim.forward.y) > 0.9f) ? player.transform.up : Vector3.up;
        player.transform.rotation = Damp(player.transform.rotation,
                                      Quaternion.LookRotation(mouseAim.forward, upVec),
                                      turnSpeed,
                                      Time.deltaTime);*/

        //этот вариант проще и ведет себя почти так же
        Vector3 direction = MouseAimPos - player.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {        
        mouseX = transform.localEulerAngles.y + Touchscreen.current.touches[0].delta.ReadValue().x * sensivity;
        mouseY += Touchscreen.current.touches[0].delta.ReadValue().y * sensivity;
        mouseY = Mathf.Clamp(mouseY, -limitY, limitY);
        transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0);
        transform.position = transform.localRotation * offset + player.transform.position;        
    }

    public Vector3 BoresightPos
    {
        get
        {
            return player == null
                 ? transform.forward * hudDistance 
                 : (player.transform.forward * hudDistance) + player.transform.position;
        }
    }

    public Vector3 MouseAimPos
    {
        get
        {
            if (mouseAim != null)
            {
                return isMouseAimFrozen
                    ? GetFrozenMouseAimPos()
                    : mouseAim.position + (mouseAim.forward * hudDistance);
            }
            else
            {
                return transform.forward * hudDistance;
            }
        }
    }

    private Vector3 GetFrozenMouseAimPos()
    {
        if (mouseAim != null)
            return mouseAim.position + (frozenDirection * hudDistance);
        else
            return transform.forward * hudDistance;
    }

    /*private Quaternion Damp(Quaternion a, Quaternion b, float lambda, float dt)
    {
        return Quaternion.Slerp(a, b, 1 - Mathf.Exp(-lambda * dt));
    }*/
}
