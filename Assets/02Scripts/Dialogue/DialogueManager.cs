using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using DalbitCafe.Core;

namespace DalbitCafe.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        private AudioSource _audioSource;

        [Header("UI ���")]
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _characterText;
        [SerializeField] private Image _characterPortrait;
        [SerializeField] private Image FadePanel;
        [SerializeField] private SpriteRenderer BackgroundSprite;

        [Header("Event")]
        [SerializeField] private EventManager eventManager;

        [Header("Ÿ���� �ӵ�")]
        [SerializeField] private float TypingSpeed = 0f;

        [Header("��� ������")]
        [SerializeField] private DialogueData[] Groups;

        [Header("��� ȿ����")]
        [SerializeField] private AudioClip typingSound;


        private int len = 0; // ��ü so ����
        private int textlen = 0; // ���� so�� ����

        bool isTyping = false; // ���� Ÿ���� ������ 
        bool isEnd = false; // ��� ��簡 ��µǾ�����

        [SerializeField] private string SceneName;

        private Coroutine typingRoutine;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        private void Start()
        {
            typingRoutine = StartCoroutine(Typing(Groups[len].Lines[textlen]));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isEnd)
            {
                if (isTyping)
                {
                    // ���� Ÿ���� ���̸� ��� ��� �ؽ�Ʈ�� ���
                    StopCoroutine(typingRoutine);
                    _characterText.text = Groups[len].Lines[textlen];
                    isTyping = false;
                }
                else
                {
                    // Ÿ������ �������� ���� ���� �Ѿ
                    NextTalk();
                }
            }
        }

        private void NextTalk() // ���� ��� ���
        {
            if (isTyping) return; // Ÿ���� �߿��� ��ŵ���� �ʵ��� ����

            textlen++;

            if (textlen == Groups[len].Lines.Length)
            {
                len++; // ���� SO�� �Ѿ

                if (len != Groups.Length) // ��ü ��簡 ������ �ʾҴٸ� ���� SO��
                {
                    textlen = 0; // ���� SO�� �Ѿ�� �ʱ�ȭ
                    eventManager.EventChange(Groups[len]);
                    typingRoutine = StartCoroutine(Typing(Groups[len].Lines[textlen]));
                }
                else
                {
                    // ������ �����ٸ�
                    isEnd = true;
                    FadePanel.gameObject.SetActive(true);
                    StartCoroutine(GameManager.Instance.FadeIn(FadePanel, 1, EndTyping));
                }
            }
            else
            {
                typingRoutine = StartCoroutine(Typing(Groups[len].Lines[textlen]));
            }
        }

        IEnumerator Typing(string text)
        {
            _characterPortrait.sprite = Groups[len].CharacterSprite;
            _nameText.text = Groups[len].CharacterName;

            _characterText.text = string.Empty; // �ʱ�ȭ
            isTyping = true;

            for (int i = 0; i < text.Length; i++)
            {
                _characterText.text += text[i];

                if (typingSound != null && !_audioSource.isPlaying)
                {
                    Debug.Log("�Ҹ�");
                    _audioSource.PlayOneShot(typingSound, 0.5f);
                }

                yield return new WaitForSeconds(TypingSpeed); // �ؽ�Ʈ ��� �ӵ� ���� (0.05�ʷ� ������)
            }

            isTyping = false; // Ÿ���� �Ϸ�
        }

        private void EndTyping()
        {
            if (isEnd) // ��� ��簡 ���� ��쿡�� �� ��ȯ
            {
                SceneManager.LoadScene(SceneName);
            }
        }
    }

}

