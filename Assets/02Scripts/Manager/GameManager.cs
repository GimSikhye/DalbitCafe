using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private AudioClip[] bgm_clips;

    [Header("��ȭ")]
    [SerializeField] private int coffeeBean;
    [SerializeField] private int gem;
    [SerializeField] private int coin;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }else 
            Destroy(gameObject);

        SceneManager.sceneLoaded += ChangeScene;
    }

    private void Start()
    {
        // ���� ���� �� ����� ������ �ҷ�����
        coffeeBean = PlayerPrefs.GetInt("CoffeeBean", 100);
        coin = PlayerPrefs.GetInt("Coin", 1000);
        gem = PlayerPrefs.GetInt("Gem", 0);

        // UI ������Ʈ
        UIManager.Instance.UpdateCoffeeBeanUI(coffeeBean);
        UIManager.Instance.UpdateCoinUI(coin);
        UIManager.Instance.UpdateGemUI(gem);

    }


    public int CoffeeBean
    {
        get { return coffeeBean; }
        set
        {
            coffeeBean = value;
            PlayerPrefs.SetInt("CoffeeBean", coffeeBean); // �ڵ� ����
            UIManager.Instance.UpdateCoffeeBeanUI(coffeeBean);
        }
    }

    public int Coin
    {
        get { return coin; }
        set
        {
            gem = value;
            PlayerPrefs.SetInt("Coin", coin); // �ڵ� ����
            UIManager.Instance.UpdateCoinUI(coin);
        }
    }

    public int Gem
    {
        get { return gem; }
        set
        {
            gem = value;
            PlayerPrefs.SetInt("Gem", gem); // �ڵ� ����
            UIManager.Instance.UpdateGemUI(gem);

        }
    }


    private void ChangeScene(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Main menu":
                Debug.Log("���θ޴�");
                SoundManager.Instance.PlayBGM(bgm_clips[0], 0.5f);
                break;
            case "Game scene":
                //Debug.Log("���Ӿ�");
                SoundManager.Instance.PlayBGM(bgm_clips[1], 0.5f);
                break;
        }
    }

    void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                UIManager.Instance.ShowExitWindow();
            }
        }
    }
}
