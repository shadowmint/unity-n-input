using UnityEngine;

public class Origin : MonoBehaviour
{
    public Vector3 position;

    public void Bind()
    {
        position = gameObject.transform.position;
    }
}
