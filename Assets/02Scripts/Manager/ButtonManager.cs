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
        GameObject ingredientButton = GameObject.Find("ingredient_btn");
        GameObject makeButton = GameObject.Find("make_btn");
        
        ingredientButton.SetActive(false);
    }

    public void MakeDrinkButton()
    {
        Debug.Log("����� ����");

        // �����
    }



    public void QuitButton()
    {
        SoundManager.Instance.PlaySFX(click_clip, 0.6f);
        Application.Quit(); 
    }
    



    
    
}
