using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dragSpeed = 0.1f; // �巡�� �ӵ� ����
    public Vector2 minBounds; // �ּ� ��ǥ(���� �Ʒ�)
    public Vector2 maxBounds; // �ִ� ��ǥ(������ ��)

    private Vector3 lastTouchPosition;

    void Update()
    {
        HandleCameraDrag();
    }

    private void HandleCameraDrag()
    {
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Vector3 direction = currentPosition - lastTouchPosition; // �̵����� ���
                MoveCamera(-direction);
                lastTouchPosition = currentPosition;
            }
        }
    }

    private void MoveCamera(Vector3 moveDirection)
    {
        Vector3 newPosition = transform.position + moveDirection * dragSpeed; // ���ο� ��ġ ���

        // ī�޶� ��ġ�� ���ѵ� ���� ������ ����
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

        transform.position = newPosition;
    }
}
