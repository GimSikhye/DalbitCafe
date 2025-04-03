using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using DalbitCafe.Core;

namespace DalbitCafe.Dialogue
{
    public class DialogueManager : MonoBehaviour, IPointerClickHandler
    {
        private AudioSource _audioSource;

        [Header("UI ���")]
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private Image _characterPortrait;

        [Header("Event")]
        [SerializeField] private EventManager _eventManager;

        [Header("Ÿ���� �ӵ�")]
        [SerializeField] private float _typingSpeed = 0f;

        [Header("��� ������")]
        [SerializeField] private DialogueData[] _groups;

        [Header("��� ȿ����")]
        [SerializeField] private AudioClip _typingSound;


        [SerializeField] private int _len = 0; // ��ü so ���� (�׷��� ��)
        private int _textIndex = 0; // ���� so�� ���� (���� ��µǴ� ����� ����)

        private bool _isTyping = false; // ���� Ÿ���� ������ 
        private bool _isEnd = false; // ��� ��簡 ��µǾ�����

        [SerializeField] private string _sceneName;

        private Coroutine _typingRoutine;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        private void Start()
        {
            _typingRoutine = StartCoroutine(Typing(_groups[_len].Lines[_textIndex]));
        }

        private void NextTalk() // ���� ��� ���
        {
            if (_isTyping) return; // Ÿ���� �߿��� ��ŵ���� �ʵ��� ����

            _textIndex++; //0 1 2 3 4 5

            if (_textIndex == _groups[_len].Lines.Length) // ���� �׷��� �ؽ�Ʈ�� �� ��µǾ��ٸ�
            {
                _len++; // ���� SO�� �Ѿ //0 1 2 3 4 5

                if (_len != _groups.Length) // ��ü ��簡 ������ �ʾҴٸ� ���� SO�� //Length�� 2��� 0 1
                {
                    _textIndex = 0; // ���� SO�� �Ѿ�� �ʱ�ȭ
                    _eventManager.EventChange(_groups[_len]);
                    _typingRoutine = StartCoroutine(Typing(_groups[_len].Lines[_textIndex]));
                }
                else
                {
                    // ������ �׷���� ������ �����ٸ�
                    _isEnd = true;
                    // ���� ������ �̵�
                    EndTyping();
                }
            }
            else
            {
                _typingRoutine = StartCoroutine(Typing(_groups[_len].Lines[_textIndex]));
            }
        }

        IEnumerator Typing(string lineText)
        {
            _characterPortrait.sprite = _groups[_len].CharacterSprite;
            _nameText.text = _groups[_len].CharacterName;

            _dialogueText.text = string.Empty; // �ʱ�ȭ
            _isTyping = true;

            for (int i = 0; i < lineText.Length; i++)
            {
                _dialogueText.text += lineText[i]; // �� ���ھ� ���

                yield return new WaitForSeconds(_typingSpeed); // �ؽ�Ʈ ��� �ӵ� ���� (0.05�ʷ� ������)
            }

            _isTyping = false; // Ÿ���� �Ϸ�
        }

        private void EndTyping()
        {
            if (_isEnd) // ��� ��簡 ���� ��쿡�� �� ��ȯ
            {
                SceneManager.LoadScene(_sceneName);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isEnd) // ���� ������� �ؽ�Ʈ ��ü ���
            {
                if (_isTyping)
                {
                    // ���� Ÿ���� ���̸� ��� ��� �ؽ�Ʈ�� ���
                    StopCoroutine(_typingRoutine);
                    _dialogueText.text = _groups[_len].Lines[_textIndex];
                    _isTyping = false;
                }
                else
                {
                    // Ÿ������ �������� ���� ���� �Ѿ
                    NextTalk();
                }
            }
        }
    }

}

