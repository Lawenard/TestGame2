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
            stick.localPosition = joystick.InverseTransformPoint(data.position);
            stick.anchoredPosition = Vector2.ClampMagnitude(stick.anchoredPosition, joystick.sizeDelta.x / 2);
            Direction = stick.anchoredPosition.normalized;
        }
    }
}
