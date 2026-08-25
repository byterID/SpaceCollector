using UnityEngine;

// Спавнер астероидов.
// Он создаёт астероиды за границами экрана,
// а потом отправляет их лететь через игровое поле.
public class AsteroidSpawner : MonoBehaviour
{
    [Header("Prefab")]
    // Префаб астероида.
    public GameObject asteroidPrefab;

    [Header("Spawn Settings")]
    // Как часто появляются астероиды.
    public float spawnInterval = 2.5f;

    // Сколько астероидов может быть на сцене одновременно.
    public int maxAsteroidsOnScene = 6;

    // Минимальная и максимальная скорость астероидов.
    public float minSpeed = 2f;
    public float maxSpeed = 5f;

    [Header("Spawn Borders")]
    // Границы, за которыми появляются астероиды.
    public float leftX = -9f;
    public float rightX = 9f;
    public float bottomY = -6f;
    public float topY = 6f;

    void Start()
    {
        InvokeRepeating("TrySpawnAsteroid", 1f, spawnInterval);
    }

    void TrySpawnAsteroid()
    {
        // Если игра закончилась — астероиды больше не появляются.
        if (GameManager.instance != null && GameManager.instance.IsGameOver())
        {
            return;
        }

        int asteroidsNow = GameObject.FindGameObjectsWithTag("Asteroid").Length;

        if (asteroidsNow < maxAsteroidsOnScene)
        {
            SpawnAsteroid();
        }
    }

    void SpawnAsteroid()
    {
        if (asteroidPrefab == null)
        {
            Debug.LogWarning("AsteroidSpawner: не назначен asteroidPrefab!");
            return;
        }

        // Выбираем случайную сторону экрана:
        // 0 — слева, 1 — справа, 2 — сверху, 3 — снизу.
        int side = Random.Range(0, 4);

        Vector3 spawnPosition = Vector3.zero;

        if (side == 0)
        {
            // Слева.
            spawnPosition = new Vector3(leftX, Random.Range(bottomY, topY), 0f);
        }
        else if (side == 1)
        {
            // Справа.
            spawnPosition = new Vector3(rightX, Random.Range(bottomY, topY), 0f);
        }
        else if (side == 2)
        {
            // Сверху.
            spawnPosition = new Vector3(Random.Range(leftX, rightX), topY, 0f);
        }
        else
        {
            // Снизу.
            spawnPosition = new Vector3(Random.Range(leftX, rightX), bottomY, 0f);
        }

        // Создаём астероид.
        GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        // Выбираем точку, примерно куда он полетит.
        // Не строго в центр, а чуть случайно, чтобы было интереснее.
        Vector2 targetPosition = new Vector2(Random.Range(-3f, 3f), Random.Range(-2f, 2f));

        // Направление = цель - текущая позиция.
        Vector2 direction = targetPosition - new Vector2(spawnPosition.x, spawnPosition.y);

        // Получаем скрипт Asteroid.
        Asteroid asteroidScript = newAsteroid.GetComponent<Asteroid>();

        if (asteroidScript != null)
        {
            asteroidScript.SetDirection(direction);
            asteroidScript.speed = Random.Range(minSpeed, maxSpeed);
        }
    }
}
