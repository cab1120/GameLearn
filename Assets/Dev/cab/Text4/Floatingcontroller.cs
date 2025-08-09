using UnityEngine;

public class Floatingcontroller : MonoBehaviour
{
    public float floatForce = 0.2f;
    public float forceChangeInterval = 1.5f;

    private Vector3 currentForce;
    private Rigidbody rb;
    private float timeSinceLastChange;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ChooseNewDirection();
    }

    private void Update()
    {
        timeSinceLastChange += Time.fixedDeltaTime;

        if (timeSinceLastChange >= forceChangeInterval)
        {
            ChooseNewDirection();
            timeSinceLastChange = 0f;
        }

        rb.AddForce(currentForce * floatForce);
    }

    private void ChooseNewDirection()
    {
        var angle = Random.Range(0f, 2 * Mathf.PI);
        currentForce = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    // 被盒子传递速度时调用
    public void AddBoxVelocity(Vector3 boxVelocity)
    {
        rb.velocity = boxVelocity;
    }

    public void AddBoxPosition(Vector3 boxPosition)
    {
        rb.MovePosition(rb.position + boxPosition);
    }
}