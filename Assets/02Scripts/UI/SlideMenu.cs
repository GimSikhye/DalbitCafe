using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace DalbitCafe.UI
{
    public class SlideMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _menuButton;
        [SerializeField] private List<RectTransform> _hiddenButtons; // ������ ��ư��
        [SerializeField] private float _spacing = 150; // ��ư ����
        [SerializeField] private float _duration = 0.5f; // �ִϸ��̼� ���ӽð�

        private bool _isMenuOpen = false; // �޴� ���� ����
        private bool _isAnimating = false; // �ߺ� ��ġ ����
        private List<Vector2> _originalPositions = new List<Vector2>(); // ��ư ���� ��ġ ����

        private void Start()
        {
            // ��ư Ŭ�� �̺�Ʈ ���
            _menuButton.onClick.AddListener(ToggleMenu);

            // �ʱ� ��ġ ���� (���ʿ� �����)
            foreach (var btn in _hiddenButtons)
            {
                _originalPositions.Add(btn.anchoredPosition); // ���� ��ġ ����
                btn.anchoredPosition = _menuButton.GetComponent<RectTransform>().anchoredPosition; // �޴� ��ư ��ġ�� ����

                // ��ư �����
                btn.gameObject.SetActive(false);
            }
        }

        private void ToggleMenu()
        {
            // UI �ߺ� �Է� ����
            if (EventSystem.current.IsPointerOverGameObject() == false) return;

            // �ִϸ��̼� ���̸� ���� ����
            if (_isAnimating) return;
            _isAnimating = true;

            _isMenuOpen = !_isMenuOpen;

            for (int i = 0; i < _hiddenButtons.Count; i++)
            {
                RectTransform btn = _hiddenButtons[i];

                if (_isMenuOpen)
                {
                    // ��ư Ȱ��ȭ �� �ִϸ��̼� ����
                    btn.gameObject.SetActive(true);

                    Vector2 targetPos = _originalPositions[i] - new Vector2(_spacing * (i + 1), 0);
                    btn.DOAnchorPos(targetPos, _duration).SetEase(Ease.OutQuad)
                        .OnComplete(() => _isAnimating = false);
                }
                else
                {
                    // ��ư�� �ٽ� �޴� ��ư���� �̵�
                    btn.DOAnchorPos(_menuButton.GetComponent<RectTransform>().anchoredPosition, _duration).SetEase(Ease.InQuad)
                        .OnComplete(() =>
                        {
                            btn.gameObject.SetActive(false); // ��ư ����
                            _isAnimating = false;
                        });
                }
            }

        }
    }
}

