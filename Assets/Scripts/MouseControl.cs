using UnityEngine;

public class MouseControl : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;

    private void Update()
    {
        Vector2 position = Input.mousePosition;
        Vector2 offset = Vector2.down * 32f;

        position += offset;

        rectTransform.anchoredPosition = position;
    }
}
