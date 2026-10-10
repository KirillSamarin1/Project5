using UnityEngine;
using TMPro; // Пространство имен для работы с TextMeshPro
using System.Globalization;

public class Calculator : MonoBehaviour
{
    [Header("UI Elements (TextMeshPro)")]
    [SerializeField] private TMP_InputField inputField1;
    [SerializeField] private TMP_InputField inputField2;
    [SerializeField] private TMP_Text resultText;

    // Метод для получения чисел из TMP_InputField
    private bool TryGetInputs(out float num1, out float num2)
    {
        num1 = 0;
        num2 = 0;

        // Проверка на null и пустые поля
        if (inputField1 == null || inputField2 == null || string.IsNullOrWhiteSpace(inputField1.text) || string.IsNullOrWhiteSpace(inputField2.text))
        {
            if (resultText != null) resultText.text = "Ошибка: Введите числа";
            return false;
        }

        // Парсинг строк в числа float
        bool parse1 = float.TryParse(inputField1.text, NumberStyles.Any, CultureInfo.InvariantCulture, out num1);
        bool parse2 = float.TryParse(inputField2.text, NumberStyles.Any, CultureInfo.InvariantCulture, out num2);

        if (!parse1 || !parse2)
        {
            if (resultText != null) resultText.text = "Ошибка: Неверный формат";
            return false;
        }

        return true;
    }

    // Метод для кнопки "+"
    public void OnAddClick()
    {
        if (!TryGetInputs(out float n1, out float n2)) return;
        resultText.text = $"= {n1 + n2}";
    }

    // Метод для кнопки "-"
    public void OnSubtractClick()
    {
        if (!TryGetInputs(out float n1, out float n2)) return;
        resultText.text = $"= {n1 - n2}";
    }

    // Метод для кнопки "*"
    public void OnMultiplyClick()
    {
        if (!TryGetInputs(out float n1, out float n2)) return;
        resultText.text = $"= {n1 * n2}";
    }

    // Метод для кнопки "/"
    public void OnDivideClick()
    {
        if (!TryGetInputs(out float n1, out float n2)) return;

        // Проверка деления на ноль
        if (Mathf.Approximately(n2, 0f))
        {
            resultText.text = "на ноль делить нельзя!";
            return;
        }

        resultText.text = $"= {n1 / n2}";
    }
}
