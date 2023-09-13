using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float m_moveSpeed = 5f;
    [SerializeField] private GameInput m_gameInput;
    [SerializeField] private LayerMask m_countersLayerMask;
    [SerializeField] private Transform m_kitchenObjectHoldPoint;

    private bool m_isWalking = false;
    private Vector3 m_lastInteractDir;
    private BaseCounter m_selectedCounter;
    private KitchenObject m_kitchenObject;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There isn't only one Player");
        Instance = this;
    }

    private void Start()
    {
        m_gameInput.OnInteractAction += M_gameInput_OnInteractAction;
        m_gameInput.OnInteractAlternateAction += M_gameInput_OnInteractAlternateAction;
    }

    private void M_gameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (m_selectedCounter != null)
            m_selectedCounter.InteractAlternate(this);
    }

    private void M_gameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(!GameManager.Instance.IsGamePlaying()) return;

        if (m_selectedCounter != null)
            m_selectedCounter.Interact(this);
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver()) return;

        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return m_isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = m_gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
            m_lastInteractDir = moveDir;

        float interactDistance = 2.0f;
        if (Physics.Raycast(transform.position, m_lastInteractDir, out RaycastHit raycastHit, interactDistance, m_countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                // Has ClearCounter
                if(baseCounter != m_selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = m_gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = m_moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2.0f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            // Cannot move towards moveDir
            // Attempt only X direction
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -0.5f || moveDir.x > 0.5f) & !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // Move only in X direction
                moveDir = moveDirX;
            }
            else
            {
                // Cannot move X direction
                // Attempt Z direction
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = (moveDir.z < -0.5f || moveDir.z > 0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // Can move only on Z
                    moveDir = moveDirZ;
                }
                else
                {
                    // cannot move in any direction
                }
            }
        }

        if (canMove)
            transform.position += moveDir * moveDistance;

        m_isWalking = moveDir != Vector3.zero;

        if (m_isWalking)
        {
            float rotateSpeed = 25f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }

    }

    private void SetSelectedCounter(BaseCounter selectedCouter)
    {
        m_selectedCounter = selectedCouter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = m_selectedCounter });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return m_kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        m_kitchenObject = kitchenObject;

        if(kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return m_kitchenObject;
    }

    public void ClearKitchenObject()
    {
        m_kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return m_kitchenObject != null;
    }
}
