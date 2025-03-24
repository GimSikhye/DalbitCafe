using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    public static CoffeeMachine LastTouchedMachine { get; private set; } // ������ ��ġ�� Ŀ�Ǹӽ� ����

    [Header("Ŀ�Ǹӽ� ���� ����")]
    [SerializeField] private CoffeeData currentCoffee; // ���� �ν��� ���� Ŀ��
    [SerializeField] private int remainingMugs; // ���� Ŀ�� �� ��
    [SerializeField] private bool isRoasting = false;

    public void RoastCoffee(CoffeeData coffee)
    {
        isRoasting = true;
        currentCoffee = coffee;
        remainingMugs = coffee.MugQty;
        Debug.Log($"{coffee.CoffeName} Ŀ�Ǹ� �ν��� ����! �� ��: {remainingMugs}");

    }

    // ���� �ν��� ���� ��, isRosating = true;
    // �˾�â ����� ����, ���� �ܼ� ���� �˾� ����.
    public void SellCoffee() // �մ����� �ȸ�.
    {
        // ���� ���� �ܼ� ǥ��
        if (remainingMugs > 0)
        {//currentCoffee.sprite (��ƼŬ�� ���ư���?) (instantiate)
            remainingMugs--;
            GameManager.Instance.Coin += currentCoffee.Price;
            Debug.Log($"{currentCoffee.CoffeName} �Ǹ�! ���� �� ��: {remainingMugs}");
        }
        else
        {
            isRoasting = false;
            Debug.Log("�� �̻� �Ǹ��� Ŀ�ǰ� �����ϴ�!");
        }
    }

    public static void SetLastTouchedMachine(CoffeeMachine machine)
    {
        LastTouchedMachine = machine;
        Debug.Log(LastTouchedMachine.gameObject.name);
    }


}
