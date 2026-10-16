using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController: MonoBehaviour {
    [SerializeField] private float vel = 2f;
    private Rigidbody2D rg;

    void Awake() {
        rg = GetComponent<Rigidbody2D>();
    }
	
	void Start () {
        rg.velocity = vel * Vector2.left;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.tag == "Destructor")
            Destroy(gameObject);
	}
}
