using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatesIconsSingleUI : MonoBehaviour
{
    [SerializeField] private Image m_image;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        m_image.sprite = kitchenObjectSO.sprite;
    }
}
