using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO m_kitchenObjectSO;
    [SerializeField] private Transform m_counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;

    [SerializeField] private bool testing;
    private KitchenObject m_kitchenObject;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && testing)
        {
            if(m_kitchenObject != null)
            {
                m_kitchenObject.SetClearCounter(secondClearCounter);
            }
        }
    }

    public void Interact()
    {
        if (m_kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(m_kitchenObjectSO.prefab, m_counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
            Debug.Log(m_kitchenObject.GetClearCounter());
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return m_counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        m_kitchenObject = kitchenObject;
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
