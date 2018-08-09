using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EyeHelpers;

public class SpriteClickTest : MonoBehaviour
{
    public GameObject targetSprite;
    public InputField inputField;
    public EVatarInputField eInputField;

    private AudioSource audioSource;

    void Awake()
    {
        if (targetSprite == null)
            targetSprite = gameObject;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (true == Input.GetMouseButtonUp(0))
        {
            if (targetSprite) targetSprite = GetClickedSprite();
            //inputField.text += targetSprite.name;
            eInputField.AppendString(targetSprite.name);
            audioSource.Play();
        }
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
