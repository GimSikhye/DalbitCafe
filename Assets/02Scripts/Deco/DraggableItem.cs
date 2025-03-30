using DalbitCafe.Deco;
using UnityEngine.EventSystems;
using UnityEngine;

namespace DalbitCafe.Deco
{
    public class DraggableItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Vector3 _initialPosition; // �ʱ� ��ġ
        private bool _isDragging = false;
        public Vector2Int _itemSize;  // ������ ũ�� (1x1, 2x1 ��)

        public void OnBeginDrag(PointerEventData eventData)
        {
            _initialPosition = transform.position;  // �巡�� ���� ��ġ ����
            _isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDragging)
            {
                Vector3 newPosition = Camera.main.ScreenToWorldPoint(eventData.position);
                newPosition.z = 0;  // Z���� ����

                // �׸��忡 ���� ������ �̵� (�ݿø�)
                transform.position = new Vector3(Mathf.Round(newPosition.x), Mathf.Round(newPosition.y), 0);

                // ��ġ �������� Ȯ�� 
                bool canPlace = DecorateManager.Instance.CanPlaceItem(new Vector2Int((int)transform.position.x, (int)transform.position.y), _itemSize);
                UpdateBorderColor(canPlace); 
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
            // ��ġ�� �� ������ ��ġ�ϰ�, �׸��� ������Ʈ
            if (DecorateManager.Instance.CanPlaceItem(new Vector2Int((int)transform.position.x, (int)transform.position.y), _itemSize))
            {
                DecorateManager.Instance.PlaceItem(new Vector2Int((int)transform.position.x, (int)transform.position.y), _itemSize);
                // ��ġ �Ϸ� �� UI �����
                //HideButtons();
            }
            else
            {
                // ���� ��ġ�� ���ư��� ó��
                transform.position = _initialPosition;
            }
        }

        private void UpdateBorderColor(bool canPlace)
        {
            // �ʷϻ�/������ �׵θ� ������Ʈ //��������Ʈ �׵θ��� �°� �������� �׷����� �ϴ¹�� ����? �׸��� �β��� �����Ҽ��ְ� �ϸ� ������
            if (canPlace)
            {
                // �ʷϻ�
                // itemBorder.color = Color.green;
            }
            else
            {
                // ������
                // itemBorder.color = Color.red;
            }
        }

        private void HideButtons()
        {
            // ��ġ �Ϸ� �� ��ư �����
        }
    }

}
