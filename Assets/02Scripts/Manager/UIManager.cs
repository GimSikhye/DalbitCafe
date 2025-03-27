using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

enum Windows
{
    Roasting = 0,
    Exit,
    CurrentMenu
}
public class UIManager : MonoBehaviour
{
    //enum���� ������ â �̸� �迭 �����ϱ�

    public static UIManager Instance;
    [SerializeField] private GameObject[] panels;
    [SerializeField] private TextMeshProUGUI captionTmp;

    [Header("��ȭ�� �ؽ�Ʈ")]
    [SerializeField] private TextMeshProUGUI coffeeBeanText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI gemText;

    // ���� ���� ���� �ٲ� �������ֱ�.
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        foreach (var panel in panels)
            panel.SetActive(false);
    }
    // ��� UI ��Ȱ��ȭ
    public void UpdateCoffeeBeanUI(int value)
    {
        coffeeBeanText.text = value.ToString();
    }

    public void UpdateCoinUI(int value)
    {
        coinText.text = value.ToString();
    }

    public void UpdateGemUI(int value)
    {
        gemText.text = value.ToString();
    }

    public void ShowRoastingWindow()
    {
        panels[(int)Windows.Roasting].SetActive(true);
    }

    public void ShowExitWindow()
    {
        panels[(int)Windows.Exit].SetActive(true);
    }

    public void ShowCapitonText()
    {
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(PlayerCtrl.Instance.transform.position);

        captionTmp.rectTransform.position = playerScreenPos;
        captionTmp.enabled = true;
        captionTmp.text = "�Ÿ��� �ʹ� �־��!";
    }

    public void ShowCurrentMenuWindow()
    {
        Debug.Log("���� �޴�â ���");
        panels[(int)Windows.CurrentMenu].SetActive(true); 

    }

    public void CloseWindow(string window)
    {
        GameObject windowPanel = GameObject.Find(window);
        windowPanel.SetActive(false);

    }



    // ��ġ ��ġ�� UI ������ �Ǵ���
    public bool IsTouchOverUI(Touch touch)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = touch.position;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }

}
