using UnityEngine;

public class CatMintThrowerTool : MonoBehaviour, ITool
{

    public GameObject mintPrefab;
    public Transform throwPoint;
    public float testThrowForce;
    private GameObject currentMint;
    public Sprite toolSprite;
    private LineRenderer lRenderer;
    [Min(3)]
    public int linePoints;
    [Min(1)]
    public float predictionLength;
    [Min(1)]
    public float lineCompensation;

    private Vector3 dir;
    private float ballMass;
    private void Start()
    {
        lRenderer = GetComponent<LineRenderer>();
        ballMass = mintPrefab.GetComponent<Rigidbody>().mass;
    }

    private void Update()
    {
        Passive();
    }
    public Sprite GetImage()
    {
        return toolSprite;
    }
    public void Main()
    {
        if(currentMint == null)
        {
            Rigidbody rb; 
            currentMint = Instantiate(mintPrefab,throwPoint.position,throwPoint.rotation);
            rb = currentMint.GetComponent<Rigidbody>();
            if(ballMass != rb.mass)
            {
                ballMass = rb.mass;
            }
            rb.AddForce(dir * testThrowForce);

        }
    }

    public void Passive()
    {
        //dir = (Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)).GetPoint(5) - throwPoint.position).normalized;
        dir = throwPoint.forward;
        ShowTrajectoryLine(throwPoint.position,dir * testThrowForce / ballMass);
        
    }

    public void Secondary()
    {
         
    }

    public void UpMain()
    {
         
    }

    public void UpSecondary()
    {
         
    }

    private void ShowTrajectoryLine(Vector3 startpoint, Vector3 startVelocity)
    {
        float timeStep = predictionLength / linePoints;

        Vector3[] lineRendererPoints = CalculateTrajectory(startpoint,startVelocity/lineCompensation,timeStep);
        //float comp = testThrowForce / 9f;
        //Vector3[] lineRendererPoints = CalculateTrajectory(startpoint,startVelocity/comp,timeStep);

        lRenderer.positionCount = linePoints;
        lRenderer.SetPositions(lineRendererPoints);
    }

    private Vector3[] CalculateTrajectory(Vector3 startpoint, Vector3 startVelocity, float timeStep)
    {
        Vector3[] lineRendererPoints = new Vector3[linePoints];
        lineRendererPoints[0] = throwPoint.position;
        for (int i = 1; i < linePoints; i++)
        {
            float timeOffset = timeStep * i;
            Vector3 progressBeforeGravity = startVelocity * timeOffset;
            Vector3 gravityOffset = Vector3.up * -0.5f * Physics.gravity.y * timeOffset * timeOffset;
            Vector3 newPosition = startpoint + progressBeforeGravity - gravityOffset;
            lineRendererPoints[i] = newPosition;
        }
        return lineRendererPoints;
    }

    public void OnEquip()
    {

    }

    public void OnUnequip()
    {
        
    }
}
