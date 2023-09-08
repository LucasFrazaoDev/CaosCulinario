using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSucess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO m_recipeListSO;
    private List<RecipeSO> m_waitingRecipeSOList;
    private float m_spawnRecipeTimer;
    private float m_spawnRecipeTimerMax = 4f;
    private int m_waitingRecipeMax = 4;
    private int m_successfulRecipeAmount;

    private void Awake()
    {
        Instance = this;
        m_waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        m_spawnRecipeTimer -= Time.deltaTime;
        if(m_spawnRecipeTimer <= 0f)
        {
            m_spawnRecipeTimer = m_spawnRecipeTimerMax;

            if(m_waitingRecipeSOList.Count < m_waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = m_recipeListSO.recipeSOList[UnityEngine.Random.Range(0, m_recipeListSO.recipeSOList.Count)];
                m_waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < m_waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = m_waitingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // Has the same number of ingredients
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    // Cycling through all ingredients in the recipe
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        // Cycling through all ingredients in the plate
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            // ingredient matches
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        // This recipe ingredient wasn't found on the plate
                        plateContentsMatchesRecipe = false;
                    }
                }
                if(plateContentsMatchesRecipe)
                {
                    //`Player delivered the correct recipe!
                    m_successfulRecipeAmount++;
                    
                    m_waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSucess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        // No matches found, wrong recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return m_waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return m_successfulRecipeAmount;
    }
}
