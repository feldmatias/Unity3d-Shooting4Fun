using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Player player;
    private GameManager gameManager;

    public float weaponChangeTimeout = 1;
    private float weaponChangeTimer;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessGamePaused();
        ProcessShowMap();
        ProcessPlayerMovementInput();
        ProcessPlayerAiming();
        ProcessPlayerShooting();
        ProcessPlayerWeaponsChange();
    }

    private void ProcessGamePaused()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gameManager.TogglePausedGame();
        }
    }

    private void ProcessShowMap()
    {
        if (Input.GetKeyUp(KeyCode.M)) {
            gameManager.HideMap();
        } else if (Input.GetKeyDown(KeyCode.M)) {
            gameManager.ShowMap();
        }
    }

    private void ProcessPlayerMovementInput()
    {
        bool running = Input.GetKey(KeyCode.LeftShift);
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.A)){
            direction -= AssetsManager.Instance.MainCamera.transform.right;
        } else if (Input.GetKey(KeyCode.D)){
            direction += AssetsManager.Instance.MainCamera.transform.right;
        }

        if (Input.GetKey(KeyCode.W)){
            direction += AssetsManager.Instance.MainCamera.transform.forward;
        } else if (Input.GetKey(KeyCode.S)){
            direction -= AssetsManager.Instance.MainCamera.transform.forward;
        }


        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C)){
            player.ToggleCrouch();
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            player.Jump();
        }

        if (direction != Vector3.zero){
            direction.y = 0;
            player.Move(direction.normalized, running);
        } else {
            player.StopMoving();
        }
    }

    private void ProcessPlayerAiming()
    {
        bool isAiming = Input.GetKey(KeyCode.Mouse1);
        player.SetIsAiming(isAiming);
    }

    private void ProcessPlayerShooting()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            player.ShootOnce();
        } else if (Input.GetKey(KeyCode.Mouse0)){
            player.ShootContinually();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            player.ReloadWeapon();
        }
    }

    private void ProcessPlayerWeaponsChange()
    {
        if (weaponChangeTimer <= 0){
            if (Input.GetAxis(MouseInput.MOUSE_SCROLL) > 0f)
            {
                // forward
                player.ChangeWeapon(1);
                weaponChangeTimer = weaponChangeTimeout;
            }
            else if (Input.GetAxis(MouseInput.MOUSE_SCROLL) < 0f)
            {
                // backwards
                player.ChangeWeapon(-1);
                weaponChangeTimer = weaponChangeTimeout;
            }
        } else
        {
            weaponChangeTimer -= Time.deltaTime;
        }
    }
}
