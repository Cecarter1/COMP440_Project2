using UnityEngine;

[RequireComponent(typeof(BaseShapeManager))]
public class RadialBackgroundSectors : MonoBehaviour
{
    [Header("Geometry")]
    public int sectors = 3;         // triangle -> 3 wedges
    public float radius = 30f;      // big enough to fill the screen

    [Header("Material")]
    public Material sectorMaterial; // drag a simple Unlit material here

    private void Start()
    {
        GenerateSectors();
    }

#if UNITY_EDITOR
    // so you can tweak radius in edit mode
    private void OnValidate()
    {
        if (!Application.isPlaying) return;
        ClearChildren();
        GenerateSectors();
    }
#endif

    private void GenerateSectors()
    {
        if (sectorMaterial == null)
        {
            // fallback so it doesn't crash; you can replace this with your own material
            sectorMaterial = new Material(Shader.Find("Unlit/Color"));
        }

        var palette = PaletteManager.Instance;
        if (palette == null) return;

        // X, Y, Z colors from palette
        Color[] colors = new Color[]
        {
            palette.XColor,
            palette.YColor,
            palette.ZColor
        };

        float angleStep = 360f / sectors;

        for (int i = 0; i < sectors; i++)
        {
            float a0 = Mathf.Deg2Rad * (i * angleStep);
            float a1 = Mathf.Deg2Rad * ((i + 1) * angleStep);

            // create child
            GameObject sector = new GameObject("Sector_" + i);
            sector.transform.SetParent(transform, false);

            var mf = sector.AddComponent<MeshFilter>();
            var mr = sector.AddComponent<MeshRenderer>();

            // simple triangle (center + two outer points)
            Mesh m = new Mesh();
            Vector3[] verts = new Vector3[3];
            int[] tris = new int[3];

            verts[0] = new Vector3(0f, 0f, 1f); // center, slightly behind line renderer
            verts[1] = new Vector3(Mathf.Cos(a0) * radius, Mathf.Sin(a0) * radius, 1f);
            verts[2] = new Vector3(Mathf.Cos(a1) * radius, Mathf.Sin(a1) * radius, 1f);

            tris[0] = 0;
            tris[1] = 1;
            tris[2] = 2;

            m.vertices = verts;
            m.triangles = tris;
            m.RecalculateNormals();
            m.RecalculateBounds();
            mf.mesh = m;

            // material & color
            mr.sharedMaterial = sectorMaterial;

            // give each sector its own tint without duplicating the material
            var mpb = new MaterialPropertyBlock();
            Color c = colors[Mathf.Clamp(i, 0, colors.Length - 1)];
            mpb.SetColor("_Color", c);       // built-in Unlit/Color
            mpb.SetColor("_BaseColor", c);   // URP/Lit, URP/Unlit etc.
            mr.SetPropertyBlock(mpb);
        }
    }

    private void ClearChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
