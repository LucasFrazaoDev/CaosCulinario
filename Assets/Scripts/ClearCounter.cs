using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO m_kitchenObjectSO;
    [SerializeField] private Transform m_counterTopPoint;

    public void Interact()
    {
        Debug.Log("Interagiu!");
        Transform kitchenObjectTransform = Instantiate(m_kitchenObjectSO.prefab, m_counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;

        Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
    }
}
