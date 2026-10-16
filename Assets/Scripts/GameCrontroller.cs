using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCrontroller: MonoBehaviour {
    [Range (0f, 0.20f)]
    public float parallaxSpeed = 0.03f;
    public RawImage Background, plataform;
    public GameObject standByUI;
    public GameObject scoreUI;
    public GameObject player;
    public GameObject enemy;
    public Text textScore;
    public Text record;

    private AudioSource music;

    public float scaleTine = 6f;
    public float timeInc = .25f;

    public GameState state = GameState.quieto;
    private int score = 0;

	// Use this for initialization
	void Start() {
        music = GetComponent<AudioSource>();
        record.text = "MEJOR: " + GetMaxPuntos().ToString();
	}
	
	// Update is called once per frame
	void Update() {
        bool isJumpping = IsJumpping();
        
        switch (state) {
            case GameState.quieto:
                if (!isJumpping) { break; }
                standByUI.SetActive(false);
                scoreUI.SetActive(true);
                music.Play();
                state = GameState.jugando;
                standByUI.SetActive(false);
                player.SendMessage("UpdateState", "JugadorCorriendo");
                player.SendMessage("ConPolvo");
                enemy.SendMessage("ComenzarGeneracionEnemigo");
                InvokeRepeating("GameTimeScale", ScaleTime, ScaleTime);
                break;
            case GameState.jugando:
                Parallax()
                break;
            case GameState.listo:
                if (isJumpping) {
                    ReiniciarJuego();
                }
                break;
        }
	}

    void Parallax() {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
        plataform.uvRect = new Rect(plataform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
    }

    void GameTimeScale() {
        Time.timeScale += timeInc;
    }

    void ReiniciarTimeScale(float time = 1f) {
        CancelInvoke("GameTimeScale");
        Time.timeScale = time;
    }

    public void ReiniciarJuego() {
        ReiniciarTimeScale();
        SceneManager.LoadScene("Juego");
    }

    public void IncPuntos() {
        textScore.text = (++score).ToString();
        if (score >= GetMaxPuntos()) {
            record.text = "MEJOR: " + score.ToString();
            SetMaxPuntos(score);
        }
    }

    public int GetMaxPuntos() {
        return PlayerPrefs.GetInt("Max Puntaje");
    }

    public void SetMaxPuntos(int currentScore) {
        PlayerPrefs.SetInt("Max Puntaje", currentScore);
    }
    
    bool IsJumpping() {
        return Input.GetKeyDown("up") || 
            Input.GetKeyDown("space") || 
            Input.GetMouseButton(0);
    }
}
