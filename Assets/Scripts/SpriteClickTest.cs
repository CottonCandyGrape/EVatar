using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteClickTest : MonoBehaviour
{
    public GameObject targetSprite;
    public InputField inputField;

    void Awake()
    {
        if (targetSprite == null)
            targetSprite = GetComponent<GameObject>();
    }

    void Update()
    {
        if (true == Input.GetMouseButtonUp(0))
        {
            targetSprite = GetClickedSprite();
            inputField.text += targetSprite.name;
        }
    }

    private GameObject GetClickedSprite()
    {
        RaycastHit hit;
        GameObject target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))
        {
            target = hit.collider.gameObject;
        }

        return target;
    }
}
