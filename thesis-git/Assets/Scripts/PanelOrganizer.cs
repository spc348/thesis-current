using UnityEngine;
using System.Collections;

public class PanelOrganizer : MonoBehaviour
{
    public GameObject[] panels, lives, subpanels, audiotriggers, markers;
    public GameObject GameText, panel0, panel1, panel2, panel3, panel4, panel5, player;
    public GameObject health0, health1, health2;
    public int activePanel, startPanel, endPanel, gamePanel;
    public CourseScript courseScript;
    public Camera mainCamera;
    public GameObject gamepanel_subpanel1, gamepanel_subpanel2, gamepanel_subpanel3, gamepanel_subpanel4, gamepanel_subpanel5, gamepanel_subpanel6, gamepanel_subopanel7;


    void Start()
    {
        mainCamera = Camera.main;
        endPanel = 6;
        gamePanel = 5;
        courseScript = GameObject.FindGameObjectWithTag("CoursePlanner").GetComponent<CourseScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        activePanel = 0;
        main_panel();
        game_subpanel();
    }



    void main_panel()
    {
        panels = new GameObject[7];

        panels[0] = panel0;
        panels[1] = panel1;
        panels[2] = panel2;
        panels[3] = panel3;
        panels[4] = panel4;
        panels[5] = GameText;
        panels[6] = panel5;

    }

    void game_subpanel()
    {
        subpanels = new GameObject[courseScript.courseLength];

        subpanels[0] = gamepanel_subpanel1;
        subpanels[1] = gamepanel_subpanel2;
        subpanels[2] = gamepanel_subpanel3;
        subpanels[3] = gamepanel_subpanel4;
        subpanels[4] = gamepanel_subpanel5;
        subpanels[5] = gamepanel_subpanel6;
        subpanels[6] = gamepanel_subopanel7;

        subpanels[0].SetActive(false);
        subpanels[1].SetActive(false);
        subpanels[2].SetActive(false);
        subpanels[3].SetActive(false);
        subpanels[4].SetActive(false);
        subpanels[5].SetActive(false);
        subpanels[6].SetActive(false);
    }

    public void clear_subpanels()
    {
        for (int i = 0; i < courseScript.courseLength; i++)
        {
            subpanels[i].SetActive(false);
        }
    }

    public void init_game_subpanel(int panel)
    {
        clear_subpanels();

        subpanels[panel].SetActive(true);

    }

    public void restart()
    {
        foreach (var i in markers)
        {
            Destroy(i);
        }
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i].SetActive(true);
        }
        //init_game_subpanel(1);
        SetPanel(startPanel);
    }

    public void draw_lives_left()
    {
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i].SetActive(true);
        }
    }

    public void GameStart()
    {

        lives = new GameObject[GameManager.life];
        lives[0] = health0;
        lives[1] = health1;
        lives[2] = health2;

        draw_lives_left();

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }

    }

    public void AddLife()
    {
        GameManager.life++;
    }

    public void SubLife()
    {
        GameManager.life--;
        lives[GameManager.life].SetActive(false);
    }

    public void SetPanel(int active)
    {
        activePanel = active;
        ClearPanels(active);
    }

    void ClearPanels(int active)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i != active)
            {
                panels[i].SetActive(false);
            }
        }
    }

    void Update()
    {
        if (!panels[activePanel].activeSelf)
            panels[activePanel].SetActive(true);

        if (GameManager.life == 0)
            SetPanel(endPanel);

        if (activePanel != gamePanel)
        {
            mainCamera.enabled = false;
            player.GetComponent<Collider>().enabled = false;

        }
        else
        {
            player.GetComponent<Collider>().enabled = true;
            mainCamera.enabled = true;
        }

		if(Input.GetKeyDown (KeyCode.Space))
		{
			clear_subpanels();
		}
    }

    public void atFinal()
    {
        SetPanel(endPanel);
    }
}
