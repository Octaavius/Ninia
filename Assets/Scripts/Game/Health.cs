using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    ///////////////////////////////////////
    public GameManager GameManager;
    ///////////////////////////////////////
    
    public GameObject HeartPrefab;
    public RectTransform HeartParent;
    public List<GameObject> Hearts;
    [SerializeField] private int startNumberOfHearts = 3;
    
    private float HeartMargin = 115f;
    private int numberOfHearts = 0;
    private int maxHearts = 5;
    private Vector2 initialPosition = Vector2.zero;

    void Start()
    {
        InitializeHearts();
    }

    public void InitializeHearts()
    {
        for (int i = 0; i < startNumberOfHearts; i++)
        {
            Debug.Log("Adding heart");
            AddHeart();
        }
    }

    public void AddHeart()
    {
        Debug.Log("Start of adding");
        if (numberOfHearts >= maxHearts) return;

        // Instantiate the new heart
        GameObject newHeart = Instantiate(HeartPrefab, HeartParent);

        // Set the RectTransform properties
        RectTransform newHeartRect = newHeart.GetComponent<RectTransform>();

        // Set the anchors to the center
        newHeartRect.anchorMin = new Vector2(0.5f, 0.5f);
        newHeartRect.anchorMax = new Vector2(0.5f, 0.5f);

        // If this is the first heart, place it at the initial position
        if (Hearts.Count == 0)
        {
            newHeartRect.anchoredPosition = initialPosition;
        }
        else
        {
            // Otherwise, position it relative to the last heart
            newHeartRect.anchoredPosition = Hearts[Hearts.Count - 1].GetComponent<RectTransform>().anchoredPosition + new Vector2(HeartMargin, 0);
        }

        newHeartRect.localScale = Vector3.one;

        // Add the new heart to the list
        Hearts.Add(newHeart);

        numberOfHearts++;
        Debug.Log("End of adding");
    }

    public void RemoveHeart(GameManager gameManager)
    {
        // Destroy the last heart in the list
        Destroy(Hearts[numberOfHearts - 1]);
        Hearts.RemoveAt(numberOfHearts - 1);
        numberOfHearts--;

        if (numberOfHearts <= 0){
            GameManager.EndGame();
        } 
    }

    public int GetNumberOfHearts()
    {
        return numberOfHearts;
    }
}
