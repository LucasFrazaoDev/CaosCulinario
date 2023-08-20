using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 5f;
    [SerializeField] private GameInput m_gameInput;
    private bool m_isWalking = false;

    private void Update()
    {
        Vector2 inputVector = m_gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * m_moveSpeed * Time.deltaTime;

        m_isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 25f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); 
    }

    public bool IsWalking()
    {
        return m_isWalking;
    }
}
