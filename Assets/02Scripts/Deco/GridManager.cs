using UnityEngine;

// ��ġ ���� ���� üũ
namespace DalbitCafe.Deco
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private int _gridWidth = 10;  // �� ���� ũ��
        [SerializeField] private int _gridHeight = 10; // ���� ���� ũ��(��ġ�� �� �ִ� ����)
        [SerializeField] private float _tileSize = 0.5f; // Ÿ�� ũ�� (�׸��� ���� ũ��)

        private bool[,] _grid;  // �׸��� ���¸� ��Ÿ���� 2D �迭 (��ġ ����) / Flase�̸� ��ġ�� �� �ִ� ����

        private void Start()
        {
            // �׸��� �ʱ�ȭ
            _grid = new bool[_gridWidth, _gridHeight];
        }

        // �׸��忡�� Ư�� ��ġ�� �������� ��ġ�� �� �ִ��� üũ
        public bool CanPlaceItem(Vector2Int position, Vector2Int size)
        {
            // �׸��� ������ ������ �ʵ��� üũ
            // ���� �ٱ��̰ų�, �Ʒ��� �Ѿ�ų�, ������ġ+������(���������ϴ�ũ��)�� �������� �Ѿ�ų�, ������ġ+����� ���������̸� �Ѿ�ٸ�
            if (position.x < 0 || position.y < 0 || position.x + size.x > _gridWidth || position.y + size.y > _gridHeight)
            {
                return false; // ��ġ �Ұ�
            }

            // �������� �����ϴ� ������ �ٸ� �������� �ִ��� Ȯ��
            for (int x = position.x; x < position.x + size.x; x++)
            {
                for (int y = position.y; y < position.y + size.y; y++)
                {
                    if (_grid[x, y])
                    {
                        return false;  // �̹� ��ġ�� ���̶�� �Ұ���
                    }
                }
            }

            return true;  // ��ġ ����
        }

        // �׸��忡 ������ ��ġ
        public void PlaceItem(Vector2Int position, Vector2Int size)
        {
            // �������� �����ϴ� ������ ������ ��ġ
            for (int x = position.x; x < position.x + size.x; x++)
            {
                for (int y = position.y; y < position.y + size.y; y++)
                {
                    _grid[x, y] = true;  // �ش� ��ġ�� ������ ��ġ�� // ��... �׸��忡 ��ġ�Ǿ������� üũ�ϰ�, �� ��ġ�� ���ӿ�����Ʈ(��������Ʈ�� ��ġ���ϳ�)
                }
            }
        }

        // �׸��忡�� �������� ���� (��ġ ���)
        public void RemoveItem(Vector2Int position, Vector2Int size)
        {
            // �������� �����ϴ� �������� �������� ����
            for (int x = position.x; x < position.x + size.x; x++)
            {
                for (int y = position.y; y < position.y + size.y; y++)
                {
                    _grid[x, y] = false;  // �ش� ��ġ���� ������ ����
                    // ���⵵ �� ��ġ�� �ִ� ���ھ����� ���� ���ϳ�
                }
            }
        }

        // �׸��� ���¸� ������ϱ� ���� �Լ� (���ϴ� ���)
        private void OnDrawGizmos()
        {
            if (_grid == null) return;

            // �׸��带 �׷����� ���� Gizmos
            Gizmos.color = Color.green;
            for (int x = 0; x < _gridWidth; x++)
            {
                for (int y = 0; y < _gridHeight; y++)
                {
                    if (_grid[x, y]) // �ش� �׸��忡 �������� ��ġ�Ǿ��ִٸ� (true���)
                    {
                        Gizmos.DrawCube(new Vector3(x * _tileSize, y * _tileSize, 0), new Vector3(_tileSize, _tileSize, 0.1f));
                    }
                }
            }
        }
    }
}

