using System.Collections.Generic;
using UnityEngine;

public class CustomerPool : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab1;
    [SerializeField] private GameObject customerPrefab2;
    [SerializeField] private int poolSize = 5;  // �̸� ������ �մ� ��
    [SerializeField] private Transform spawnPoint; // �մ� ���� ��ġ
    [SerializeField] private Transform parent;
    private Queue<GameObject> customerPool = new Queue<GameObject>();
    
    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject customer = Instantiate(Random.value > 0.5f ? customerPrefab1 : customerPrefab2, spawnPoint.position, Quaternion.identity, parent);
            customer.SetActive(false);
            customerPool.Enqueue(customer);
        }
    }

    public GameObject SpawnCustomer()
    {
        if (customerPool.Count > 0)
        {
            GameObject customer = customerPool.Dequeue();
            customer.SetActive(true);
            customer.transform.position = spawnPoint.position;
            return customer;
        }
        return null;
    }

    private void OrderMenu()
    {
        //�丮�� �޴��� �ֹ�
        // ��ǳ�� ����.(�޴� Sprite)

    }

    public void ReturnCustomer(GameObject customer)
    {
        customer.SetActive(false);
        customerPool.Enqueue(customer);
    }
}
