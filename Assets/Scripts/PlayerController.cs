using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private static readonly int JugadorMuertoHash = Animator.StringToHash("JugadorMuerto");
    private static readonly int JugadorSaltandoHash = Animator.StringToHash("JugadorSaltando");
    private static readonly int JugadorCorriendoHash = Animator.StringToHash("JugadorCorriendo");

    [SerializeField] private GameCrontroller game;
    [SerializeField] private AudioClip jugadorSaltando;
    [SerializeField] private AudioClip jugadorMuriendo;
    [SerializeField] private AudioClip puntoSonido;
    [SerializeField] private ParticleSystem dust;

    private AudioSource sound;
    private Animator animator;
    private bool isJumping = false;
    private bool isDead = false;

    public event Action Died;
    public event Action DeathAnimationFinished;

    void Awake()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    public void OnStartJumping() => dust.Stop();

    public void OnFinishedJumping()
    {
        isJumping = false;
        dust.Play();
    }

    public void StartRunning()
    {
        isDead = false;
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

    private bool CanJump() => game.IsPlaying() && !isJumping;

    private void Jump()
    {
        isJumping = true;
        animator.Play(JugadorSaltandoHash);
        sound.clip = jugadorSaltando;
        sound.Play();
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        dust.Stop();

        sound.clip = jugadorMuriendo;
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
                game.IncPuntos();
                sound.clip = puntoSonido;
                sound.Play();
                break;

            default:
                break;
        }
    }
}
