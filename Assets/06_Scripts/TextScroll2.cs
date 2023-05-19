using UnityEngine;
using UnityEngine.SceneManagement;

public class TextScroll2 : MonoBehaviour
{
    public float speed = 110.0f;
    public float stopPosition = 500.0f;
    public GameObject hiddenObject;

    private bool isMoving = true;
    private bool isHiddenObjectShown = false;

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if (transform.position.y >= stopPosition)
            {
                isMoving = false;
            }
        }
        else if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!isHiddenObjectShown)
            {
                hiddenObject.SetActive(true);
                isHiddenObjectShown = true;
            }
            else
            {
                SceneManager.LoadScene("Title");
            }
        }
    }
}
