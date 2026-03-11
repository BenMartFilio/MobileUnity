using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class SliceObject : MonoBehaviour
{
    [SerializeField] private GameObject unslicedObject;
    private GameObject slicedObject;

    private Rigidbody2D rb;
    private Collider2D col;

    public float force = 5f;
    public float torque = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = rb.GetComponent<Collider2D>();
    }
    public void setSlicedObject (GameObject slicedObject)
    {
        this.slicedObject = slicedObject;
    }

    public GameObject getSlicedObject()
    {
        return slicedObject;
    }

    public void Slice(Vector2 sliceDirection)
    {
        unslicedObject.SetActive(false);
        slicedObject.SetActive(true);

        for (int i = 0; i < slicedObject.transform.childCount; i++) 
        {
            Rigidbody2D slicerRb = slicedObject.transform.GetChild(i).GetComponent<Rigidbody2D>();
            slicerRb.linearVelocity = rb.linearVelocity;

            Vector2 perpendicular = new Vector2(-sliceDirection.y, sliceDirection.x);
            slicerRb.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
            if (i == 0)
            {
                slicerRb.AddForce(-perpendicular * force, ForceMode2D.Impulse);
            }
            else
            {
                slicerRb.AddForce(perpendicular * force, ForceMode2D.Impulse);
            }
                

            slicerRb.AddTorque(UnityEngine.Random.Range(-torque, torque), ForceMode2D.Impulse);
        }


        col.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
