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


    private void Update()
    {
        HideMenuOnOustideTouch();
    }

    void HideMenuOnOustideTouch()
    {
        // ��ġ�� 1�� �̻� ���� ���� ó��
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // ù ��° ��ġ�� ó��

            // ��ġ ���°� Began�� ���� ����
            if (touch.phase == TouchPhase.Began)
            {
                // UI �ܺθ� ��ġ���� ��
                if (!UIManager.Instance.IsTouchOverUI(touch))
                {
                    if (currentMenuPanel != null)
                    {
                        Debug.Log("â ��");
                        currentMenuPanel.SetActive(false);
                    }
                    else
                    {
                        Debug.LogError("currentMenuPanel�� null�Դϴ�!");
                    }
                }
            }
        }
    }


    public void UpdateMenuPanel(CoffeeMachine coffeeMachine) // ���߿� ���ʸ�<T>�� �佺��, �ͼ��� ��� ������� //sell Coffee�Լ����� ȣ��, ó�� �г��� ��ﶧ�� ȣ��.
    {
        
        if (coffeeMachine != null && coffeeMachine.IsRoasting)
        {
            currentMenuPanel.SetActive(true);
            menuNameText.text = coffeeMachine.CurrentCoffee.CoffeName;
            menuIcon.sprite = coffeeMachine.CurrentCoffee.MenuIcon;
            remainingMugsText.text = $"{coffeeMachine.RemainingMugs}�� ����";
            mugProgressBar.value = (float)coffeeMachine.RemainingMugs / coffeeMachine.CurrentCoffee.MugQty; // ��ũ�ѹ� value�� ������Ʈ
        }
        else
        {
            currentMenuPanel.SetActive(false); 
        }
    }


}
