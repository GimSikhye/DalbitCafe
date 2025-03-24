using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance;
    [SerializeField] private AudioClip click_clip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

    }


    public void LoadButton(string sceneName)
    {
        SoundManager.Instance.PlaySFX(click_clip, 0.6f);
        Debug.Log("�� �̵�");
        SceneManager.LoadScene(sceneName);
    }

    public void CloseWindowButton(string windowName)
    {
        GameObject window = GameObject.Find(windowName);
        if (window == null)
        {
            Debug.LogError("��Ȱ��ȭ Ȱ �����츦 ã�� ���߽��ϴ�!");
            return;
        }

        Debug.Log($"��Ȱ��ȭ�� �г�: {window.name}");
        window.SetActive(false);
    }


    public void IngredientButton()
    {
        // ����� ��ư ���� Ingrdient Button ��Ȱ��ȭ�ϱ�.

        GameObject ingredientButton = GameObject.Find("ingredient_btn");
        GameObject makeButton = GameObject.Find("make_btn");
        GameObject beanUseTmp = GameObject.Find("beanUse_tmp");

        ingredientButton.SetActive(false);
        beanUseTmp.SetActive(false);
        makeButton.SetActive(true);
    }


    public void MakeDrinkButton(GameObject button)
    {
        // ���� ��ư�� ���� Menu Container ã��
        GameObject menuContainer = button.transform.parent?.gameObject;

        RoastingWindow roastingWindow = FindObjectOfType<RoastingWindow>();
        int index = roastingWindow.menuContainers.IndexOf(menuContainer);

        if (index < 0 || index >= roastingWindow.coffeDataList.Count)
        { 
            Debug.LogError("��ȿ���� ���� Ŀ�� �޴� ����!");
            return;
        }

        // �ش� �ε����� �̿��Ͽ� coffeDataList���� CoffeeData�� ��������
        CoffeeData coffeeData = roastingWindow.coffeDataList[index];

        // ���� �Ҹ� üũ
        if (GameManager.Instance.CoffeeBean >= coffeeData.BeanUse)
        {
            GameManager.Instance.CoffeeBean -= coffeeData.BeanUse;
            CoffeeMachine.LastTouchedMachine.RoastCoffee(coffeeData);
            Debug.Log($"{coffeeData.CoffeName} �ν��� ����!");
        }
        else
        {
            Debug.LogError("Ŀ������ �����մϴ�!");
        }
    }


    public void QuitButton()
    {
        SoundManager.Instance.PlaySFX(click_clip, 0.6f);
        Application.Quit();
    }






}
