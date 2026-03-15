using System;
using UnityEngine;

public class LastInputSystem3 : MonoBehaviour
{
    public event Action OnTapScreen;
    [SerializeField] private float _tapDuration = 0.5f;

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position

    private float width = 0.0f;
    private float height = 0.0f;
    private float dragDistance;  

    private void Start()
    {
        width = Screen.width;
        height = Screen.height;

        dragDistance = Screen.height * 15 / 100;

        /*_tapAction = InputSystem.actions.FindAction("Tap");*/
    }
    private void Update()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {
                }
                else
                {
                    OnTapScreen?.Invoke();
                }
            }
        }
    }
}
