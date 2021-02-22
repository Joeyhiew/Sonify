using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shape : MonoBehaviour
{
    public Text ratioText;
    public Text distanceText;
    EllipseRenderer ellipse;
    EllipseRenderer ellipse1;
    EllipseRenderer ellipse2;
    PlaneRenderer plane;

    public ModeManager modeManager;

    [SerializeField]
    private AudioManager audioManager;

    [SerializeField]
    private GameObject EllipsePrefab;

    [SerializeField]
    private GameObject PlanePrefab;

    public Rfunction rFunctions;

    private float volBoundInner = 7f;
    private float volBoundOuter = 10f;

    private List<GameObject> shapeList = new List<GameObject>();

    Vector3 Origin = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        rFunctions = new Rfunction();


        //
        //--------------Ellipse + Rectangle -----------
        //
/*        ellipse = Instantiate(EllipsePrefab, Origin, Quaternion.identity).GetComponent<EllipseRenderer>();
        //ellipse = GameObject.FindObjectOfType<EllipseRenderer>();
        ellipse.InitializeEllipse(new Ellipse(2, 3, 0, 0, 2));

        plane = Instantiate(PlanePrefab, Origin, Quaternion.identity).GetComponent<PlaneRenderer>();
        //plane = GameObject.FindObjectOfType<PlaneRenderer>();

        plane.InitializePlaneLine(new Lines(1, -3.5f, 1));
        plane.InitializePlaneLine(new Lines(-1, 1, 0));
        plane.InitializePlaneLine(new Lines(-1, 5, 1));
        plane.InitializePlaneLine(new Lines(1, 0, 0));

        plane.GeneratePlane();*/

        //
        //--------------2 Identical Circles -----------
        //
/*        ellipse1 = Instantiate(EllipsePrefab).GetComponent<EllipseRenderer>();
        ellipse2 = Instantiate(EllipsePrefab).GetComponent<EllipseRenderer>();
        ellipse1.InitializeEllipse(new Ellipse(1, 1, -3, 0, 1));
        ellipse2.InitializeEllipse(new Ellipse(1, 1, 3, 0, 1));*/

        //
        //--------------1 Big and 1 Small Circle -----------
        //
        GameObject e1 = (GameObject)Instantiate(EllipsePrefab);
        ellipse1 = e1.GetComponent<EllipseRenderer>();
        GameObject e2 = (GameObject)Instantiate(EllipsePrefab);
        ellipse2 = e2.GetComponent<EllipseRenderer>();
        //ellipse1 = Instantiate(EllipsePrefab).GetComponent<EllipseRenderer>();
        //ellipse2 = Instantiate(EllipsePrefab).GetComponent<EllipseRenderer>();
        ellipse1.InitializeEllipse(new Ellipse(1, 1, -6, 0, 1));
        ellipse2.InitializeEllipse(new Ellipse(1, 1, 10, 0, 10));
        shapeList.Add(e1);
        shapeList.Add(e2);
    }

    void Update()
    {
        //
        //--------------Ellipse + Rectangle -----------
        //
/*        float ellipseFreqRatio = ellipse.updateMousePosition();
        float planeFreqRatio = plane.updateMousePosition();
        float finalRatio = Mathf.Max(ellipseFreqRatio, planeFreqRatio);
        //float finalRatio = rFunctions.Union(ellipseFreqRatio, planeFreqRatio);
        audioManager.updateFrequency((finalRatio/10));*/

        //
        //--------------2 Identical Circles -----------
        //
        float ellipseFreqRatio1 = ellipse1.updateMousePosition();
        float ellipseFreqRatio2 = ellipse2.updateMousePosition();
        float finalRatio = Mathf.Max(ellipseFreqRatio1, ellipseFreqRatio2);
        ratioText.text = "Value: " + ((float)Math.Round(finalRatio * 100f) / 100f).ToString();
        distanceText.text = "Dist: " + ((float)Math.Round(volBoundOuter * 100f) / 100f).ToString();
        
        //print(volBoundOuter);
        //float finalRatio = ellipseFreqRatio1;
        //float finalRatio = rFunctions.Union(ellipseFreqRatio, planeFreqRatio);
        

        if (modeManager.modeIndex == 0)
        {
            audioManager.updateFrequency(((finalRatio)) / 100);
            audioManager.Vol = 1f;
        }
        else if (modeManager.modeIndex == 1)
        {
            audioManager.updateFrequency(((finalRatio))/volBoundOuter);
            /// to only play sounds after a distance
            if (Mathf.Abs(finalRatio) > volBoundOuter)
            {
                audioManager.Vol = 0;
            }
            else
            {
                audioManager.Vol = 1f;
                /*            if (Mathf.Abs(finalRatio) > volBoundInner)
                            {
                                float volRatio = (Mathf.Abs(finalRatio) - volBoundInner) / (volBoundOuter - volBoundInner);
                                audioManager.Vol = Mathf.Lerp(1, 0, volRatio);
                            }
                            else
                            {
                                audioManager.Vol = 1f;
                            }*/
            }

            AdjustDistance();
            AdjustFrequency();
        }
        // Low and continuous
        else if (modeManager.modeIndex == 2)
        {
            audioManager.updateFrequency(Mathf.Abs(finalRatio) / 100);
            audioManager.Vol = 1f;
            AdjustDistance();
            AdjustFrequency();
        }
        // Low and distance
        else if(modeManager.modeIndex == 3)
        {
            //audioManager.updateFrequency(Mathf.Abs(finalRatio) / volBoundOuter);
            /// to only play sounds after a distance
            print(finalRatio);
            if (Mathf.Abs(finalRatio) > volBoundOuter)
            {
                audioManager.Vol = 0;
            }
            else
            {
                audioManager.Vol = 1f;
                            if (Mathf.Abs(finalRatio) > volBoundInner)
                            {
                                audioManager.updateFrequency(Mathf.Abs(finalRatio) / volBoundOuter);
                                //float volRatio = (Mathf.Abs(finalRatio) - volBoundInner) / (volBoundOuter - volBoundInner);
                                //audioManager.Vol = Mathf.Lerp(1, 0, volRatio);
                            }
                            else
                            {
                                audioManager.updateFrequency(Mathf.Abs(finalRatio) / volBoundOuter);
                                //audioManager.Vol = 1f;
                            }
            }

            AdjustDistance();
            AdjustFrequency();
        }        

        // display
        if (modeManager.displayIndex == 0)
        {
            foreach (GameObject obj in shapeList)
            {
                obj.GetComponent<MeshRenderer>().material = null;
            }

        }
        // remove display
        else if (modeManager.displayIndex == 1)
        {
            foreach (GameObject obj in shapeList)
            {
                obj.GetComponent<MeshRenderer>().material.color = Color.black;
            }
        }
    }

    private void AdjustDistance()
    {
        if (Input.GetKey("left"))
        {
            volBoundInner = volBoundInner - 0.05f;
            volBoundOuter = volBoundOuter - 0.05f;
        }
        else if (Input.GetKey("right"))
        {
            volBoundInner = volBoundInner + 0.05f;
            volBoundOuter = volBoundOuter + 0.05f;
        }
    }

    private void AdjustFrequency()
    {
        if (Input.GetKeyUp("up"))
        {
            audioManager.IncreaseFrequencyRange();
        }
        else if (Input.GetKeyUp("down"))
        {
            audioManager.DecreaseFrequencyRange();
        }
    }

}
