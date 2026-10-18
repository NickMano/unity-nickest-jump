using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private static readonly int JugadorMuertoHash = Animator.StringToHash("JugadorMuerto");
    private static readonly int JugadorSaltandoHash = Animator.StringToHash("JugadorSaltando");
    private static readonly int JugadorCorriendoHash = Animator.StringToHash("JugadorCorriendo");

    [SerializeField] private GameController game;
    [SerializeField] private AudioClip jumpingAudioClip;
    [SerializeField] private AudioClip dieAudioClip;
    [SerializeField] private AudioClip pointAudioClip;
    [SerializeField] private ParticleSystem dust;

    private AudioSource sound;
    private Animator animator;
    private bool isJumping = false;
    private bool isDead = false;

    public event Action Died;
    public event Action DeathAnimationFinished;
    public event Action PointCollected;

    void Awake()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    public void OnStartJumping() => dust.Stop();

    public void OnFinishedJumping()
    {
        isJumping = false;
        if (isDead) return;
        dust.Play();
    }

    public void StartRunning()
    {
        isDead = false;
        isJumping = false;
        animator.Play(JugadorCorriendoHash);
        dust.Play();
    }

    public void OnDeathAnimationFinished()
    {
        DeathAnimationFinished?.Invoke();
    }

    public void TryJump()
    {
        if (!CanJump()) return;
        Jump();
    }

    private bool CanJump() => game.IsPlaying() && !isJumping && !isDead;

    private void Jump()
    {
        isJumping = true;
        animator.Play(JugadorSaltandoHash);
        sound.clip = jumpingAudioClip;
        sound.Play();
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        dust.Stop();

        sound.clip = dieAudioClip;
        sound.Play();

        animator.Play(JugadorMuertoHash);
        Died?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!game.IsPlaying()) return;

        switch (other.gameObject.tag)
        {
            case "Enemigo":
                Die();
                break;

            case "Puntos":
                PointCollected?.Invoke();
                sound.clip = pointAudioClip;
                sound.Play();
                break;

            default:
                break;
        }
    }
}
