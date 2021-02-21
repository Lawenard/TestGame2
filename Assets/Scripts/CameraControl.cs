using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed;

    [SerializeField] private Transform xPivot;
    [SerializeField] private Transform yPivot;

    void Update()
    {
        xPivot.Rotate(Vector3.up * (joystick.Direction.x * speed * Time.deltaTime));
        yPivot.Rotate(Vector3.left * (joystick.Direction.y * speed * Time.deltaTime));
    }
}
