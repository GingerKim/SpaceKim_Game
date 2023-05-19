using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlinkingText : MonoBehaviour
{
    public float blinkTime = 0.7f; // ¡°∏Í ¡÷±‚
    private Text textComponent;

    private void Start()
    {
        textComponent = GetComponent<Text>();
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(blinkTime);
            textComponent.enabled = !textComponent.enabled;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Prologue");
        }
    }
}