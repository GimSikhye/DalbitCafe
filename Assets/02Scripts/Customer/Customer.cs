using UnityEngine;
using Pathfinding; // A* Pathfinding ���ӽ����̽� �߰�

public class Customer : MonoBehaviour
{
    private Transform target; // �̵��� ��ǥ ����
    private AIPath aiPath; // A* ��� Ž���� ���� AIPath ������Ʈ

    void Start()
    {
        aiPath = GetComponent<AIPath>(); // AIPath ������Ʈ ��������
        if (aiPath == null)
        {
            Debug.LogError("AIPath ������Ʈ�� �����ϴ�!");
            return;
        }

        SetNextDestination(); // ù ��° ��ǥ ����
    }

    void SetNextDestination()
    {
        GameObject pathParent = GameObject.Find("PathPoints");
        if (pathParent != null)
        {
            Transform[] pathPoints = new Transform[pathParent.transform.childCount];
            for (int i = 0; i < pathPoints.Length; i++)
            {
                pathPoints[i] = pathParent.transform.GetChild(i);
            }

            if (pathPoints.Length > 0)
            {
                target = pathPoints[0]; // ù ��° ������ ��ǥ�� ����
                aiPath.destination = target.position; // A* �̵� ��ǥ ����
            }
        }
    }

    void Update()
    {
        // ��ǥ ������ �����ϸ� ���� �������� ����
        if (aiPath.reachedDestination && target != null)
        {
            target = target.GetChild(0); // ���� ��ǥ ����
            aiPath.destination = target.position;
        }
    }
}
