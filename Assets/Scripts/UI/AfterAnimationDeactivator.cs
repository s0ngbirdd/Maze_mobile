using UnityEngine;

public class AfterAnimationDeactivator : MonoBehaviour
{
    public void DeactivateAfterAnimation()
    {
        gameObject.SetActive(false);
    }
}
