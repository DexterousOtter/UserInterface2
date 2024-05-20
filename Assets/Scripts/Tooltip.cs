using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public GameObject tooltip;

    public void toggleActive(bool active)
    {
        tooltip.SetActive(active);
    }
    public void setText(string name, string description)
    {
        nameText.text = name;
        descriptionText.text = description;
    }
    public static Tooltip Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void setTransformFromMousePosition()
    {
        Camera mainCamera = Camera.main;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;
        mousePosition.x -= 75;
        mousePosition.y += 75;
        transform.position = mainCamera.ScreenToWorldPoint(mousePosition);
    }
}
