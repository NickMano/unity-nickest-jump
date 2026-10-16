using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public Animator animator;
    public GameObject game;
    public GameObject enemigo;
    public AudioClip jugadorSaltando;
    public AudioClip jugadorMuriendo;
    public AudioClip puntoSonido;
    public ParticleSystem polvo;

    private AudioSource sonido;
    private float posY;

	void Awake() {
	    animator = GetComponent<Animator>();
        sonido = GetComponent<AudioSource>();
	}
	
	void Start() {
        posY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        bool enJuego = game.GetComponent<GameCrontroller>().gamestate == GameState.jugando;
        bool accion = Input.GetKeyDown("up") || Input.GetKeyDown("space") || Input.GetMouseButton(0);
        bool caminando = transform.position.y == posY;
        if (enJuego && accion && caminando)
        {
            UpdateState("JugadorSaltando");
            sonido.clip = jugadorSaltando;
            sonido.Play();
        }
	}

    public void UpdateState(string state = null){
        if(state != null){
            animator.Play(state);
        }
    }

    void ConPolvo(){
        polvo.Play();
    }

    void SinPolvo(){
        polvo.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemigo")
        {
			UpdateState("JugadorMuerto");
            game.GetComponent<GameCrontroller>().gamestate = GameState.muerto;
            enemigo.SendMessage("TerminarGeneracionEnemigo", true);
            game.SendMessage("ReiniciarTimeScale", 0.7f);
            game.GetComponent<AudioSource>().Stop();
            sonido.clip = jugadorMuriendo;
            sonido.Play();

            SinPolvo();
            return;
        }
        
        if(otro.gameObject.tag == "Puntos") {
            game.SendMessage("IncPuntos");
            sonido.clip = puntoSonido;
            sonido.Play();
        }
    }

    void EstaListo() {
        game.GetComponent<GameCrontroller>().gamestate = GameState.listo;
    }
}
