using UnityEngine;
using TMPro;
using System.Globalization;

public class TwoNumbersComparer : MonoBehaviour
{
    [Header("UI Elements (TextMeshPro)")]
    [SerializeField] private TMP_InputField inputField1;
    [SerializeField] private TMP_InputField inputField2;
    [SerializeField] private TMP_Text resultText;

    public void OnCompareClick()
    {
        if (inputField1 == null || inputField2 == null || string.IsNullOrWhiteSpace(inputField1.text) || string.IsNullOrWhiteSpace(inputField2.text))
        {
            if (resultText != null) resultText.text = "Ошибка: Введите числа";
            return;
        }

        bool parse1 = float.TryParse(inputField1.text, NumberStyles.Any, CultureInfo.InvariantCulture, out float num1);
        bool parse2 = float.TryParse(inputField2.text, NumberStyles.Any, CultureInfo.InvariantCulture, out float num2);

        if (!parse1 || !parse2)
        {
            resultText.text = "Ошибка: Неверный формат";
            return;
        }

        if (Mathf.Approximately(num1, num2))
        {
            resultText.text = "Результат: Равны";
        }
        else if (num1 > num2)
        {
            resultText.text = $"Наибольшее: {num1}";
        }
        else
        {
            resultText.text = $"Наибольшее: {num2}";
        }
    }
}
