using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f); // Scale on hover
    public Vector3 normalScale = new Vector3(1f, 1f, 1f);      // Normal scale

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = normalScale; // Ensure the button starts at normal scale
    }

    // Triggered when the mouse enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.localScale = hoverScale;
    }

    // Triggered when the mouse exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.localScale = normalScale;
    }
}
