using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonClicked : MonoBehaviour {

    private Image _thisObjBtn;
    public GameObject _target;
    public string _functionName = "Regame";

	// Use this for initialization
	void Start () {

        _thisObjBtn = gameObject.GetComponentInChildren<Image>();

    }
	
	// Update is called once per frame
    void Update()
    {
        if( Input.GetMouseButtonDown(0))
        {
            if(_thisObjBtn.Raycast(Input.mousePosition, Camera.main))
            {

                if (_target != null)
                {
                    _target.SendMessage(_functionName, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
