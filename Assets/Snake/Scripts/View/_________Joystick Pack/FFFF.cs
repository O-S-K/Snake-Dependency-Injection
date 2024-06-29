using UnityEngine;
using UnityEngine.EventSystems;

public class FFFF : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool isPressed = false;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}