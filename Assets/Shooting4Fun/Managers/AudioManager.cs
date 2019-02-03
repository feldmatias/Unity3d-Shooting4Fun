using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Clips")]
    public AudioClip explosionAudio;
    public AudioClip jumpAudio;
    public AudioClip pistolShootAudio;
    public AudioClip subfusilShootAudio;
    public AudioClip shotgunShootAudio;
    public AudioClip assaultRifleShootAudio;
    public AudioClip rocketLauncherShootAudio;
    public AudioClip grenadeLauncherShootAudio;
    public AudioClip reloadAudio;
    public AudioClip boosterPickAudio;

    public AudioClip noAmmoAudio;
    public float noAmmoAudioTimeout = 0.2f;
    public float noAmmoAudioVolume = 1.5f;
    private float noAmmoAudioTimer = 0;

    public AudioClip[] footstepsAudios;
    public float footstepsWalkTimeout = 0.5f;
    public float footstepsRunTimeout = 0.2f;
    public float footstepsVolume = 0.7f;
    private float footstepsAudioTimer = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void PlayAudio(AudioClip audio, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audio, position);
    }

    private void PlayAudio(AudioClip audio, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(audio, position, volume);
    }

    public void PlayExplosionAudio(Vector3 position)
    {
        PlayAudio(explosionAudio, position);
    }

    public void PlayPistolShoot(Vector3 position)
    {
        PlayAudio(pistolShootAudio, position);
    }

    public void PlaySubfusilShoot(Vector3 position)
    {
        PlayAudio(subfusilShootAudio, position);
    }

    public void PlayShotgunShoot(Vector3 position)
    {
        PlayAudio(shotgunShootAudio, position);
    }

    public void PlayAssaultRifleShoot(Vector3 position)
    {
        PlayAudio(assaultRifleShootAudio, position);
    }

    public void PlayGrenadeLauncherShoot(Vector3 position)
    {
        PlayAudio(grenadeLauncherShootAudio, position);
    }

    public void PlayRocketLauncherShoot(Vector3 position)
    {
        PlayAudio(rocketLauncherShootAudio, position);
    }

    public void PlayWeaponReload(Vector3 position)
    {
        PlayAudio(reloadAudio, position);
    }

    public void PlayNoAmmo(Vector3 position)
    {
        if (Time.time - noAmmoAudioTimer > noAmmoAudioTimeout){
            noAmmoAudioTimer = Time.time;
            PlayAudio(noAmmoAudio, position, noAmmoAudioVolume);
        }
    }

    public void PlayJumpAudio(Vector3 position)
    {
        PlayAudio(jumpAudio, position);
    }

    public void PlayFootsteps(Vector3 position, bool running)
    {
        var timer = running ? footstepsRunTimeout : footstepsWalkTimeout;
        if (Time.time - footstepsAudioTimer > timer)
        {
            footstepsAudioTimer = Time.time;
            PlayAudio(footstepsAudios[Random.Range(0, footstepsAudios.Length)], position, footstepsVolume);
        }
    }

    public void PlayBosoterPickAudio(Vector3 position)
    {
        PlayAudio(boosterPickAudio, position);
    }
}
