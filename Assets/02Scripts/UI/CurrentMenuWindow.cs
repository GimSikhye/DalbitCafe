using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentMenuWindow : MonoBehaviour
{
    public GameObject currentMenuPanel; 
    public TextMeshProUGUI menuNameText;
    public Image menuIcon;     
    public Slider mugProgressBar; 
    public TextMeshProUGUI remainingMugsText;  

    public void UpdateMenuPanel(CoffeeMachine coffeeMachine) // ���߿� ���ʸ�<T>�� �佺��, �ͼ��� ��� ������� //sell Coffee�Լ����� ȣ��, ó�� �г��� ��ﶧ�� ȣ��.
    {
        if (coffeeMachine != null && coffeeMachine.IsRoasting)
        {
            currentMenuPanel.SetActive(true);
            menuNameText.text = coffeeMachine.CurrentCoffee.CoffeName;
            menuIcon.sprite = coffeeMachine.CurrentCoffee.MenuIcon;
            remainingMugsText.text = $"{coffeeMachine.RemainingMugs}�� ����";
            mugProgressBar.value = (float)coffeeMachine.RemainingMugs / coffeeMachine.CurrentCoffee.MugQty;
        }
        else
        {
            currentMenuPanel.SetActive(false);
        }
    }
}
