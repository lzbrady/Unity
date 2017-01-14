using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerHUDHelper : MonoBehaviour {

    public Text GameText;
    public Image TextBox;

    List<string> textStore;
    bool clearingText;
    float ellapsedTime = 0f;

    void Start()
    {
        textStore = new List<string>();
        clearingText = false;
        textStore.Add("");
        //AddText("hello/My name is Eric/Nice to meet you");
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(GameText.text);
	    if (textStore.Count <= 1)
        {
            Debug.Log("disappear");
            TextBox.enabled = false;
        }else
        {
            RunText();
            TextBox.enabled = true;
        }
        ellapsedTime += Time.deltaTime;
	}

    void RunText()
    {
        if (Input.GetKey("t") && !GameText.text.Equals("") && ellapsedTime > 1)
        {
            ellapsedTime = 0;
            textStore.Remove(GameText.text);
        }
        GameText.text = (string)textStore[textStore.Count - 1];
    }

    void AddText(string text)
    {
        var temp = text.Split("/n"[0]);
        for (int i = temp.Length-1; i >= 0; i--)
        {
            textStore.Add(temp[i]);
        }
    }


}
