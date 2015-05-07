using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class PanelOrganizer : MonoBehaviour
{
    public GameObject[] Panels, TokenArray, Subpanels, Audiotriggers, Markers;
    public GameObject GameText, Panel0, Panel1, Panel2, Panel3, Panel4, Panel5, Panel6, Player;
    public GameObject Panel7, Panel8, Panel9;
    public GameObject Token1, Token2, Token3, Token4, Token5, Token6, Token7;
    public int ActivePanel, StartPanel, EndPanel, GamePanel;
    public Camera MainCamera;
    public GameObject GamepanelSubpanel1, GamepanelSubpanel2, GamepanelSubpanel3, GamepanelSubpanel4;
    public GameObject GamepanelSubpanel5, GamepanelSubpanel6, GamepanelSubpanel7;

    public GameObject altSubpanel1, altSubpanel2, altSubpanel3, altSubpanel4;
    public GameObject altSubpanel5, altSubpanel6, altSubpanel7;

    static bool useSymInstruction;
    private static float panelFade;
    public static PanelOrganizer PanelsRef;
    public GameObject[] UtilPanelsArray;
    public GameObject utilPanel1, utilPanel2, utilPanel3, utilPanel4, utilPanel5;

    public static float PanelFade
    {
        get
        {
            return panelFade;
        }
        set { panelFade = value; }
    }

    public static bool UseSymbInstruction
    {
        get
        {
            return useSymInstruction;
        }
        set
        {
            useSymInstruction = value;
        }
    }

    void Awake()
    {
        PanelsRef = this;
    }

    void Start()
    {
        panelFade = 2f;
        MainCamera = Camera.main;
        EndPanel = 6;
        GamePanel = 5;
        Player = GameObject.FindGameObjectWithTag("Player");
        ActivePanel = 0;
        LoadMainPanel();
        LoadTokens();
    }

    void LoadTokens()
    {
        TokenArray = new GameObject[CourseScript.CourseLength];
        TokenArray[0] = Token1;
        TokenArray[1] = Token2;
        TokenArray[2] = Token3;
        TokenArray[3] = Token4;
        TokenArray[4] = Token5;
        TokenArray[5] = Token6;
        TokenArray[6] = Token7;
    }

    void LoadMainPanel()
    {
        Panels = new GameObject[11];
        Panels[0] = Panel0;
        Panels[1] = Panel1;
        Panels[2] = Panel2;
        Panels[3] = Panel3;
        Panels[4] = Panel4;
        Panels[5] = GameText;
        Panels[6] = Panel5;
        Panels[7] = Panel6;
        Panels[8] = Panel7;
        Panels[9] = Panel8;
        Panels[10] = Panel9;
    }

    public void UtilPanels()
    {
        UtilPanelsArray = new GameObject[5];
        UtilPanelsArray[0] = utilPanel1;
        UtilPanelsArray[1] = utilPanel2;
        UtilPanelsArray[2] = utilPanel3;
        UtilPanelsArray[3] = utilPanel4;
        UtilPanelsArray[4] = utilPanel5;

        foreach (var i in UtilPanelsArray)
        {
            i.SetActive(false);
        }
    }

    public void ClearUtils()
    {
        foreach (var o in UtilPanelsArray)
        {
            o.SetActive(false);
        }
    }
    public void InitUtilPanel(int prompt)
    {
        //ClearUtils();
        bool subpanel = false;
        
        foreach (var o in Subpanels)
        {
            if (o.activeSelf) subpanel = true;
        }
        foreach (var o in UtilPanelsArray)
        {
            if (o.activeSelf) subpanel = true;
        }
        if (!subpanel)
        {
            UtilPanelsArray[prompt].SetActive(true);
        }

        //start at the bottom
        UtilPanelsArray[prompt].GetComponent<Image>().CrossFadeAlpha(0f, 0f, false);
        //UtilPanelsArray[prompt].GetComponentInChildren<Text>().CrossFadeAlpha(0f, 0f, false);
        //UtilPanelsArray[prompt].GetComponentInChildren<Image>().CrossFadeAlpha(0f, 0f, false);
        //work to the top
        UtilPanelsArray[prompt].GetComponent<Image>().CrossFadeAlpha(1f, 1f, false);
        //UtilPanelsArray[prompt].GetComponentInChildren<Text>().CrossFadeAlpha(1f, 1f, false);
        //UtilPanelsArray[prompt].GetComponentInChildren<Image>().CrossFadeAlpha(1f, 1f, false);
    }

    public void GameSubpanel()
    {
        Subpanels = new GameObject[PreferenceSelections.InstanceAttributes.CourseLength];

        if (useSymInstruction)
        {
            Subpanels[0] = altSubpanel1;
            Subpanels[1] = altSubpanel2;
            Subpanels[2] = altSubpanel3;
            Subpanels[3] = altSubpanel4;
            Subpanels[4] = altSubpanel5;
            Subpanels[5] = altSubpanel6;
            Subpanels[6] = altSubpanel7;
        }
        else
        {
            Subpanels[0] = GamepanelSubpanel1;
            Subpanels[1] = GamepanelSubpanel2;
            Subpanels[2] = GamepanelSubpanel3;
            Subpanels[3] = GamepanelSubpanel4;
            Subpanels[4] = GamepanelSubpanel5;
            Subpanels[5] = GamepanelSubpanel6;
            Subpanels[6] = GamepanelSubpanel7;
        }

        foreach (var o in Subpanels)
        {
            o.SetActive(false);
            o.GetComponent<Image>().CrossFadeAlpha(0f, 0f, false);
        }
    }

    public void ClearSubpanels()
    {
        foreach (var o in Subpanels)
            if (Subpanels != null) o.SetActive(false);
    }

    public void InitGameSubpanel(int panel)
    {
        ClearSubpanels();
        Subpanels[panel].SetActive(true);
        //start at the bottom
        Subpanels[panel].GetComponent<Image>().CrossFadeAlpha(0f, 0f, false);
        Subpanels[panel].GetComponentInChildren<Text>().CrossFadeAlpha(0f, 0f, false);
        Subpanels[panel].GetComponentInChildren<Image>().CrossFadeAlpha(0f, 0f, false);
        //work to the top
        Subpanels[panel].GetComponent<Image>().CrossFadeAlpha(1f, 1f, false);
        Subpanels[panel].GetComponentInChildren<Text>().CrossFadeAlpha(1f, 1f, false);
        Subpanels[panel].GetComponentInChildren<Image>().CrossFadeAlpha(1f, 1f, false);

    }

    public void Restart()
    {
        foreach (var i in Markers)
        {
            Destroy(i);
        }
        ClearTokens();
        SetPanel(StartPanel);
    }

    public void GameStart()
    {
        GameSubpanel();
        UtilPanels();
        ClearPanels();
        ClearTokens();
    }

    public void AddToken(int place)
    {
        TokenArray[place].SetActive(true);
    }

    void ClearTokens()
    {
        foreach (var t in TokenArray)
        {
            t.SetActive(false);
        }
    }

    public void SetPanel(int active)
    {
        ActivePanel = active;
        ClearPanelsExcept(active);
    }

    void ClearPanels()
    {

        for (int i = 0; i < Panels.Length; i++)
        {
            Panels[i].SetActive(false);
        }
    }

    void ClearPanelsExcept(int active)
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            if (i != active)
            {
                Panels[i].SetActive(false);
            }
        }
    }

    void Update()
    {
        if (!Panels[ActivePanel].activeSelf)
            Panels[ActivePanel].SetActive(true);

        if (ActivePanel == GamePanel)
        {
            //Player.GetComponent<Collider>().enabled = true;
            MainCamera.enabled = true;
        }
        else
        {
            MainCamera.enabled = false;
            //Player.GetComponent<Collider>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClearSubpanels();
        }
    }

    public void AtFinal()
    {
        SetPanel(EndPanel);
    }

    
}
