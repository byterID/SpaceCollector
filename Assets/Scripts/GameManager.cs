using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Главный управляющий скрипт игры.
// Он хранит счёт, проверяет победу и поражение.
public class GameManager : MonoBehaviour
{
    // Singleton — удобная ссылка, чтобы другие скрипты могли обратиться к GameManager.
    public static GameManager instance;

    [Header("Game Rules")]
    // Сколько кристаллов нужно собрать для победы.
    public int crystalsToWin = 10;

    // Сколько кристаллов уже собрано.
    private int collectedCrystals = 0;

    // Закончилась ли игра.
    private bool gameOver = false;

    [Header("UI")]
    // Текст счётчика кристаллов.
    public Text crystalsText;

    // Окно победы.
    public GameObject winPanel;

    // Окно поражения.
    public GameObject losePanel;

    void Awake()
    {
        // Проверяем, что GameManager в сцене один.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // В начале игры прячем окна.
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }

        UpdateCrystalsText();
    }

    void Update()
    {
        // Если игра закончилась, можно нажать R для перезапуска.
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    // Этот метод вызывается, когда игрок собирает кристалл.
    public void AddCrystal()
    {
        // Если игра уже закончилась, ничего не считаем.
        if (gameOver)
        {
            return;
        }

        collectedCrystals++;

        UpdateCrystalsText();

        // Проверяем победу.
        if (collectedCrystals >= crystalsToWin)
        {
            WinGame();
        }
    }

    // Победа.
    public void WinGame()
    {
        if (gameOver)
        {
            return;
        }

        gameOver = true;

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

    // Поражение.
    public void LoseGame()
    {
        if (gameOver)
        {
            return;
        }

        gameOver = true;

        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
    }

    // Обновляем текст счётчика.
    private void UpdateCrystalsText()
    {
        if (crystalsText != null)
        {
            crystalsText.text = "Кристаллы: " + collectedCrystals + " / " + crystalsToWin;
        }
    }

    // Перезапуск сцены.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Другие скрипты могут спросить: закончилась ли игра?
    public bool IsGameOver()
    {
        return gameOver;
    }
}
