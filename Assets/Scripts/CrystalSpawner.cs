using UnityEngine;

// Спавнер кристаллов.
// Он создаёт кристаллы в случайных местах на игровом поле.
public class CrystalSpawner : MonoBehaviour
{
    [Header("Prefab")]
    // Префаб кристалла, который будет появляться.
    public GameObject crystalPrefab;

    [Header("Spawn Settings")]
    // Сколько кристаллов может быть на поле одновременно.
    public int maxCrystalsOnScene = 5;

    // Сколько кристаллов создать в самом начале.
    public int crystalsAtStart = 3;

    // Как часто создавать новые кристаллы.
    public float spawnInterval = 2f;

    [Header("Spawn Area")]
    // Границы области появления кристаллов.
    public float minX = -7f;
    public float maxX = 7f;
    public float minY = -4f;
    public float maxY = 4f;

    void Start()
    {
        // Создаём несколько кристаллов в начале игры.
        for (int i = 0; i < crystalsAtStart; i++)
        {
            SpawnCrystal();
        }

        // Запускаем повторяющийся спавн.
        InvokeRepeating("TrySpawnCrystal", spawnInterval, spawnInterval);
    }

    void TrySpawnCrystal()
    {
        // Если игра закончилась — больше ничего не создаём.
        if (GameManager.instance != null && GameManager.instance.IsGameOver())
        {
            return;
        }

        // Считаем, сколько кристаллов уже есть на сцене.
        int crystalsNow = GameObject.FindGameObjectsWithTag("Crystal").Length;

        // Если кристаллов меньше максимума — создаём новый.
        if (crystalsNow < maxCrystalsOnScene)
        {
            SpawnCrystal();
        }
    }

    void SpawnCrystal()
    {
        if (crystalPrefab == null)
        {
            Debug.LogWarning("CrystalSpawner: не назначен crystalPrefab!");
            return;
        }

        // Случайная позиция внутри заданных границ.
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        Instantiate(crystalPrefab, spawnPosition, Quaternion.identity);
    }
}
