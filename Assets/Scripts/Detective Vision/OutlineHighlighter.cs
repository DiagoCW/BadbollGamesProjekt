using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class OutlineHighlighter : MonoBehaviour
{
    [SerializeField] private Color outlineColor = Color.yellow;
    [SerializeField] private float outlineWidth = 0.03f;
    [SerializeField] private ClueType clueType = ClueType.Regular;
    public enum ClueType { Regular, Important, Critical, Hidden }

    private GameObject outlineObject;
    private Renderer outlineRenderer;
    private bool isHighlighted;

    void Awake()
    {
        CreateOutline();
        outlineObject.SetActive(false);
    }

    void CreateOutline()
    {

        if (outlineObject != null) return;

        outlineObject = new GameObject("Outline");
        outlineObject.transform.SetParent(transform);
        outlineObject.transform.localPosition = Vector3.zero;
        outlineObject.transform.localRotation = Quaternion.identity;
        outlineObject.transform.localScale = Vector3.one * (1f + outlineWidth);

        MeshFilter mf = GetComponent<MeshFilter>();
        MeshFilter outlineMF = outlineObject.AddComponent<MeshFilter>();
        outlineMF.sharedMesh = mf.sharedMesh;

        outlineRenderer = outlineObject.AddComponent<MeshRenderer>();
        outlineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        // Shader selection
        Shader outlineShader;
        if (GraphicsSettings.defaultRenderPipeline != null)
        {
            string pipelineType = GraphicsSettings.defaultRenderPipeline.GetType().Name;
            if (pipelineType.Contains("Universal"))
                outlineShader = Shader.Find("Universal Render Pipeline/Unlit");
            else
                outlineShader = Shader.Find("HDRP/Unlit");
        }
        else
        {
            outlineShader = Shader.Find("Unlit/Color");
        }

        if (outlineShader == null)
            outlineShader = Shader.Find("Hidden/InternalErrorShader");

        Material outlineMat = new Material(outlineShader);

        
        Color hdrColor = outlineColor;
        hdrColor *= 2.2f;                           

        outlineMat.color = hdrColor;
        outlineMat.SetColor("_BaseColor", hdrColor);

        outlineMat.EnableKeyword("_EMISSION");
        outlineMat.SetColor("_EmissionColor", hdrColor * 1.1f);
        outlineMat.SetFloat("_EmissionScale", 1f);

        outlineMat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

        
        outlineMat.SetInt("_ZWrite", 0);
        outlineMat.SetInt("_Cull", (int)CullMode.Front);
        outlineMat.SetInt("_ZTest", (int)CompareFunction.Always);
        outlineMat.renderQueue = 3000;

        outlineRenderer.material = outlineMat;
    }

    void OnDestroy()
    {
        if (outlineObject != null)
            DestroyImmediate(outlineObject);
    }

    public void SetHighlighted(bool value, float duration)
    {
        if (value == isHighlighted) return;

        isHighlighted = value;

        if (isHighlighted)
        {
            CreateOutline();
            outlineObject.SetActive(true);

            if (duration > 0f)
            {
                CancelInvoke(nameof(TurnOff));
                Invoke(nameof(TurnOff), duration);
            }
        }
        else
        {
            TurnOff();
        }
    }

    private void TurnOff()
    {
        if (outlineObject != null)
            outlineObject.SetActive(false);
        isHighlighted = false;
    }
}