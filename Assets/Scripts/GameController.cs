using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour
{
    // MARK: Private properties
    private AudioSource music;
    private int score = 0;
    private GameState currentState = GameState.standBy;

    [SerializeField] private PlayerController player;
    [SerializeField] private EnemyGeneratorController enemyGenerator;
    [SerializeField] private InputHandler inputHandler;

    // MARK: Public properties
    [Range(0f, 0.20f)]
    public float parallaxSpeed = 0.03f;

    public RawImage background, plataform;
    public GameObject standByUI;
    public GameObject scoreUI;

    public Text textScore;
    public Text record;

    public float scaleTime = 6f;
    public float timeInc = .25f;

    void Awake()
    {
        music = GetComponent<AudioSource>();
    }

    // MARK: Lifecycle methods
    void Start()
    {
        record.text = "MEJOR: " + GetMaxPuntos().ToString();
    }

    void OnEnable()
    {
        inputHandler.JumpPressed += HandleJumpPressed;
        player.Died += EndGame;
        player.DeathAnimationFinished += SetReady;
        player.PointCollected += IncPuntos;
    }

    void OnDisable()
    {
        inputHandler.JumpPressed -= HandleJumpPressed;
        player.Died -= EndGame;
        player.DeathAnimationFinished -= SetReady;
        player.PointCollected -= IncPuntos;

    }

    void Update()
    {
        if (!IsPlaying()) return;
        Parallax();
    }

    // MARK: Public methods
    public void IncPuntos()
    {
        if (!IsPlaying()) return;

        textScore.text = (++score).ToString();
        if (score >= GetMaxPuntos())
        {
            record.text = "MEJOR: " + score.ToString();
            SetMaxPuntos(score);
        }
    }

    public int GetMaxPuntos()
    {
        return PlayerPrefs.GetInt("Max Puntaje");
    }

    public void SetMaxPuntos(int currentScore)
    {
        PlayerPrefs.SetInt("Max Puntaje", currentScore);
    }

    public bool IsPlaying() => currentState == GameState.jugando;

    public void ReiniciarTimeScale(float time = 1f)
    {
        CancelInvoke(nameof(GameTimeScale));
        Time.timeScale = time;
    }

    private void StartGame()
    {
        standByUI.SetActive(false);
        scoreUI.SetActive(true);
        music.Play();
        currentState = GameState.jugando;
        player.StartRunning();
        enemyGenerator.StartGeneration();
        InvokeRepeating(nameof(GameTimeScale), scaleTime, scaleTime);
    }

    private void RestartGame()
    {
        ReiniciarTimeScale();
        SceneManager.LoadScene("Main");
    }

    private void EndGame()
    {
        if (!IsPlaying()) return;

        currentState = GameState.muerto;
        ReiniciarTimeScale(0.7f);
        music.Stop();
        enemyGenerator.Finish();
    }

    private void SetReady()
    {
        if (currentState != GameState.muerto) return;
        currentState = GameState.listo;
    }

    // MARK: Internal methods
    void Parallax()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
        plataform.uvRect = new Rect(plataform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
    }

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
