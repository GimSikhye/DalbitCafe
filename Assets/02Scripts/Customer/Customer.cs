using UnityEngine;
using System.Collections;

public class Customer : MonoBehaviour
{
    public enum CustomerState { Entering, Ordering, Exiting, Despawning }
    private CustomerState state;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Transform[] pathPoints; // �̵� ��� (�Ա� �� ���� �� �ⱸ)
    private int currentPointIndex = 0;

    private CustomerPool customerPool;

    private void Start()
    {
        customerPool = FindObjectOfType<CustomerPool>(); // �մ� Ǯ ã��
        StartCoroutine(CustomerRoutine());
    }

    private IEnumerator CustomerRoutine()
    {
        // 1. ���� �Ա��� �̵�
        state = CustomerState.Entering;
        yield return MoveToNextPoint();

        // 2. ���뿡�� �ֹ� (���� ��� �ð�)
        state = CustomerState.Ordering;
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        // 3. �ⱸ�� �̵�
        state = CustomerState.Exiting;
        yield return MoveToNextPoint();

        // 4. ȭ�� ������ ������ (�̵� �� Ǯ�� ��ȯ)
        state = CustomerState.Despawning;
        yield return MoveToNextPoint();
        customerPool.ReturnCustomer(gameObject);
    }

    private IEnumerator MoveToNextPoint()
    {
        Vector3 target = pathPoints[currentPointIndex].position;
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        currentPointIndex++;
    }
}
