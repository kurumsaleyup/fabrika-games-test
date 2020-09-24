
using UnityEngine;
using UnityEditor.UI;

public class LinesDrawer : MonoBehaviour
{

    public GameObject linePrefab;
    GameObject tier;
    //public LayerMask cantDrawOverLayer;
    int cantDrawOverLayerIndex;

    [Space(30f)]
    public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;
    public bool isDrawing = false;

    public GameObject cylinderPrefab;
    public Transform trasnformZaa;


    //[SerializeField]
    //public float minX = -3f, maxX = 1.6f, minY = -1.1f, maxY = 0.4f;

    public Line currentLine;

    Camera cam;


    void Start()
    {
        cam = Camera.main;
        //cantDrawOverLayerIndex = LayerMask.NameToLayer ( "CantDrawOver" );
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
            BeginDraw();

        if (currentLine != null && isDrawing)
            Draw();

        if (Input.GetMouseButtonUp(0))
            EndDraw();


    }

    // Begin Draw ----------------------------------------------
    void BeginDraw()
    {
        if (currentLine == null)
        {
            currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();
        }
        currentLine.ResetLine();


        //if (this.transform.position != Vector3.zero)
        //{
        //    Debug.Log("Transform 0 değil");
        //    this.transform.position = Vector3.zero;
        //}

        //Set line properties
        currentLine.UsePhysics(false);
        currentLine.SetLineColor(lineColor);
        currentLine.SetPointsMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);
        isDrawing = true;


    }
    // Draw ----------------------------------------------------
    void Draw()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        currentLine.AddPoint(mousePosition);
    }
    // End Draw ------------------------------------------------

    void EndDraw()
    {
        if (currentLine != null)
        {
            if (currentLine.pointsCount < 2)
            {
                currentLine.ResetLine();
            }
            else
            {

                //teker oluşturuyom
                Vector2 TransVecLAST, TransVecFIRST;
                TransVecLAST = currentLine.GetLastPoint();
                TransVecFIRST = currentLine.GetFirstPoint();//transformzaa
                trasnformZaa.position = new Vector3(TransVecFIRST.x, TransVecFIRST.y, 0);
                currentLine.transform.GetChild(0).position = new Vector3(TransVecFIRST.x, TransVecFIRST.y, 0);
                currentLine.transform.GetChild(1).position = new Vector3(TransVecLAST.x, TransVecLAST.y, 0);
                //teker oluşturdum

                currentLine.UsePhysics(true);
                isDrawing = false;

            }
        }
    }

}





//