using UnityEngine;
using UnityEngine.AI;  // NavMeshAgent�� ����ϱ� ���� �߰�
using DalbitCafe.Operations;
using System.Collections.Generic;

namespace DalbitCafe.Customer
{
    public class Customer : MonoBehaviour
    {
        [Header("�� ã��")]
        private NavMeshAgent _navMeshAgent; // NavMeshAgent�� ��ü
        private CustomerPool _custmorPool;
        private Transform _target; // �̵��� ��ǥ ����

        [Header("�ֹ� ����")]
        [SerializeField] private GameObject _speechBalloon;
        [SerializeField] private SpriteRenderer _orderMenuSprite;
        private CoffeeData _randomCoffee; // �ֹ��� Ŀ�� ����
        private CoffeeMachine _orderedFromMachine; // �ֹ��� Ŀ�Ǹӽ� ����
        private bool _isOrdering = false;

        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>(); // NavMeshAgent ������Ʈ ��������
            _custmorPool = FindAnyObjectByType<CustomerPool>();
            SetDestination(); // ��ǥ ����
        }

        void SetDestination()
        {
            _target = GameObject.Find("Cashdesk")?.transform; // "Destination" ���� ������Ʈ�� ã�� ��ǥ�� ����

            if (_target != null)
            {
                _navMeshAgent.SetDestination(_target.position); // NavMeshAgent�� �̵� ��ǥ ����
            }
        }

        void Update()
        {
            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (_target != null) //�������� �����Ͽ��ٸ�
                {
                    if (!_isOrdering)
                    {
                        StartOrdering();
                    }
                }
            }
        }

        // �ֹ��� �����ϴ� �Լ�
        void StartOrdering()
        {
            _isOrdering = true;
            _navMeshAgent.isStopped = true; // �̵� ����

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
                    _orderMenuSprite.sprite = _randomCoffee.MenuIcon;
                }
                else
                {
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
                    // currentMenu ������Ʈ�ϱ�
                }
            }

            _isOrdering = false;
            _speechBalloon.SetActive(false);
            _navMeshAgent.isStopped = false; // �̵� �簳
        }

        void LeaveStore()
        {
            _isOrdering = false;
            _speechBalloon.SetActive(false);
            _navMeshAgent.isStopped = false; // �̵� �簳
        }
    }
}
