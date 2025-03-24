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

        // 1. ���� ��ư�� ���� Menu Container ã��
        GameObject menuContainer = button.transform.parent?.gameObject;
        if (menuContainer == null)
        {
            Debug.LogError("Menu Container�� ã�� �� �����ϴ�! (��ư�� �θ� ������Ʈ�� ����)");
            return;
        }
        Debug.Log($"ã�� Menu Container: {menuContainer.name}");

        // 2. DrinkWindow ������Ʈ ã��
        DrinkWindow drinkWindow = FindObjectOfType<DrinkWindow>();
        int index = drinkWindow.menuContainers.IndexOf(menuContainer);
        if (index < 0)
        {
            Debug.LogError("�ش� Menu Container�� DrinkWindow.menuContainers ����Ʈ�� �����ϴ�!");
            return;
        }

        // �ش� �ε����� �̿��Ͽ� coffeDataList���� CoffeeData�� ��������
        CoffeeData coffeeData = drinkWindow.coffeDataList[index];
        if (coffeeData == null)
        {
            Debug.LogError("coffeDataList[" + index + "]�� null�Դϴ�!");
            return;
        }

        int beanUseAmount = coffeeData.BeanUse; // Ŀ�Ǹ� ���� �� ����� ���� �Ҹ�

        // 5. GameManager.Instance�� Ŀ���� �������� beanUseAmount��ŭ ����
        if (GameManager.Instance.CoffeeBean >= beanUseAmount)
        {
            GameManager.Instance.CoffeeBean -= beanUseAmount;
            Debug.Log($"{coffeeData.CoffeName} Ŀ�Ǹ� ����ϴ�. ���� Ŀ����: {GameManager.Instance.CoffeeBean}");
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
