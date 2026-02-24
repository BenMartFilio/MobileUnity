using UnityEngine;

public class GetPositionAndCallCanvas : MonoBehaviour
{
    [SerializeField] private MovementButtonCanvas canvasToMove;
    public void ChangerPos()
    {
        canvasToMove.ChangePosition(transform.position);
    }
}
