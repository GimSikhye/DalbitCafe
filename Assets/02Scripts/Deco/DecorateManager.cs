using DalbitCafe.Customer;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
namespace DalbitCafe.Deco
{
    public class DecorateManager : MonoBehaviour
    {
        public static DecorateManager Instance;
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _customerParent;
        private GameObject[] _customers;
        [SerializeField] private GameObject _decorateUI; // ��ġ UI Ȱ��ȭ/��Ȱ��ȭ

        [SerializeField] private bool _isDecorateMode = false;
        [SerializeField] private GridManager _gridManager;


        private void Start()
        {
            // �ʿ�� �ʱ�ȭ �ڵ� �߰�
        }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }


        // ��ġ ��� Ȱ��ȭ
        public void ActivateDecorateMode()
        {
            if (_isDecorateMode) return;

            _isDecorateMode = true;
            _player.SetActive(false); // �÷��̾� ��Ȱ��ȭ
            _customers = new GameObject[_customerParent.childCount];

            for (int i = 0; i < _customers.Length; i++)
            {
                _customers[i] = _customerParent.GetChild(i).gameObject;
                _customers[i].SetActive(false); // �մԵ� ��Ȱ��ȭ
            }
            _decorateUI.SetActive(true); // ��ġ UI Ȱ��ȭ
        }

        // ��ġ ��� ��Ȱ��ȭ
        public void DeactivateDecorateMode()
        {
            if (!_isDecorateMode) return;

            _isDecorateMode = false;
            _player.SetActive(true); // �÷��̾� Ȱ��ȭ
            foreach (var customer in _customers)
            {
                customer.SetActive(true); // �մԵ� Ȱ��ȭ
            }
            _decorateUI.SetActive(false); // ��ġ UI ��Ȱ��ȭ
        }

        // ������ ��ġ ��� ���� üũ
        public bool CanPlaceItem(Vector2Int position, Vector2Int size)
        {
            return _gridManager.CanPlaceItem(position, size);
        }

        // ������ ��ġ
        public void PlaceItem(Vector2Int position, Vector2Int size)
        {
            _gridManager.PlaceItem(position, size); // ������ ��ġ ó��
        }
    }

}
