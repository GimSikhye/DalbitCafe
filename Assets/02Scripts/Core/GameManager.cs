using UnityEngine;
using UnityEngine.SceneManagement;
using DalbitCafe.UI;
enum Bgm
{
    Main = 0,
    Game,
    End
}
namespace DalbitCafe.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [SerializeField] private AudioClip[] bgm_clips;

        [Header("��ȭ")]
        [SerializeField] private int _coffeeBean;
        [SerializeField] private int _gem;
        [SerializeField] private int _coin;



        void Awake()
        {
            if (Instance == null) // ù ��° GameManager��� ����
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else // ���� GameManager�� �ִٸ�, �� ���� GameManager�� �����ϰ� ���� ���� ����
            {
                Destroy(Instance.gameObject);
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            SceneManager.sceneLoaded += ChangeScene;
        }

        private void Start()
        {
            // ���� ���� �� ����� ������ �ҷ�����
            _coffeeBean = PlayerPrefs.GetInt("CoffeeBean", 100);
            _coin = PlayerPrefs.GetInt("Coin", 1000);
            _gem = PlayerPrefs.GetInt("Gem", 0);

            // UI ������Ʈ
            UIManager.Instance.UpdateCoffeeBeanUI(_coffeeBean);
            UIManager.Instance.UpdateCoinUI(_coin);
            UIManager.Instance.UpdateGemUI(_gem);

        }


        void Update()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    UIManager.Instance.ShowExitPopUp();
                }
            }
        }


        public int CoffeeBean
        {
            get { return _coffeeBean; }
            set
            {
                _coffeeBean = value;
                PlayerPrefs.SetInt("CoffeeBean", _coffeeBean); // �ڵ� ����
                UIManager.Instance.UpdateCoffeeBeanUI(_coffeeBean);
            }
        }

        public int Coin
        {
            get { return _coin; }
            set
            {
                _coin = value;
                PlayerPrefs.SetInt("Coin", _coin); // �ڵ� ����
                UIManager.Instance.UpdateCoinUI(_coin);
            }
        }

        public int Gem
        {
            get { return _gem; }
            set
            {
                _gem = value;
                PlayerPrefs.SetInt("Gem", _gem); // �ڵ� ����
                UIManager.Instance.UpdateGemUI(_gem);

            }
        }


        private void ChangeScene(Scene scene, LoadSceneMode mode)
        {
            switch (scene.name)
            {
                case "MainMenu":
                    Debug.Log("���θ޴�");
                    SoundManager.Instance.PlayBGM(bgm_clips[(int)Bgm.Main], 0.5f);
                    break;
                case "GameScene":
                    SoundManager.Instance.PlayBGM(bgm_clips[(int)Bgm.Game], 0.5f);
                    break;
            }
        }


    }

}
