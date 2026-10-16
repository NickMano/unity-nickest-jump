using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCrontroller : MonoBehaviour {

    [Range (0f, 0.20f)]
    public float parallaxSpeed = 0.03f;
    public RawImage background, plataform;
    public GameObject uiQuieto;
    public GameObject uiPuntos;
    public GameObject jugador;
    public GameObject enemy;
    public Text puntosTexto;
    public Text record;

    private AudioSource musica;

    public float escaleTime = 6f;
    public float timeInc = .25f;

    public GameState gamestate = GameState.quieto;
    private int puntos = 0;

	// Use this for initialization
	void Start () {
        musica = GetComponent<AudioSource>();
        record.text = "MEJOR: " + GetMaxPuntos().ToString();
	}
	
	// Update is called once per frame
	void Update () {
        bool salta = Input.GetKeyDown("up") || Input.GetKeyDown("space") || Input.GetMouseButton(0);

        //empieza el juego
        if (gamestate == GameState.quieto && salta)
        {
            uiQuieto.SetActive(false);
            uiPuntos.SetActive(true);
            musica.Play();
            gamestate = GameState.jugando;
            uiQuieto.SetActive(false);
            jugador.SendMessage("UpdateState", "JugadorCorriendo");
            jugador.SendMessage("ConPolvo");
            enemy.SendMessage("ComenzarGeneracionEnemigo");
            InvokeRepeating("GameTimeScale", escaleTime, escaleTime);
        }
        //juego en marcha
        else if (gamestate == GameState.jugando)
        {
            Parallax();
        }
        else if (gamestate == GameState.listo)
        {
            if (salta)
            {
                ReiniciarJuego();
            }
        }
	}

    void Parallax(){
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
        plataform.uvRect = new Rect(plataform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
    }

    void GameTimeScale(){
        Time.timeScale += timeInc;
    }

    void ReiniciarTimeScale(float tiempo = 1f){
        CancelInvoke("GameTimeScale");
        Time.timeScale = tiempo;
    }

    public void ReiniciarJuego(){
        ReiniciarTimeScale();
        SceneManager.LoadScene("Juego");
    }

    public void IncPuntos(){
        puntosTexto.text = (++puntos).ToString();
        if(puntos >= GetMaxPuntos()){
            record.text = "MEJOR: " + puntos.ToString();
            SetMaxPuntos(puntos);
        }
    }

    public int GetMaxPuntos(){
        return PlayerPrefs.GetInt("Max Puntaje");
    }

    public void SetMaxPuntos(int puntajeActual){
        PlayerPrefs.SetInt("Max Puntaje", puntajeActual);
    }
}
