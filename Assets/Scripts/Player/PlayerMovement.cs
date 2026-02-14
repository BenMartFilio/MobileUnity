using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform[] m_transforms;
    [SerializeField] private InputPlayerManagerCustom m_inputManager;
    private Rigidbody2D rb;
    private int m_index = 2;
    private int m_moveSpeed = 1;

    private void OnEnable()
    {
        m_inputManager.OnMoveLeft += MoveToPreviousPosition;
        m_inputManager.OnMoveRight += MoveToNextPosition;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_index = 2;
        UpdatePosition(1);
    }

    public void MoveToNextPosition()
    {
        m_index += m_moveSpeed;
        m_index = Mathf.Clamp(m_index,0,m_transforms.Length-1);
        UpdatePosition(1);
    }
    public void MoveToPreviousPosition() 
    {
        m_index -= m_moveSpeed;
        m_index = Mathf.Clamp(m_index, 0, m_transforms.Length - 1);
        UpdatePosition(-1);
    }
    public void MoveToDirection(int direction) //direction -1 ou 1
    {
        m_index += m_moveSpeed*direction;
        m_index = Mathf.Clamp(m_index, 0, m_transforms.Length - 1);
        UpdatePosition(direction);
    }
    private void UpdatePosition(float Orientation)
    {
        transform.position = m_transforms[m_index].position;
        Quaternion actualRotation = transform.rotation;
        actualRotation.y = Mathf.Clamp(180f * Orientation*-1, 0f, 180f);
        transform.rotation = actualRotation;
    }
}
