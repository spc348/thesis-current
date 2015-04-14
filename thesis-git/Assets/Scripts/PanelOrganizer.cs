using System.Collections;
using UnityEngine;

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

    public GameObject PromptPanel1, PromptPanel2, PromptPanel3, PromptPanel4;
    public GameObject altPromptPanel1, altPromptPanel2, altPromptPanel3, altPromptPanel4;

    public static bool useTextInstruction;

    public static PanelOrganizer PanelsRef;

    public static bool UseSymbInstruction
    {
        get
        {
            return useTextInstruction;
        }
        set
        {
            useTextInstruction = value;
        }
    }

    void Awake()
    {
        PanelsRef = this;
    }

    void Start()
    {
        MainCamera = Camera.main;
        EndPanel = 6;
        GamePanel = 5;
        Player = GameObject.FindGameObjectWithTag("Player");
        ActivePanel = 0;
        LoadMainPanel();

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

    public void GameSubpanel()
    {
        Subpanels = new GameObject[PreferenceSelections.InstanceAttributes.CourseLength + 4];

        if (PreferenceSelections.InstanceAttributes.UseSymbols)
        {
            Subpanels[0] = altSubpanel1;
            Subpanels[1] = altSubpanel2;
            Subpanels[2] = altSubpanel3;
            Subpanels[3] = altSubpanel4;
            Subpanels[4] = altSubpanel5;
            Subpanels[5] = altSubpanel6;
            Subpanels[6] = altSubpanel7;
            Subpanels[7] = altPromptPanel1; // getting cold
            Subpanels[8] = altPromptPanel2; // turn around
            Subpanels[9] = altPromptPanel3; // jaywalking
            Subpanels[10] = altPromptPanel4; //reward
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
            Subpanels[7] = PromptPanel1; // getting cold
            Subpanels[8] = PromptPanel2; // turn around
            Subpanels[9] = PromptPanel3; // jaywalking
            Subpanels[10] = PromptPanel4; // reward
        }

        Subpanels[0].SetActive(false);
        Subpanels[1].SetActive(false);
        Subpanels[2].SetActive(false);
        Subpanels[3].SetActive(false);
        Subpanels[4].SetActive(false);
        Subpanels[5].SetActive(false);
        Subpanels[6].SetActive(false);
    }

    public void ClearSubpanels()
    {
        for (int i = 0; i < PreferenceSelections.InstanceAttributes.CourseLength + 4; i++)
        {
            Subpanels[i].SetActive(false);
        }
    }

    public void InitGameSubpanel(int panel)
    {
        ClearSubpanels();

        Subpanels[panel].SetActive(true);

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
        ClearPanels();
        ClearTokens();
    }

    public void AddToken()
    {
        bool flag = false;
        for (int i = 0; i < TokenArray.Length; i++)
        {
            if (!TokenArray[i].activeSelf && !flag)
            {
                TokenArray[i].SetActive(true);
                flag = true;
            }
        }
    }

    void ClearTokens()
    {
        for (int i = 0; i < TokenArray.Length; i++)
        {
            TokenArray[i].SetActive(false);
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
            Player.GetComponent<Collider>().enabled = true;
            MainCamera.enabled = true;
        }
        else
        {
            MainCamera.enabled = false;
            Player.GetComponent<Collider>().enabled = false;
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
