using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteClickTest : MonoBehaviour
{
    public GameObject targetSprite;
    public InputField inputField;

    private AudioSource audioSource;

    void Awake()
    {
        if (targetSprite == null)
            targetSprite = GetComponent<GameObject>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //inputField.ActivateInputField();
        if (true == Input.GetMouseButtonUp(0))
        {
            targetSprite = GetClickedSprite();
            //inputField.ActivateInputField();
            inputField.text += targetSprite.name;
            inputField.Select();
            audioSource.Play();
        }
        inputField.ActivateInputField();
        //inputField.MoveTextStart(true);
        //inputField.Select();
    }

    private GameObject GetClickedSprite()
    {
        RaycastHit hit;
        GameObject target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))
            target = hit.collider.gameObject;
        
        return target;
    }
}
