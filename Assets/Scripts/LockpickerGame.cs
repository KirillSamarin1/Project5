using UnityEngine;
using TMPro;

public class LockpickerGame : MonoBehaviour
{
    [Header("UI: Индикаторы (TextMeshPro)")]
    [SerializeField] private TMP_Text timerText;             // Сюда перетащите текст таймера
    [SerializeField] private TMP_Text pin1Text;              // Сюда перетащите текст 1-го пина
    [SerializeField] private TMP_Text pin2Text;              // Сюда перетащите текст 2-го пина
    [SerializeField] private TMP_Text pin3Text;              // Сюда перетащите текст 3-го пина
    [SerializeField] private TMP_Text statusText;            // Сюда выводится сообщение об исходе игры

    [Header("UI: Панель кнопок и Перезапуск")]
    [SerializeField] private GameObject instrumentsPanel;    // Панель (родительский объект) с 3 кнопками инструментов
    [SerializeField] private GameObject restartButtonObject; // Объект кнопки "Заново" для скрытия/показа

    [Header("Настройки времени и баланса")]
    [SerializeField] private float gameDuration = 60f;       // Время на игру в секундах
    [SerializeField] private int targetPinValue = 5;          // Какое значение пинов нужно для победы (5, 5, 5)

    [Header("Стартовые значения пинов")]
    [SerializeField] private int startPin1 = 7;
    [SerializeField] private int startPin2 = 3;
    [SerializeField] private int startPin3 = 5;

    private float currentTimer;
    private int currentPin1;
    private int currentPin2;
    private int currentPin3;
    private bool isGameActive = false;

    void Start()
    {
        RestartGame();
    }

    void Update()
    {
        // Если игра завершена, таймер застывает
        if (!isGameActive) return;

        // Отсчёт времени в обратную сторону с помощью Time.deltaTime
        currentTimer -= Time.deltaTime;

        // Если время полностью вышло
        if (currentTimer <= 0)
        {
            currentTimer = 0;
            GameOver(false); // Игрок проиграл
        }

        UpdateUI();
    }

    // 1. Кнопка ДРЕЛЬ (+1 | -1 | 0)
    public void OnDrillClick()
    {
        if (!isGameActive) return;
        ApplyPinChanges(1, -1, 0);
    }

    // 2. Кнопка МОЛОТОК (-1 | 2 | -1)
    public void OnHammerClick()
    {
        if (!isGameActive) return;
        ApplyPinChanges(-1, 2, -1);
    }

    // 3. Кнопка ОТМЫЧКА (-1 | +1 | +1)
    public void OnPicklockClick()
    {
        if (!isGameActive) return;
        ApplyPinChanges(-1, 1, 1);
    }

    // Изменение значений пинов с ограничением диапазона от 0 до 10 (Mathf.Clamp)
    private void ApplyPinChanges(int change1, int change2, int change3)
    {
        currentPin1 = Mathf.Clamp(currentPin1 + change1, 0, 10);
        currentPin2 = Mathf.Clamp(currentPin2 + change2, 0, 10);
        currentPin3 = Mathf.Clamp(currentPin3 + change3, 0, 10);

        UpdateUI();
        CheckWinCondition();
    }

    // Проверка условий победы
    private void CheckWinCondition()
    {
        if (currentPin1 == targetPinValue && currentPin2 == targetPinValue && currentPin3 == targetPinValue)
        {
            GameOver(true); // Игрок победил
        }
    }

    // Логика завершения игры (Победа или Поражение)
    private void GameOver(bool isWin)
    {
        isGameActive = false;

        // Показываем кнопку "Заново" для новой попытки
        if (restartButtonObject != null) restartButtonObject.SetActive(true);

        // Скрываем панель с инструментами взлома
        if (instrumentsPanel != null) instrumentsPanel.SetActive(false);

        // Проверяем привязку поля статуса во избежание ошибок в консоли
        if (statusText != null)
        {
            if (isWin)
            {
                statusText.color = Color.green; // Зелёный цвет для победы
                statusText.text = "Вы выиграли";
            }
            else
            {
                statusText.color = Color.red;   // Красный цвет для поражения
                statusText.text = "Вы проиграли";
            }
        }
        else
        {
            Debug.LogError("Ошибка: Вы забыли перетащить объект Status Text в инспектор GameManager!");
        }
    }

    // Метод для обновления всех текстовых полей
    private void UpdateUI()
    {
        if (timerText != null)
        {
            // Округляем в большую сторону, чтобы секунды плавно уменьшались (60..59..58)
            timerText.text = Mathf.CeilToInt(currentTimer).ToString();
        }

        pin1Text.text = currentPin1.ToString();
        pin2Text.text = currentPin2.ToString();
        pin3Text.text = currentPin3.ToString();
    }

    // Сброс и перезапуск игры (вызывается на старте и при нажатии на "Заново")
    public void RestartGame()
    {
        currentTimer = gameDuration;
        currentPin1 = startPin1;
        currentPin2 = startPin2;
        currentPin3 = startPin3;

        if (statusText != null)
        {
            statusText.text = ""; // Очищаем строку результата перед новой игрой
        }
        
        // Прячем кнопку "Заново", пока идёт игра
        if (restartButtonObject != null) restartButtonObject.SetActive(false);
        
        // Возвращаем на место кнопки инструментов взлома
        if (instrumentsPanel != null) instrumentsPanel.SetActive(true);

        isGameActive = true;
        UpdateUI();
    }
}
