using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGM : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LoadScene()
    {
        Debug.Log("test");
        //Application.LoadLevel("1_play");
        SceneManager.LoadScene("1_play");
    }
}
