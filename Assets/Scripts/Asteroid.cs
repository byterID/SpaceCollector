using UnityEngine;

// Скрипт астероида.
// Астероид летит по экрану и уничтожает игрока при касании.
public class Asteroid : MonoBehaviour
{
    // Скорость астероида.
    public float speed = 3f;

    // Направление движения.
    private Vector2 direction;

    // Через сколько секунд астероид сам исчезнет,
    // чтобы не засорять сцену.
    public float lifeTime = 8f;

    void Start()
    {
        // Уничтожить астероид через несколько секунд.
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Если игра закончилась, астероиды не двигаются.
        if (GameManager.instance != null && GameManager.instance.IsGameOver())
        {
            return;
        }

        // Двигаем астероид.
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // Этот метод вызывает спавнер, чтобы задать направление полёта.
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Если астероид коснулся игрока — поражение.
        if (other.CompareTag("Player"))
        {
            GameManager.instance.LoseGame();
        }
    }
}
