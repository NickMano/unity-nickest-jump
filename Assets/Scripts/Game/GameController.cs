using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour
{
    // MARK: Private properties
    private AudioSource music;
    private GameState currentState = GameState.standBy;

    [SerializeField] private PlayerController player;
    [SerializeField] private EnemyGeneratorController enemyGenerator;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private ParallaxController parallax;
    [SerializeField] private GameHUD hud;
    [SerializeField] private ScoreController scoreController;

    // MARK: Public properties
    public float scaleTime = 6f;
    public float timeInc = .25f;

    void Awake()
    {
        music = GetComponent<AudioSource>();
    }

    // MARK: Lifecycle methods
    void Start()
    {
        hud.ShowStandby();
    }

    void OnEnable()
    {
        inputHandler.JumpPressed += HandleJumpPressed;
        player.Died += EndGame;
        player.DeathAnimationFinished += SetReady;
        player.PointCollected += AddPoints;
    }

    void OnDisable()
    {
        inputHandler.JumpPressed -= HandleJumpPressed;
        player.Died -= EndGame;
        player.DeathAnimationFinished -= SetReady;
        player.PointCollected -= AddPoints;

    }

    // MARK: Public methods
    public void AddPoints()
    {
        if (!IsPlaying()) return;
        scoreController.AddPoints(1);
    }

    public bool IsPlaying() => currentState == GameState.jugando;

    public void RestartTimeScale(float time = 1f)
    {
        CancelInvoke(nameof(GameTimeScale));
        Time.timeScale = time;
    }

    private void StartGame()
    {
        hud.ShowPlaying();
        parallax.StartScrolling();
        music.Play();
        currentState = GameState.jugando;

        player.StartRunning();
        enemyGenerator.StartGeneration();
        InvokeRepeating(nameof(GameTimeScale), scaleTime, scaleTime);
    }

    private void RestartGame()
    {
        RestartTimeScale();
        SceneManager.LoadScene("Main");
    }

    private void EndGame()
    {
        if (!IsPlaying()) return;

        currentState = GameState.muerto;
        RestartTimeScale(0.7f);
        music.Stop();
        enemyGenerator.Finish();
        parallax.StopScrolling();
    }

    private void SetReady()
    {
        if (currentState != GameState.muerto) return;
        currentState = GameState.listo;
    }

    // MARK: Internal methods
    void GameTimeScale()
    {
        Time.timeScale += timeInc;
    }

    private void HandleJumpPressed()
    {
        switch (currentState)
        {
            case GameState.standBy:
                StartGame();
                break;

            case GameState.jugando:
                player.TryJump();
                break;

            case GameState.listo:
                RestartGame();
                break;

            default: break;
        }
    }
}
