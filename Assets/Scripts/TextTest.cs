using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTest : MonoBehaviour
{
    public InputField FirstInputField;
    public InputField SecondInputField;
    public InputField ThirdInputField;
    public InputField FourthInputField;

    void Start()
    {        
        SecondInputField.onValueChange.AddListener(delegate { ValueChangeCheck1(); });
        ThirdInputField.onValueChange.AddListener(delegate { ValueChangeCheck2(); });
        FourthInputField.onValueChange.AddListener(delegate { ValueChangeCheck3(); });
    }

    void Update()
    {
        
    }

    void ValueChangeCheck1()
    {
        Debug.Log(SecondInputField.text);
        FirstInputField.text += SecondInputField.text;
    }

    void ValueChangeCheck2()
    {
        Debug.Log(SecondInputField.text);        
        FirstInputField.text += ThirdInputField.text;
    }

    void ValueChangeCheck3()
    {
        Debug.Log(SecondInputField.text);
        FirstInputField.text += FourthInputField.text;
    }
}
