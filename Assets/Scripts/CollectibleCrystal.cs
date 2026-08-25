using UnityEngine;

// Скрипт кристалла.
// Когда игрок касается кристалла, кристалл исчезает,
// а счётчик собранных кристаллов увеличивается.
public class CollectibleCrystal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что кристалл задел именно игрок.
        if (other.CompareTag("Player"))
        {
            // Добавляем один кристалл в счёт.
            GameManager.instance.AddCrystal();

            // Уничтожаем кристалл.
            Destroy(gameObject);
        }
    }
}
