using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
using DalbitCafe.Operations;

namespace DalbitCafe.Customer
{

    public class Customer : MonoBehaviour
    {
        [Header("�� ã��")]
        private AIPath _aiPath; // A* ��� Ž���� ���� AIPath ������Ʈ
        private CustomerPool _custmorPool;
        private Transform _target; // �̵��� ��ǥ ����
        private GameObject _pathParent;
        private Transform[] _pathPoints;
        private int _currentIndex = 0;

        [Header("�ֹ� ����")]
        [SerializeField] private GameObject _speechBalloon;
        [SerializeField] private SpriteRenderer _orderMenuSprite;
        private CoffeeData _randomCoffee; // �ֹ��� Ŀ�� ����
        private CoffeeMachine _orderedFromMachine; // �ֹ��� Ŀ�Ǹӽ� ����
        private bool _isOrdering = false;

        void Start()
        {
            _aiPath = GetComponent<AIPath>();
            _custmorPool = FindAnyObjectByType<CustomerPool>();
            SetNextDestination(); // ù ��° ��ǥ ����
        }

        void SetNextDestination()
        {
            _pathParent = GameObject.Find("PathPoints");
            if (_pathParent != null)
            {
                _pathPoints = new Transform[_pathParent.transform.childCount];
                for (int i = 0; i < _pathPoints.Length; i++)
                {
                    _pathPoints[i] = _pathParent.transform.GetChild(i);
                }

                if (_pathPoints.Length > 0)
                {
                    _currentIndex = 0; // ���� �� �ε��� �ʱ�ȭ
                    _target = _pathPoints[0]; // ù ��° ������ ��ǥ�� ����
                    _aiPath.destination = _target.position; // A* �̵� ��ǥ ����
                }
            }
        }

        void Update()
        {
            if (_aiPath.reachedDestination && _target != null) //�������� �����Ͽ��ٸ�
            {
                if (_currentIndex == 1 && !_isOrdering)
                {
                    StartOrdering();
                    _currentIndex++;

                    return;
                }

                _currentIndex++;

                if (_currentIndex >= _pathPoints.Length)
                {
                    // ��θ� �� �������� ������Ʈ ��Ȱ��ȭ �� Pool�� ��ȯ
                    _custmorPool.ReturnCustomer(gameObject);
                    return;
                }

                _target = _pathPoints[_currentIndex];
                _aiPath.destination = _target.position;
            }
        }

        // �ֹ��� �����ϴ� �Լ�
        void StartOrdering()
        {
            _isOrdering = true;
            _aiPath.canMove = false; // �̵� ����

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
            _aiPath.canMove = true;

        }

        void LeaveStore()
        {
            _isOrdering = false;
            _speechBalloon.SetActive(false);
            _aiPath.canMove = true;

            SetNextDestination();
        }
    }


}

