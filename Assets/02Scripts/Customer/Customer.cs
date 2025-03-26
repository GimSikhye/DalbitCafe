using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
using NUnit.Framework; // A* Pathfinding ���ӽ����̽� �߰�

public class Customer : MonoBehaviour
{
    private AIPath aiPath; // A* ��� Ž���� ���� AIPath ������Ʈ
    private CustomerPool custerPool;
    private Transform target; // �̵��� ��ǥ ����
    GameObject pathParent;
    Transform[] pathPoints;
    private int currentIndex = 0;

    [Header("�ֹ� ����")]
    [SerializeField] private GameObject speechBalloon;
    [SerializeField] private SpriteRenderer orderMenuSprite;
    private bool isOrdering = false; 

    void Start()
    {
        aiPath = GetComponent<AIPath>(); // AIPath ������Ʈ ��������
        custerPool = FindAnyObjectByType<CustomerPool>();
        SetNextDestination(); // ù ��° ��ǥ ����
    }

    void SetNextDestination()
    {
        pathParent = GameObject.Find("PathPoints");
        if (pathParent != null)
        {
            pathPoints = new Transform[pathParent.transform.childCount];
            for (int i = 0; i < pathPoints.Length; i++)
            {
                pathPoints[i] = pathParent.transform.GetChild(i);
            }

            if (pathPoints.Length > 0)
            {
                currentIndex = 0; // ���� �� �ε��� �ʱ�ȭ
                target = pathPoints[0]; // ù ��° ������ ��ǥ�� ����
                aiPath.destination = target.position; // A* �̵� ��ǥ ����
            }
        }
    }

    void Update() 
    {
        if (aiPath.reachedDestination && target != null)
        {
            if(currentIndex == 1 && !isOrdering)
            {
                StartOrdering();
                return;
            }

            currentIndex++;

            if (currentIndex >= pathPoints.Length)
            {
                // ��θ� �� �������� ������Ʈ ��Ȱ��ȭ �� Pool�� ��ȯ
                custerPool.ReturnCustomer(gameObject);
                return;
            }

            target = pathPoints[currentIndex];
            aiPath.destination = target.position;
        }
    }

    // �ֹ��� �����ϴ� �Լ�
    void StartOrdering()
    {
        isOrdering = true;
        aiPath.canMove = false; // �̵� ����
        speechBalloon.SetActive(true);

        // ��� Ŀ�Ǹӽſ��� ������ Ŀ�� ����
        CoffeeMachine[] coffeeMachines = FindObjectsOfType<CoffeeMachine>();
        if(coffeeMachines.Length > 0 )
        {
            List<CoffeeData> availableCoffees = new List<CoffeeData>();
            foreach(var machine in coffeeMachines)
            {
                if(machine.CurrentCoffee != null)
                {
                    availableCoffees.Add(machine.CurrentCoffee);
                }
            }

            if(availableCoffees.Count > 0)
            {
                CoffeeData randomCoffee = availableCoffees[Random.Range(0, availableCoffees.Count)];
                orderMenuSprite.sprite = randomCoffee.MenuIcon;
            }
            else
            {
                Debug.Log("�ֹ� ������ Ŀ�ǰ� �����ϴ�!");
            }
        }

    }

    // �մ��� ��ġ�ϸ� �ֹ��� �Ϸ�ǰ� �̵��� �簳��
    private void OnMouseDown()
    {
        if(isOrdering)
        {
            FinishOrder();
        }
    }

    void FinishOrder()
    {
        isOrdering = false;
        speechBalloon.SetActive(false);
        aiPath.canMove = true;
        SetNextDestination();
    }
}
