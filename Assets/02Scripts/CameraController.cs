using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dragSpeed = 0.1f; // �巡�� �ӵ� ����
    public Vector2 minLimit; // ī�޶� �̵� �ּ� ��ǥ(x,y)
    public Vector2 maxLimit; // ī�޶� �̵� �ִ� ��ǥ(x,y)

    private Vector3 dragOrigin; // �巡�� ������


    void Start()
    {
        
    }

    void Update()
    {
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

        }
    }
}
