using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

enum Windows
{
    MakeCoffee = 0,
    Exit,
    CurrentMenu
}
public class UIManager : MonoBehaviour
{
    //enum���� ������ â �̸� �迭 �����ϱ�

    public static UIManager Instance;
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private TextMeshProUGUI _captionText;

    [Header("��ȭ�� �ؽ�Ʈ")]
    [SerializeField] private TextMeshProUGUI _coffeeBeanText;
    [SerializeField] private TextMeshProUGUI _coinText;
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
        foreach (var panel in _panels)
            panel.SetActive(false);
    }
    // ��� UI ��Ȱ��ȭ
    public void UpdateCoffeeBeanUI(int value)
    {
        _coffeeBeanText.text = value.ToString();
    }

    public void UpdateCoinUI(int value)
    {
        _coinText.text = value.ToString();
    }

    public void UpdateGemUI(int value)
    {
        gemText.text = value.ToString();
    }

    public void ShowRoastingWindow()
    {
        _panels[(int)Windows.MakeCoffee].SetActive(true);
    }

    public void ShowExitWindow()
    {
        _panels[(int)Windows.Exit].SetActive(true);
    }

    public void ShowCapitonText()
    {
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(PlayerCtrl.Instance.transform.position);

        _captionText.rectTransform.position = playerScreenPos;
        _captionText.enabled = true;
        _captionText.text = "�Ÿ��� �ʹ� �־��!";
    }

    public void ShowCurrentMenuWindow()
    {
        Debug.Log("���� �޴�â ���");
        _panels[(int)Windows.CurrentMenu].SetActive(true); 

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
