using System.Collections.Generic;
using UnityEngine;

public class LifeBarGestion : MonoBehaviour
{
    [SerializeField] private GameObject lifeHeart;
    [SerializeField] private EndGestion endGestion;
    private List<OnLifeLose> whichHeart = new List<OnLifeLose>();
    private int CurrentHeartDisplayed;
    public void OnLifeUpdate(int Life)
    {
        if (Life < CurrentHeartDisplayed)
        {
            for (int i = 0; i < CurrentHeartDisplayed - Life; i++)
            {
                whichHeart[CurrentHeartDisplayed - (i+1)].LifeLosed();
                whichHeart.RemoveAt(CurrentHeartDisplayed - (i+1));
            }
            CurrentHeartDisplayed = Life;
        }

        else if (Life > CurrentHeartDisplayed) 
        {
            for (int i = 0; i < Life - CurrentHeartDisplayed; i++)
            {
                GameObject newCreatedHeart = Instantiate(lifeHeart);
                newCreatedHeart.transform.SetParent(this.transform);
                whichHeart.Add(newCreatedHeart.GetComponent<OnLifeLose>());
            }
            CurrentHeartDisplayed = Life;
        }

        if (Life <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        endGestion.OnEndGame();
    }
}
