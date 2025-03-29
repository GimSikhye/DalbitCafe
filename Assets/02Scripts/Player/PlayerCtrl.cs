using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using DalbitCafe.UI;
using DalbitCafe.Operations;
namespace DablitCafe.Player
{
    public class PlayerCtrl : MonoBehaviour
    {
        [Header("�̱���")]
        public static PlayerCtrl Instance;
        [Space(10)]
        [Header("��ġ UI")]
        [SerializeField] private Image touch_feedback;
        [Header("Ŀ�Ǹӽ� ����")]
        [SerializeField] private float interactionRange;
        [Header("�÷��̾� �̵�")]
        private float moveSpeed = 3f;

        private SpriteRenderer spriteRenderer;
        public SpriteRenderer SpriteRender { get { return spriteRenderer; } }
        private Animator animator;
        private bool startedOverUI = false; // ��ġ ���� �� UI �� ���� ���
        private Vector3 targetPosition;
        private bool isMoving = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                spriteRenderer = GetComponent<SpriteRenderer>();
                animator = GetComponent<Animator>();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Update()
        {
            OnTouch();
        }

        private void OnTouch()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // ��ġ ���� �� UI ������ üũ�ϰ� ���
                if (touch.phase == TouchPhase.Began)
                {
                    startedOverUI = UIManager.Instance.IsTouchOverUI(touch);
                }
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    ShowTouchFeedback(touch.position);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    CoffeMachine(touch);

                    // ��ġ ���� ��, UI���� �������� ���� ��쿡�� �̵� ó��
                    if (startedOverUI)
                    {
                        startedOverUI = false; // �ʱ�ȭ
                        return;
                    }

                    {
                        // ��ġ �������� ��ġ�� ���� layer�� floor�� �ƴ϶�� return
                        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                        Collider2D hitCollider = Physics2D.OverlapPoint(touchPosition);

                        if (hitCollider == null || hitCollider.gameObject.layer != LayerMask.NameToLayer("Floor")) return;
                    }
                    // �̵� ó��
                    OnMove(touch);
                }
            }
        }

        private void CoffeMachine(Touch touch)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane)); //��ġ�� ����
            Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);

            if (hitCollider != null && hitCollider.transform.CompareTag("Coffee Machine"))
            {
                // �Ÿ��� Ȯ���ؼ� ����� ��츸 �˾� ǥ��
                if (Vector3.Distance(transform.position, hitCollider.transform.position) < interactionRange)
                {
                    CoffeeMachine.SetLastTouchedMachine(hitCollider.GetComponent<CoffeeMachine>());
                    if (hitCollider.gameObject.GetComponent<CoffeeMachine>().IsRoasting == true)
                    {
                        UIManager.Instance.ShowCurrentMenuPopUp();
                        GameObject currentMenuWindow = GameObject.Find("currentMenu Window");
                        currentMenuWindow.GetComponent<CurrentMenuWindow>().UpdateMenuPanel(hitCollider.gameObject.GetComponent<CoffeeMachine>());

                    }
                    else
                    {
                        UIManager.Instance.ShowMakeCoffeePopUp(); // UIManager�� �˾� ǥ�� �Լ� ȣ��
                                                                  // currentMenuWindow.UpdateMenuPanel(); //Ŀ�ǵ����� �ֱ�
                    }
                }
                else
                {
                    Debug.Log("�Ÿ��� �ʹ� �־��!!");
                    UIManager.Instance.ShowCapitonText();
                }
            }
        }

        private void OnMove(Touch touch)
        {

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));
            worldPosition.z = 0;
            targetPosition = worldPosition; // �̵� ��ǥ ��ġ ����

            if (!isMoving) // �̵� ���� �ƴ� ���� �̵� ����
            {
                StartCoroutine(MoveToTarget());
            }

        }

        private IEnumerator MoveToTarget()
        {
            isMoving = true;
            animator.SetBool("isMoving", true);

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                Vector3 direction = (targetPosition - transform.position).normalized;
                SetAnimation(direction); // �̵� ���� �ִϸ��̼� ����

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = targetPosition; // ��Ȯ�� ��ġ ����
            isMoving = false;
            animator.SetBool("isMoving", false);

            touch_feedback.enabled = false;
        }

        private void SetAnimation(Vector3 direction)
        {

            // �̵� ������ Normalize�Ͽ� MoveX, MoveY �� ����
            Vector3 normalizedDirection = direction.normalized;
            animator.SetFloat("MoveX", normalizedDirection.x);
            animator.SetFloat("MoveY", normalizedDirection.y);

        }


        private void ShowTouchFeedback(Vector2 screenPosition)
        {
            // UI ����̹Ƿ� localPosition�� ����Ͽ� ĵ���� ������ ��ǥ ����
            touch_feedback.rectTransform.position = screenPosition;
            touch_feedback.enabled = true;
        }




    }


}

