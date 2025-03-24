using UnityEngine;
using Pathfinding; // A* Pathfinding ���ӽ����̽� �߰�

public class Customer : MonoBehaviour
{
    private Transform target; // �̵��� ��ǥ ����
    private AIPath aiPath; // A* ��� Ž���� ���� AIPath ������Ʈ
    GameObject pathParent;
    Transform[] pathPoints;
    private int currentIndex = 0;
    private CustomerPool custerPool;

    void Start()
    {
        aiPath = GetComponent<AIPath>(); // AIPath ������Ʈ ��������
        if (aiPath == null)
        {
            Debug.LogError("AIPath ������Ʈ�� �����ϴ�!");
            return;
        }
        
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
}
