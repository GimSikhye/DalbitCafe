using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //enum���� ������ â �̸� �迭 �����ϱ�

    public static UIManager Instance;
    [SerializeField] private GameObject[] panels;
    //[SerializeField] private GameObject captionText;
    [SerializeField] private TextMeshProUGUI captionTmp;


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
        foreach(var item  in panels)
        {
            item.SetActive(false);  
        }
        captionTmp.enabled = false;
    }

    public void ShowPopup()
    {
        panels[0].SetActive(true);
    }

    public void ShowExitWindow()
    {

    }

    public void ShowCapitonText()
    {
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(PlayerCtrl.Instance.transform.position);

        captionTmp.rectTransform.position = playerScreenPos;
        captionTmp.enabled = true;
        captionTmp.text = "�Ÿ��� �ʹ� �־��!";
    }



}
