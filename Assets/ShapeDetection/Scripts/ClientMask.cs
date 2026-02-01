using UnityEngine;
using UnityEngine.U2D;

public class ClientMask : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteShapeController shape;
    private int id;
    private MaterialPropertyBlock mpb;

    public void Initialize(int id)
    {
        if (TextureManager.Instance.TextureExist(id) && ShapeManager.Instance.ShapeExist(id))
        {
            ModifySprite(TextureManager.Instance.LoadTexture(id));
            ModifyShape(ShapeManager.Instance.GetShape(id));
        }
    }

    private void ModifyShape(Vector2[] points)
    {
        shape.spline.Clear();
        for (int i = 0; i < points.Length; i++)
        {
            shape.spline.InsertPointAt(i, points[i]);
        }
    }

    private void ModifySprite(Texture2D texture)
    {
        mpb = new MaterialPropertyBlock();
        sprite.GetPropertyBlock(mpb);
        mpb.SetTexture("_MainTex", texture);
        sprite.SetPropertyBlock(mpb);
    }
}
