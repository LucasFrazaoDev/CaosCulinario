using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 5f;
    [SerializeField] private GameInput m_gameInput;
    [SerializeField] private LayerMask m_countersLayerMask;

    private bool m_isWalking = false;
    private Vector3 m_lastInteractDir;

    private void Start()
    {
        m_gameInput.OnInteractAction += M_gameInput_OnInteractAction;
    }

    private void M_gameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        Vector2 inputVector = m_gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
            m_lastInteractDir = moveDir;

        float interactDistance = 2.0f;
        if (Physics.Raycast(transform.position, m_lastInteractDir, out RaycastHit raycastHit, interactDistance, m_countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                // Has ClearCounter
                clearCounter.Interact();
            }
        }
    }

    private void Update()
    {
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
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                // Has ClearCounter
            }
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
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

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
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

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

        float rotateSpeed = 25f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

    }
}
