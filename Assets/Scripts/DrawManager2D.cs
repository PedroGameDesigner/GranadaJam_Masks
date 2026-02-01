using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class DrawManager2D : MonoBehaviour
{
    [Header("Refs")]
    public Camera cam;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Canvas dimensions")]
    public int totalXPixels = 1024;
    public int totalYPixels = 1024;

    [Header("Brush")]
    [SerializeField] private int brushSize = 4;
    [SerializeField] private Color brushColor = Color.black;

    [Header("Smoothing")]
    public bool useInterpolation = true;

    [Header("Bounds (local space)")]
    public Transform topLeftCorner;
    public Transform bottomRightCorner;
    public Transform point;

    [Header("Runtime")]
    public Texture2D generatedTexture;

    private Color[] colorMap;

    private int xPixel = 0;
    private int yPixel = 0;

    private bool pressedLastFrame = false;
    private int lastX = 0;
    private int lastY = 0;

    private float xMult;
    private float yMult;

    private InputSystem_Actions input;

    private MaterialPropertyBlock mpb;

    private bool startDetection = false;
    private int storageID;

    [SerializeField] private PlayerInputDetection playerInput;
    [SerializeField] private GameObject canvasControlls;
    [SerializeField] private GameObject Mask;

    private bool savedTexture = false;
    private void Awake()
    {
        input = new InputSystem_Actions();
        mpb = new MaterialPropertyBlock();
        SetCanvasVisible(false);

                
    }
    public void SetCanvasVisible(bool value)
    {
        canvasControlls.SetActive(value);

    }

    private void OnEnable()
    {
        input.Enable();
        playerInput.OnMaskCreated += InitDrawing;
    }

    private void OnDisable()
    {
        input.Disable();
        playerInput.OnMaskCreated -= InitDrawing;

    }

    //private void Start()
    //{
    //    InitDrawing();
    //}

    private void InitDrawing(int storageID)
    {
        this.storageID = storageID;
        SetCanvasVisible(true);
        InitializeTexture();
        Mask.transform.position = transform.localPosition= new Vector3(0f,0f,0f);

        startDetection = true;
        spriteRenderer.color = new Color(1f,1f,1f,1f);

        xMult = totalXPixels / (bottomRightCorner.localPosition.x - topLeftCorner.localPosition.x);
        yMult = totalYPixels / (bottomRightCorner.localPosition.y - topLeftCorner.localPosition.y);
    }

    public void HideSpriteRenderer()
    {
        //spriteRenderer.color = new Color(0f,0f,0f,0f);
    }


    private void Update() { 

         if(!startDetection) return;

    
        if (Mouse.current == null || cam == null) return;

        bool isHoldingClick = input.UI.Click.IsPressed();

        if (isHoldingClick)
            CalculatePixel2D();
        else
            pressedLastFrame = false;
    }
    public void SetSpriteTexture(SpriteRenderer sprite)
    {
        spriteRenderer = sprite;
    }


    public void InitializeTexture()
    {

        if (savedTexture) return;


        colorMap = new Color[totalXPixels * totalYPixels];

        
        generatedTexture = new Texture2D(totalXPixels, totalYPixels, TextureFormat.RGBA32, false);
        generatedTexture.filterMode = FilterMode.Point;

        ApplyTextureToSpriteRenderer();     // textura para ambos

        ResetColor();
    }

    private void ApplyTextureToSpriteRenderer()
    {
        if (spriteRenderer == null) return;

        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetTexture("_MainTex", generatedTexture);
        spriteRenderer.SetPropertyBlock(mpb);
    }





    private void CalculatePixel2D()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 world = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -cam.transform.position.z));

        RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);
        if (hit.collider != null )
        {
            
            point.position = hit.point;

            xPixel = (int)((point.localPosition.x - topLeftCorner.localPosition.x) * xMult);
            yPixel = (int)((point.localPosition.y - topLeftCorner.localPosition.y) * yMult);

            ChangePixelsAroundPoint();
        }
        else
        {
            pressedLastFrame = false;
        }
    }

    private void ChangePixelsAroundPoint()
    {
        if (useInterpolation && pressedLastFrame && (lastX != xPixel || lastY != yPixel))
        {
            int dx = xPixel - lastX;
            int dy = yPixel - lastY;
            int dist = (int)Mathf.Sqrt(dx * dx + dy * dy);

            for (int i = 1; i <= dist; i++)
            {
                int ix = (i * xPixel + (dist - i) * lastX) / dist;
                int iy = (i * yPixel + (dist - i) * lastY) / dist;
                DrawBrush(ix, iy);
            }
        }
        else
        {
            DrawBrush(xPixel, yPixel);
        }

        pressedLastFrame = true;
        lastX = xPixel;
        lastY = yPixel;

        SetTexture();
    }

    private void DrawBrush(int xPix, int yPix)
    {
        int i = xPix - brushSize + 1;
        int j = yPix - brushSize + 1;
        int maxi = xPix + brushSize - 1;
        int maxj = yPix + brushSize - 1;

        if (i < 0) i = 0;
        if (j < 0) j = 0;
        if (maxi >= totalXPixels) maxi = totalXPixels - 1;
        if (maxj >= totalYPixels) maxj = totalYPixels - 1;

        for (int x = i; x <= maxi; x++)
        {
            for (int y = j; y <= maxj; y++)
            {
                int dx = x - xPix;
                int dy = y - yPix;

                if (dx * dx + dy * dy <= brushSize * brushSize)
                    colorMap[x * totalYPixels + y] = brushColor;
            }
        }
    }

    private void SetTexture()
    {
        generatedTexture.SetPixels(colorMap);
        generatedTexture.Apply();
    }

    private void ResetColor()
    {
        Array.Fill(colorMap, Color.white);
        SetTexture();
    }

    // =====================
    // PUBLIC API (UI)
    // =====================

    public void SetColor(Color value)
    {
        brushColor = value;
        pressedLastFrame = false;
    }

    public void OnSliderChanged(float value)
    {
        brushSize = Mathf.Max(1, Mathf.RoundToInt(value));
    }

    public void ResetCanvas()
    {
        pressedLastFrame = false;
        lastX = lastY = 0;
        ResetColor();
    }

    private Texture2D RotateTexture(Texture2D originalTexture, bool clockwise)
    {
        Color32[] original = originalTexture.GetPixels32();
        Color32[] rotated = new Color32[original.Length];
        int w = originalTexture.width;
        int h = originalTexture.height;

        int iRotated, iOriginal;

        for (int j = 0; j < h; ++j)
        {
            for (int i = 0; i < w; ++i)
            {
                iRotated = (i + 1) * h - j - 1;
                iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
                rotated[iRotated] = original[iOriginal];
            }
        }

        Texture2D rotatedTexture = new Texture2D(h, w);
        rotatedTexture.SetPixels32(rotated);
        rotatedTexture.Apply();
        return rotatedTexture;
    }

    public void SaveTexture()
    {
       // var newTexture = RotateTexture(generatedTexture, true);
        FindFirstObjectByType<TextureManager>().SaveTexture(storageID, generatedTexture);


        //var newTexture = RotateTexture(generatedTexture, true);
        //byte[] bytes = newTexture.EncodeToPNG();
        //string path = Path.Combine(Application.persistentDataPath,
        //                            "Texture_" + DateTime.Now.Ticks + ".png");
        //Debug.Log("Saved at " + path);
        //File.WriteAllBytes(path, bytes);
    }

    public void FinishDrawing()
    {
        SetDetection(false);
        SaveTexture();
        MaskManager.Instance.SaveMask(storageID, spriteRenderer.transform.parent.gameObject, TextureManager.Instance.LoadTexture(storageID));
        SetCanvasVisible(false);
        savedTexture = true;
    }

    public void SetDetection(bool detection)
    {
        startDetection = detection;
    }

}
