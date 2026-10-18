using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Rigidbody2D rg;

    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rg.linearVelocity = speed * Vector2.left;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Destructor"))
            Destroy(gameObject);
    }
}
