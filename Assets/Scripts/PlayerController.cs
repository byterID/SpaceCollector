using UnityEngine;

// Этот скрипт отвечает за движение игрока.
// Его можно повесить на объект Player.
public class PlayerController : MonoBehaviour
{
    // Скорость игрока.
    // Это public-переменная, поэтому её можно менять прямо в редакторе.
    public float speed = 5f;

    // Сюда мы сохраним физику игрока.
    private Rigidbody2D rb;

    // Здесь будет храниться направление движения.
    private Vector2 movement;

    void Start()
    {
        // Получаем компонент Rigidbody2D с объекта Player.
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Получаем нажатия клавиш.
        // Horizontal — это A/D или стрелки влево/вправо.
        // Vertical — это W/S или стрелки вверх/вниз.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Чтобы по диагонали игрок не двигался быстрее.
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // Двигаем игрока.
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
