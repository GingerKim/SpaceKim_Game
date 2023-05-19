using UnityEngine;
using UnityEngine.SceneManagement;

public class TextScroll1 : MonoBehaviour
{
    public float speed = 110.0f;
    public float stopPosition = 500.0f;

    private bool isMoving = true;

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
            SceneManager.LoadScene("Main");
        }
    }
}