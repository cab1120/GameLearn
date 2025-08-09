using UnityEngine;

public class CatchPen : MonoBehaviour
{
    public GameObject keyPrefab;
    public GameObject Player;
    private float newDistance;

    private float oldDistance;
    private float scale;

    private void LateUpdate()
    {
        GetDistance();
    }

    private void GetDistance()
    {
        newDistance = Vector3.Distance(Player.transform.position, keyPrefab.transform.position);
        if (oldDistance == 0) oldDistance = newDistance;
        if (newDistance - oldDistance > 0.03 || newDistance - oldDistance < -0.03)
        {
            scale = newDistance / oldDistance;
            ChangeKey(scale);
            oldDistance = newDistance;
        }
    }

    private void ChangeKey(float Scale)
    {
        var boxCollider = keyPrefab.GetComponent<BoxCollider>();

        var oldHeighty = boxCollider.size.y * keyPrefab.transform.localScale.y;
        var Ground = keyPrefab.transform.position.y - oldHeighty * 0.5f;

        keyPrefab.transform.localScale *= Scale;
//        Debug.Log(Scale);
        var newHeighty = boxCollider.size.y * keyPrefab.transform.localScale.y;
        var newY = Ground + newHeighty * 0.5f;

        var newPos = new Vector3(keyPrefab.transform.position.x, newY, keyPrefab.transform.position.z);

        keyPrefab.transform.position = newPos;
    }
}