using UnityEngine;

public class WearMask : MonoBehaviour
{
    public Vector3 maskSize;
    public Vector3 facePosition;
    public void PutOnMask(int id)
    {
        if (MaskManager.Instance.ExistMask(id))
        {
            var mask = MaskManager.Instance.LoadMask(id);
            mask.transform.parent = transform;
            mask.transform.localScale = maskSize;
            mask.transform.localPosition = facePosition;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector3)(transform.rotation * facePosition * transform.localScale.x) + transform.position, 1f);
    }
}
