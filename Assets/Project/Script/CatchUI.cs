using UnityEngine;
using TMPro;
using System.Collections;

public class CatchUI : MonoBehaviour
{
    public TextMeshProUGUI text;

    public Color successColor = Color.green;
    public Color failColor = Color.red;

    private Coroutine clearRoutine;

    void Start()
    {
        Debug.Log("CATCH UI STARTED");

        if (text != null)
        {
            text.text = "UI TEST OK";
            text.color = Color.white;

            StartCoroutine(ClearTestText());
        }
        else
        {
            Debug.LogError("TEXT NOT ASSIGNED!");
        }
    }

    IEnumerator ClearTestText()
    {
        yield return new WaitForSeconds(2f);

        if (text != null)
            text.text = "";
    }

    public void ShowSuccess()
    {
        Debug.Log("SUCCESS TRIGGERED");

        ShowMessage(
            "GOTCHA!",
            successColor
        );
    }

    public void ShowFail()
    {
        Debug.Log("FAIL TRIGGERED");

        ShowMessage(
            "It escaped!",
            failColor
        );
    }

    void ShowMessage(string msg, Color color)
    {
        if (text == null)
        {
            Debug.LogError("TEXT NULL");
            return;
        }

        text.text = msg;
        text.color = color;

        if (clearRoutine != null)
            StopCoroutine(clearRoutine);

        clearRoutine =
            StartCoroutine(ClearAfterDelay());
    }

    IEnumerator ClearAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);

        if (text != null)
            text.text = "";
    }
}