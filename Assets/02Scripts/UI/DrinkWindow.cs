using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class DrinkWindow : MonoBehaviour
{
    [Header("�޴� ������ ����Ʈ")]
    [SerializeField] private List<CoffeeData> coffeDataList; // SO ������ ����Ʈ

    [Header("�޴� UI �г�")]
    [SerializeField] private List<GameObject> menuContainers; //menu COntainer �г� ����Ʈ

    void Start()
    {
        UpdateMenuUI();
    }

    void UpdateMenuUI()
    {
        for(int i = 0; i< coffeDataList.Count && i < menuContainers.Count; i++)
        {
            CoffeeData coffee = coffeDataList[i];
            GameObject container = menuContainers[i];

            // �� UI ��� ��������
            TextMeshProUGUI nameTmp = container.transform.Find("name_tmp").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI coinTmp = container.transform.Find("coin_tmp").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI mugQtyTmp = container.transform.Find("mugQty_tmp").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI beanUseTmp = container.transform.Find("beanUse_tmp").GetComponent<TextMeshProUGUI>();

            Image iconImg = container.transform.Find("icon_Img").GetComponent<Image>();

            // UI ������Ʈ
            nameTmp.text = coffee.CoffeName;
            coinTmp.text = coffee.Price.ToString();
            mugQtyTmp.text = "X " + coffee.MugQty.ToString();
            beanUseTmp.text = "- " + coffee.BeanUse.ToString();
            iconImg.sprite = coffee.MenuIcon;
            
        }
    }
}
