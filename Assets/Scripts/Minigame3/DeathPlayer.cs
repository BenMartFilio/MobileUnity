using System.Collections.Generic;
using UnityEngine;

public class DeathPlayer : MonoBehaviour
{
    [SerializeField] private EndGestion endGestion;
    private List<OnLifeLose> whichHeart = new List<OnLifeLose>();
    private int CurrentHeartDisplayed;

    public void Death()
    {
        endGestion.OnEndGame();
    }
}
