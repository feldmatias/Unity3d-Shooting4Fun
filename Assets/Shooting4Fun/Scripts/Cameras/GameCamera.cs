using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [Header ("Position")]
    public float backOffset;
    public float backAimingOffset;
    public float verticalOffset;
    public float horizontalAimingOffset;

    [Header("Rotation")]
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;

    private Player target;
    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    // Start is called before the first frame update
    void Start()
    {
        target = AssetsManager.Instance.Player;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SetRotation();
        SetPosition();
    }

    void SetPosition()
    {
        var currentBackOffset = target.playerAim.IsAiming ? backAimingOffset : backOffset;
        var horizontalOffset = target.playerAim.IsAiming ? horizontalAimingOffset : 0f;
        transform.position = target.transform.position - transform.forward * currentBackOffset + transform.up * verticalOffset + transform.right * horizontalOffset;
    }

    void SetRotation()
    {
        float mouseX = Input.GetAxis(MouseInput.MOUSE_X);
        float mouseY = -Input.GetAxis(MouseInput.MOUSE_Y);

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }
}
