using System;
using UnityEngine;

public class InputPlayerManagerCustom : MonoBehaviour
{
    public event Action OnMoveLeft;
    public event Action OnMoveRight;

    private void Update()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch firstTouch = Input.GetTouch(0);

            if (firstTouch.phase == TouchPhase.Began)
            {
                Debug.LogWarning("Start Touching !!!");
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        { 
            MoveLeft(); 
        }
    }


    public void MoveLeft()
    {
        OnMoveLeft?.Invoke();
    }
    public void MoveRight()
    {
        OnMoveRight?.Invoke();
    }
}
