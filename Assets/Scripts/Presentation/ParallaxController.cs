using UnityEngine;
using UnityEngine.UI;

public class ParallaxController : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 0.20f)]
    private float parallaxSpeed = 0.03f;
    private bool isScrolling = false;

    [SerializeField] private RawImage background;
    [SerializeField] private RawImage plataform;

    void Update()
    {
        if (!isScrolling) return;

        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
        plataform.uvRect = new Rect(plataform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
    }

    public void StartScrolling() => isScrolling = true;

    public void StopScrolling() => isScrolling = false;
}
