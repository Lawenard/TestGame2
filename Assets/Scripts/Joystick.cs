using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : EventTrigger
{
    [SerializeField] private RectTransform stick;

    private RectTransform joystick;
    private PointerEventData data;

    public Vector2 Direction { get; private set; }

    private void Awake()
    {
        joystick = GetComponent<RectTransform>();
        Direction = Vector2.zero;
    }

    public override void OnPointerDown(PointerEventData data)
    {
        this.data = data;
    }

    public override void OnPointerUp(PointerEventData data)
    {
        this.data = null;
        stick.anchoredPosition = Vector2.zero;
        Direction = Vector2.zero;
    }

    private void Update()
    {
        if (data != null)
        {
            Vector2 newPosition;
            newPosition = joystick.InverseTransformPoint(data.position - joystick.sizeDelta / 4);
            newPosition = Vector2.ClampMagnitude(newPosition, joystick.sizeDelta.x / 2);
            stick.anchoredPosition = newPosition;
            Direction = newPosition.normalized;
        }
    }
}
