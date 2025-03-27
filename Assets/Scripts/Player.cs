using UnityEngine;

public class Player : MonoBehaviour
{
    //Animation stuff
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    public Sprite[] sprites;
    private int spriteIndex;
    //Control stuff
    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }
    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }
    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }
       if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                direction = Vector3.up * strength;
            }
        }
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (SoundPlayer.Instance != null)
                SoundPlayer.Instance.PlaySound(SoundPlayer.Instance.splat);

            GameManager.Instance?.GameOver();
        }
        else if (other.CompareTag("Scoring"))
        {
            if (SoundPlayer.Instance != null)
                SoundPlayer.Instance.PlaySound(SoundPlayer.Instance.pop);

            GameManager.Instance?.IncreaseScore();
        }
    }
    private void AnimateSprite()
    {
        spriteIndex++;
        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }
        spriteRenderer.sprite = sprites[spriteIndex];
    }
}
