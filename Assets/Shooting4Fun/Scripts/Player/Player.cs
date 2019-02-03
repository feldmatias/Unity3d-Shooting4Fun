using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDeathable
{
    [Header("Movement")]
    public float movementSpeed;
    public float runningMultiplier;
    public float crouchingMultiplier;
    public float rotationSpeed;
    public float jumpForce;
    public float airJumpForce;
    public PlayerJump playerJump;
    public PlayerAim playerAim;
    public Stamina playerStamina;

    [Header("Weapons")]
    public PlayerWeapon[] weapons;

    private Rigidbody rigidBody;
    private Quaternion targetRotation;
    private Animator animator;

    private int weaponIndex = 0;
    private bool isCrouching = false;
    private bool isDead = false;

    public PlayerWeapon SelectedWeapon { get { return weapons[weaponIndex]; } }
    public bool IsAiming {  get { return playerAim.IsAiming; } }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        targetRotation = Quaternion.Euler(transform.forward);
        foreach (var weapon in weapons){
            weapon.SetActive(false);
        }
        SelectedWeapon.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Animate();
    }

    public void Move(Vector3 direction, bool running)
    {
        var speed = movementSpeed;
        var canRun = running && CanRun();
        if (isCrouching){
            speed *= crouchingMultiplier;
        } else if (canRun && playerStamina.HasStamina()){
            speed *= runningMultiplier;
        }

        float verticalVelocity = rigidBody.velocity.y;
        var velocity = direction * speed * Time.deltaTime;
        velocity.y = verticalVelocity;
        rigidBody.velocity = velocity;

        targetRotation = Quaternion.LookRotation(direction);

        if (playerJump.IsGrounded()){
            AudioManager.Instance.PlayFootsteps(transform.position, canRun && playerStamina.HasStamina());
        }

        if (canRun){
            playerStamina.DecreaseStamina();
        } else {
            playerStamina.IncreaseStamina();
        }
    }

    public void StopMoving()
    {
        playerStamina.IncreaseStamina();
    }

    public void ToggleCrouch(){
        animator.SetTrigger(PlayerAnimationTags.CROUCH_TRIGGER);
        isCrouching = !isCrouching;
    }

    public void Jump()
    {
        if (isCrouching){
            ToggleCrouch();
        } else if (playerJump.CanJump() && !playerAim.IsAiming) {
            var force = playerJump.IsGrounded() ? jumpForce : airJumpForce;
            rigidBody.AddForce(transform.up * force, ForceMode.Impulse);
            playerJump.Jump();
            animator.SetTrigger(PlayerAnimationTags.JUMP_TRIGGER);
            AudioManager.Instance.PlayJumpAudio(transform.position);
        }
    }

    public void SetIsAiming(bool isAiming)
    {
        playerAim.SetIsAiming(isAiming && !isCrouching);
    }

    private void Rotate()
    {
        if (playerAim.IsAiming){
            targetRotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Animate()
    {
        //Plane movement (walk, run)
        var planeVelocity = rigidBody.velocity;
        planeVelocity.y = 0;

        var planeSpeed = planeVelocity.magnitude;
        animator.SetFloat(PlayerAnimationTags.PLANE_VELOCITY, planeSpeed);

        //Vertical movement (jump, fall)
        animator.SetFloat(PlayerAnimationTags.VERTICAL_VELOCITY, rigidBody.velocity.y);
    }

    public void ShootOnce()
    {
        if (playerAim.IsAiming){
            SelectedWeapon.Shoot();
        }
    }

    public void ShootContinually()
    {
        if (playerAim.IsAiming && SelectedWeapon.shootsContinually) {
            SelectedWeapon.Shoot();
        }
    }

    public void ChangeWeapon(int direction)
    {
        SelectedWeapon.SetActive(false);
        weaponIndex += Math.Sign(direction);
        if (weaponIndex < 0){
            weaponIndex = weapons.Length - 1;
        } else if (weaponIndex >= weapons.Length) {
            weaponIndex = 0;
        }
        SelectedWeapon.SetActive(true);
    }

    public void ReloadWeapon()
    {
        SelectedWeapon.Reload();
    }

    private bool CanRun()
    {
        return !playerAim.IsAiming && playerJump.IsGrounded() && !isCrouching;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void Die()
    {
        isDead = true;
        animator.SetTrigger(PlayerAnimationTags.DEAD_TRIGGER);
        GetComponentInParent<GameManager>().EndGame();
    }
}
