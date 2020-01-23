using UnityEngine;

/// <summary>
/// Puts together the images asked for.
/// </summary>
public class Assembler : MonoBehaviour
{
    /// <summary>
    /// The target sprite renderers. These should be in the same order as the
    /// desired texture locations.
    /// </summary>
    public SpriteRenderer[] spriteRenderers;

    /// <summary>
    /// The texture to sprite script that contains the textures.
    /// </summary>
    public TextureToSprite textures;

    /// <summary>
    /// Puts the images together.
    /// </summary>
    public void Assemble()
    {
        
        for (int i = 1; i <= spriteRenderers.Length; ++i)
        {
            var tex = textures.textureList[i];
            var sr = spriteRenderers[i-1];

            sr.sprite = Sprite.Create(
                tex, 
                new Rect(0, 0, tex.width, tex.height), 
                new Vector2(0.5f, 0.5f)
            );
        }

        Vector3 adjustment = new Vector3(-545.36f, -318.98f, 259.59f);
        transform.localPosition = adjustment;
    }
}
