using System.Collections.Generic;
using UnityEngine;

public class CameraObstructionHandler : MonoBehaviour
{
    public Transform player;
    public LayerMask obstructionMask;
    public float transparency = 0.2f;

    private List<Renderer> currentObstructions = new List<Renderer>();

    void Update()
    {
        ClearObstructions();

        Vector3 direction = player.position - transform.position;
        float distance = Vector3.Distance(player.position, transform.position);

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance, obstructionMask);
        //RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance);

        foreach (RaycastHit hit in hits)
        {
            Debug.Log("Hit object: " + hit.collider.name);

            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null)
            {
                MakeTransparent(rend);
                if (!currentObstructions.Contains(rend))
                    currentObstructions.Add(rend);
            }
        }
    }

    void MakeTransparent(Renderer rend)
    {
        foreach (Material mat in rend.materials)
        {
            Color color = mat.color;
            color.a = transparency;
            mat.color = color;
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
        }
    }

    void ClearObstructions()
    {
        foreach (Renderer rend in currentObstructions)
        {
            foreach (Material mat in rend.materials)
            {
                Color color = mat.color;
                color.a = 1f;
                mat.color = color;
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1);
                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.renderQueue = -1;
            }
        }

        currentObstructions.Clear();
    }
}
