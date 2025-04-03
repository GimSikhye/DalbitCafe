using UnityEngine;
using UnityEngine.AI;  // NavMeshAgent�� ����ϱ� ���� �߰�
using DalbitCafe.Operations;
using System.Collections.Generic;
using UnityEngine.UI;

namespace DalbitCafe.Customer
{
    public class Customer : MonoBehaviour
    {
        [Header("�� ã��")]
        private NavMeshAgent _agent; // NavMeshAgent�� ��ü
        private Vector3 _targetDestination;  // ������ ���� ����
        private Transform _cashDesk; // �̵��� ��ǥ ����
        private Transform _outside;
        private CustomerPool _customerPool;

        [Header("�ֹ� ����")]
        [SerializeField] private GameObject _speechBalloon;
        [SerializeField] private SpriteRenderer _orderMenuSpriteRenderer;
        [SerializeField] private Sprite _angrySprite;

        private CoffeeData _randomCoffee; // �ֹ��� Ŀ�� ����
        private CoffeeMachine _orderedFromMachine; // �ֹ��� Ŀ�Ǹӽ� ����
        [SerializeField] private bool _isOrdering = false;


        void Start()
        {
            _agent = GetComponent<NavMeshAgent>(); // NavMeshAgent ������Ʈ ��������
            _customerPool = FindAnyObjectByType<CustomerPool>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            Intialize(); // ��ǥ ����

        }

        public void MoveTo(Transform destination)
        {
            _targetDestination = destination.position; // ������ ����
            _agent.SetDestination(_targetDestination);
        }

        private void Intialize()
        {
            _cashDesk = GameObject.Find("Cashdesk")?.transform; // "Destination" ���� ������Ʈ�� ã�� ��ǥ�� ����
            _outside = GameObject.Find("Outside")?.transform;

            MoveTo(_cashDesk);

        }

        private void Update()
        {
            if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance) // �������� �������� �� ����
            {
                if (_targetDestination == _cashDesk.position)
                {
                    Debug.Log("���뿡 �����߽��ϴ�");
                    StartOrdering();
                }
                else if (_targetDestination == _outside.position)
                {
                    Debug.Log("outside");
                    _speechBalloon.SetActive(false);
                    _customerPool.ReturnCustomer(this.gameObject);
                }
            }
        }
        // �ֹ��� �����ϴ� �Լ�
        void StartOrdering()
        {
            _isOrdering = true;
            _agent.isStopped = true; // �̵� ����
            Debug.Log("�ֹ��� �����մϴ�.");

            // ��� Ŀ�Ǹӽſ��� ������ Ŀ�� ����
            CoffeeMachine[] coffeeMachines = FindObjectsOfType<CoffeeMachine>();
            if (coffeeMachines.Length > 0)
            {
                List<CoffeeMachine> availableMachines = new List<CoffeeMachine>();
                foreach (var machine in coffeeMachines)
                {
                    if (machine.CurrentCoffee != null && machine.RemainingMugs > 0)
                    {
                        availableMachines.Add(machine);
                    }
                }

                if (availableMachines.Count > 0)
                {
                    _orderedFromMachine = availableMachines[Random.Range(0, availableMachines.Count)];
                    _randomCoffee = _orderedFromMachine.CurrentCoffee;
                    _speechBalloon.SetActive(true);
                    _orderMenuSpriteRenderer.sprite = _randomCoffee.MenuIcon;
                }
                else
                {
                    _speechBalloon.SetActive(true);
                    _orderMenuSpriteRenderer.sprite = _angrySprite;

                    LeaveStore();
                    Debug.Log("�ֹ� ������ Ŀ�ǰ� �����ϴ�!");
                }
            }
        }

        // �մ��� ��ġ�ϸ� �ֹ��� �Ϸ�ǰ� �̵��� �簳��
        private void OnMouseDown()
        {
            if (_isOrdering)
            {
                FinishOrder();
            }
        }

        void FinishOrder()
        {
            if (_randomCoffee != null)
            {
                if (_orderedFromMachine != null)
                {
                    _orderedFromMachine.SellCoffee(); // Ŀ�Ǹӽ��� �� �� ����
                    _isOrdering = false;
                    _speechBalloon.SetActive(false);
                    LeaveStore();

                }
            }


        }

        void LeaveStore()
        {
            _agent.isStopped = false; // �̵� �簳
            //_speechBalloon.SetActive(false);
            MoveTo(_outside);

        }
    }
}
