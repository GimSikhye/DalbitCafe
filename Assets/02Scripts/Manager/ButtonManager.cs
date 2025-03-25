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
            SceneManager.sceneLoaded += OnSceneLoaded; // �� �ε� �� ȣ��

        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"�� �ε��: {scene.name}");
        // ���� �ٲ� �� GameManager�� �ٽ� ã�������� ����
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager �ν��Ͻ��� ã�� �� ����!");
        }
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
        window.SetActive(false);
    }


    public void RoastingButton(GameObject button)
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
