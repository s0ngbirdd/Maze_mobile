using System;
using UnityEngine;

public class Finish : MonoBehaviour
{
    // Public
    public static event Action OnFinish;
    public static event Action<Vector3> OnFinishPosition;

    private void OnTriggerEnter(Collider other)
    {
        OnFinishPosition?.Invoke(other.gameObject.transform.position);
        OnFinish?.Invoke();
    }
}
