using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    [Header("�̱���")]
    public static PlayerCtrl Instance;
    [Space(10)]
    [Header("��ġ UI")]
    [SerializeField] private Image touch_feedback;
    [Header("Ŀ�Ǹӽ� ����")]
    [SerializeField] private Transform coffeeMachine;
    [SerializeField] private float interactionRange;
    [Header("�÷��̾� �̵�")]
    private float moveSpeed = 3f;

    private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRender { get { return spriteRenderer; } }

    private bool startedOverUI = false; // ��ġ ���� �� UI �� ���� ���
    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            spriteRenderer = GetComponent<SpriteRenderer>();
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
                startedOverUI = IsTouchOverUI(touch);
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
                    Debug.Log("��ġ ������ UI ������ �̷������");
                    startedOverUI = false; // �ʱ�ȭ
                    return;
                }

                {
                    // ��ġ �������� ��ġ�� ���� layer�� floor�� �ƴ϶�� return
                    Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    Collider2D hitCollider = Physics2D.OverlapPoint(touchPosition);

                    if (hitCollider == null || hitCollider.gameObject.layer != LayerMask.NameToLayer("Floor"))
                    {
                        Debug.Log("�ٴ����θ� �̵��� �� �ֽ��ϴ�.");
                        return;
                    }

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

        if (hitCollider != null && hitCollider.transform == coffeeMachine)
        {
            // �Ÿ��� Ȯ���ؼ� ����� ��츸 �˾� ǥ��
            if (Vector3.Distance(transform.position, coffeeMachine.position) < interactionRange)
            {
                UIManager.Instance.ShowPopup(); // UIManager�� �˾� ǥ�� �Լ� ȣ��
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

        if(!isMoving) // �̵� ���� �ƴ� ���� �̵� ����
        {
            StartCoroutine(MoveToTarget());
        }

    }

    private IEnumerator MoveToTarget()
    {
        isMoving = true;
        while(Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition; // ��Ȯ�� ��ġ ����
        isMoving = false;
        touch_feedback.enabled = false;

    }

    // ��ġ ��ġ�� UI ������ �Ǵ��ϴ� Ŀ���� �Լ�
    private bool IsTouchOverUI(Touch touch)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = touch.position;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void ShowTouchFeedback(Vector2 screenPosition)
    {
        // UI ����̹Ƿ� localPosition�� ����Ͽ� ĵ���� ������ ��ǥ ����
        touch_feedback.rectTransform.position = screenPosition;
        touch_feedback.enabled = true;
    }




}
