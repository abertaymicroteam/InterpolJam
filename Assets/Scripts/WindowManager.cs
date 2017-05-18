using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour {


    struct CharacterPortrait
    {
        public Rect character;
        public Vector2 startingPosition;
        public bool pressed;
        public int jobLevel;
        public int wantedLevel;
        public float timer;
        public CharacterPortrait(Rect pos)
        {
            startingPosition = new Vector2(pos.x,pos.y);
            character = pos;
            pressed = false;
            jobLevel = 0;
            wantedLevel = 0;
            timer = 0.0f;
        }
    }

    struct Continent
    {
        public string name;
        public Rect holder;
        public Rect window;
        public Rect[] Slots;
        public CharacterPortrait[] Workers;
        public int easyWorkers;
        public int mediumWorkers;
        public int hardWorkers;

        

        public Continent(Rect pos, string name_)
        {
            Slots = new Rect[4];
            Workers = new CharacterPortrait[4];

            holder = new Rect(0, 0, 0, 0);
            window = pos;

            name = name_;

            easyWorkers = 0;
            mediumWorkers = 0;
            hardWorkers = 0;

            Slots[0] = new Rect(pos.width / 3, 0, pos.width * 0.6f, 0);
            Slots[1] = new Rect(pos.width / 3, 0, pos.width * 0.6f, pos.height / 3);
            Slots[2] = new Rect(pos.width / 3, 0, pos.width * 0.6f, (pos.height / 3) * 2);
            Slots[3] = new Rect(0, 0, 0, 0);

            Workers[0] = new CharacterPortrait(new Rect(10, 40, 40, 40));
            Workers[1] = new CharacterPortrait(new Rect(60, 40, 40, 40));
            Workers[2] = new CharacterPortrait(new Rect(10, 90, 40, 40));
            Workers[3] = new CharacterPortrait(new Rect(60, 90, 40, 40));

        }

        public void updateWindows()
        {
            if (Slots != null) { 
            for (int i = 0; i < 3; i++)
            {
                Slots[i].width = (window.width / 3) * 2;
                Slots[i].height = (window.height - 20) / 3;
                Slots[i].x = window.width / 3;
                Slots[i].y = ((window.height - 20) / 3 * i) + 20;
            }
            Slots[3].width = (window.width / 3);
            Slots[3].height = (window.height - 60);
            Slots[3].x = 0;
            Slots[3].y = 20;
            }
        }
    }

    Rect[] Windows = new Rect[8];
    public Rect Notifications, NorthAmerica, SouthAmerica, Europe, Africa, Asia, Australia;
    Continent[] Continents = new Continent[8];
    public int showWindow;
    CharacterPortrait[] AmeriChar = new CharacterPortrait[4];
    public gameManager gameMan;
    public bool GUIopen;
    public bool investigating;
    public Vector2 suspect;
    [SerializeField]
    public Contract[,] contracts = new Contract[4,6];
    public Rect[] boxes;
    public Notification News;
    public NewsScript NewsPaper;
    public float investigationTimer;
    public int option;
    public int Reds;
    public GUIStyle[,] gStyle;
    public Texture2D red, blue;
    public GameObject gameover;

    public void Start() {
        GUIStyle[,] gStyle = new GUIStyle[4,6];

   
        Reds = 0;

        Rect[] boxes = new Rect[4];

        //initialise continents
        Continents[2] = new Continent(new Rect(0,0,0,0), "North America");
        Continents[3] = new Continent(new Rect(0, 0, 0, 0), "South America");
        Continents[4] = new Continent(new Rect(0, 0, 0, 0), "Europe");
        Continents[5] = new Continent(new Rect(0, 0, 0, 0), "Africa");
        Continents[6] = new Continent(new Rect(0, 0, 0, 0), "Asia");
        Continents[7] = new Continent(new Rect(0, 0, 0, 0), "Oceania");

        boxes[0] = new Rect(0, 0, 0, 0);
        boxes[1] = new Rect(0, 0, 0, 0);
        boxes[2] = new Rect(0, 0, 0, 0);
        boxes[3] = new Rect(0, 0, 0, 0);

        //initialise contracts
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                contracts[i, j] = this.gameObject.AddComponent<Contract>();
            }
        }

        Windows[1] = Notifications;
        showWindow = 0;
        suspect = new Vector2(0, 0);
        GUIopen = false;
        investigating = false;
        Screen.SetResolution(1280, 720, true);
    }
    public void Update() 
    {
        
        if(Reds > 5)
        {
            gameover.SetActive(true);
        }

        for (int i = 0; i < Continents.Length; i++)
        {
            Continents[i].window.width = Screen.width * 0.5f;
            Continents[i].window.height = (Screen.height / 9) * 4;
            Continents[i].window.x = (Screen.width - Continents[i].window.width) / 2;
            Continents[i].window.y = ((Screen.height - Continents[i].window.height) / 2) - (Screen.height / 9);
            
        }

        Windows[1].width = Screen.width * 0.5f;
        Windows[1].height = (Screen.height / 9) * 4;
        Windows[1].x = (Screen.width - Windows[1].width) / 2;
        Windows[1].y = ((Screen.height - Windows[1].height) / 2) - (Screen.height / 9);


        investigation();

        if(investigationTimer> 30)
        {
            investigating = false;
            investigationTimer = 0.0f;
        }


    }

    void OnGUI()
    {
        if (showWindow > 1)
        {
            switch (showWindow) {
                case 1: 
                    break;
                case 2: Windows[showWindow] = GUI.Window(2, Continents[showWindow].window, DoMyWindow, Continents[showWindow].name);
                    break;
                case 3: Windows[showWindow] = GUI.Window(3, Continents[showWindow].window, DoMyWindow, Continents[showWindow].name);
                    break;
                case 4: Windows[showWindow] = GUI.Window(4, Continents[showWindow].window, DoMyWindow, Continents[showWindow].name);
                    break;
                case 5: Windows[showWindow] = GUI.Window(5, Continents[showWindow].window, DoMyWindow, Continents[showWindow].name);
                    break;
                case 6: Windows[showWindow] = GUI.Window(6, Continents[showWindow].window, DoMyWindow, Continents[showWindow].name);
                    break;
                case 7: Windows[showWindow] = GUI.Window(7, Continents[showWindow].window, DoMyWindow, Continents[showWindow].name);
                    break;
            }

            GUIopen = true;      
        }
        else if (showWindow == 0){
            GUIopen = false;
        }

       
    }

    void DoMyWindow(int windowID)
    {

        
          
        

        if (GUI.Button(new Rect(100, Continents[showWindow].window.height - 30, 100, 20), "Dismiss"))
        {
            showWindow = 0;
        }

        GUI.Box(new Rect( Continents[showWindow].window.width / 3, 20  , (Continents[showWindow].window.width / 3) * 2, (Continents[showWindow].window.height - 20) / 3), "Easy");
        GUI.Box(new Rect(Continents[showWindow].window.width / 3, (((Continents[showWindow].window.height) - 20) / 3 ) + 20, (Continents[showWindow].window.width / 3) * 2, (Continents[showWindow].window.height - 20) / 3), "Medium");
        GUI.Box(new Rect(Continents[showWindow].window.width / 3, (((Continents[showWindow].window.height) - 20) / 3 * 2) + 20, (Continents[showWindow].window.width / 3) * 2, (Continents[showWindow].window.height - 20) / 3), "Hard");
        GUI.Box(new Rect(0,20,Continents[showWindow].window.width/3, Continents[showWindow].window.height -60), "Workers");

        for ( int i = 0; i < 4; i++)
        {
            if (Continents[showWindow].Workers != null)
            {

               
                if (contracts[i, showWindow-2].wantedLevel == 2)
                {
                    switch (Continents[showWindow].Workers[i].jobLevel)
                    {
                        case 1:
                            Continents[showWindow].easyWorkers--;
                            break;
                        case 2:
                            Continents[showWindow].mediumWorkers--;
                            break;
                        case 3:
                            Continents[showWindow].hardWorkers--;
                            break;
                    }
                    Continents[showWindow].Workers[i].jobLevel = 0;
                    contracts[i, showWindow - 2].jobLevel = 0;
                    Continents[showWindow].Workers[i].character.x = Continents[showWindow].Workers[i].startingPosition.x;
                    Continents[showWindow].Workers[i].character.y = Continents[showWindow].Workers[i].startingPosition.y;
                    GUI.color = Color.red;
                }
                if (contracts[i, showWindow - 2].wantedLevel == 1)
                {
                    GUI.color = Color.blue;
                }

                if (contracts[i, showWindow - 2].wantedLevel < 2)
                {
                    if (Continents[showWindow].Workers[i].character.Contains(Event.current.mousePosition))
                    {

                        if (Event.current.type == EventType.MouseDown)
                        {
                            if (contracts[i, showWindow-2].wantedLevel != 2)
                            {
                                Continents[showWindow].holder = Continents[showWindow].Workers[i].character;
                                Continents[showWindow].Workers[i].pressed = true;

                                switch (Continents[showWindow].Workers[i].jobLevel)
                                {
                                    case 1:
                                        Continents[showWindow].Workers[i].jobLevel = 0;
                                        Continents[showWindow].easyWorkers--;
                                        break;
                                    case 2:
                                        Continents[showWindow].Workers[i].jobLevel = 0;
                                        Continents[showWindow].mediumWorkers--;
                                        break;
                                    case 3:
                                        Continents[showWindow].Workers[i].jobLevel = 0;
                                        Continents[showWindow].hardWorkers--;
                                        break;
                                }

                                contracts[i, showWindow - 2].jobLevel = 0;
                            }

                        }

                        if (Event.current.type == EventType.MouseUp)
                        {
                            Continents[showWindow].Workers[i].pressed = false;

                            if (Continents[showWindow].Slots[0].Contains(Event.current.mousePosition) && gameMan.money >= 250.0f)
                            {
                                Continents[showWindow].Workers[i].character.y = (Continents[showWindow].Slots[0].y + Continents[showWindow].Slots[0].height / 2) - Continents[showWindow].Workers[i].character.height / 2;
                                Continents[showWindow].easyWorkers++;
                                Continents[showWindow].Workers[i].jobLevel = 1;
                                contracts[i, showWindow - 2].jobLevel = 1;
                            }
                            else if (Continents[showWindow].Slots[0].Contains(Event.current.mousePosition) && gameMan.money < 250.0f)
                            {
                                gameMan.bankruptcyEntered();
                                Continents[showWindow].Workers[i].character.x = Continents[showWindow].Workers[i].startingPosition.x;
                                Continents[showWindow].Workers[i].character.y = Continents[showWindow].Workers[i].startingPosition.y;
                            }
                            else if (Continents[showWindow].Slots[1].Contains(Event.current.mousePosition) && gameMan.money >= 500.0f)
                            {
                                Continents[showWindow].Workers[i].character.y = (Continents[showWindow].Slots[1].y + Continents[showWindow].Slots[1].height / 2) - Continents[showWindow].Workers[i].character.height / 2;
                                Continents[showWindow].mediumWorkers++;
                                Continents[showWindow].Workers[i].jobLevel = 2;
                                contracts[i, showWindow - 2].jobLevel = 2;
                            }
                            else if (Continents[showWindow].Slots[1].Contains(Event.current.mousePosition) && gameMan.money < 500.0f)
                            {
                                gameMan.bankruptcyEntered();
                                Continents[showWindow].Workers[i].character.x = Continents[showWindow].Workers[i].startingPosition.x;
                                Continents[showWindow].Workers[i].character.y = Continents[showWindow].Workers[i].startingPosition.y;
                            }
                            else if (Continents[showWindow].Slots[2].Contains(Event.current.mousePosition) && gameMan.money >= 1000.0f)
                            {
                                Continents[showWindow].Workers[i].character.y = (Continents[showWindow].Slots[2].y + Continents[showWindow].Slots[2].height / 2) - Continents[showWindow].Workers[i].character.height / 2;
                                Continents[showWindow].hardWorkers++;
                                Continents[showWindow].Workers[i].jobLevel = 3;
                                contracts[i, showWindow - 2].jobLevel = 3;
                            }
                            else if (Continents[showWindow].Slots[2].Contains(Event.current.mousePosition) && gameMan.money < 1000.0f)
                            {
                                gameMan.bankruptcyEntered();
                                Continents[showWindow].Workers[i].character.x = Continents[showWindow].Workers[i].startingPosition.x;
                                Continents[showWindow].Workers[i].character.y = Continents[showWindow].Workers[i].startingPosition.y;
                            }
                            else if (Continents[showWindow].Slots[3].Contains(Event.current.mousePosition))
                            {
                                Continents[showWindow].Workers[i].character.x = Continents[showWindow].Workers[i].startingPosition.x;
                                Continents[showWindow].Workers[i].character.y = Continents[showWindow].Workers[i].startingPosition.y;
                            }
                            else
                            {
                                Continents[showWindow].Workers[i].character.x = Continents[showWindow].Workers[i].startingPosition.x;
                                Continents[showWindow].Workers[i].character.y = Continents[showWindow].Workers[i].startingPosition.y;
                            }
                        }
                    }
                    if (Continents[showWindow].Workers[i].pressed && Event.current.type == EventType.MouseDrag)
                    {
                        if (contracts[i, showWindow-2].wantedLevel != 2)
                        {
                            Continents[showWindow].Workers[i].character.x += Event.current.delta.x;
                            Continents[showWindow].Workers[i].character.y += Event.current.delta.y;
                        }
                    }
                }
            }
            Continents[showWindow].updateWindows();
            if (Continents[showWindow].Workers != null)
            {
                GUI.Button(Continents[showWindow].Workers[i].character, "Char");
            }
            GUI.color = Color.white;
        }
    }

    public void chooseWindow(int id)
    {
        showWindow = id;
    }

    public int getEasy(int continent)
    {
        return Continents[continent+2].easyWorkers;
    }

    public int getMedium(int continent)
    {
        return Continents[continent+2].mediumWorkers;
    }

    public int getHard(int continent)
    {     
        return Continents[continent+2].hardWorkers;
    }

    public void investigation()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                   
                    //increment timer
                    if (contracts[i,j].jobLevel != 0 && !investigating)
                    {
                        contracts[i, j].incrementTimer();
                    }
                    //set 2 zero if not on the job
                    else if (contracts[i, j].jobLevel == 0 && contracts[i, j].timer != 0)
                    {
                    contracts[i, j].timer = 0;
                    }

                    //roll dice if less than 20 investigate else reset timer
                    if (contracts[i, j].timer > 20.0f)
                    {
                        if(Random.Range(0,100) < 20)
                        {
                            //WHATS THE STORY 
                            News.show(Random.Range(1,4));
                            investigating = true;
                            showWindow = 1;
                            GUIopen = true;
                            suspect = new Vector2(i, j);
                            investigationTimer = 0.0f;
                            break;
                        }
                        else
                        {
                        contracts[i, j].timer = 0.0f;
                        }
                    }
                }
                if (investigating)
                {
                
                break;
                }
            if (investigating)
            {
                break;
            }
        }

        if (investigating)
        {
            investigationTimer += Time.deltaTime; 
        }
        if (investigationTimer > 15.0f && option != 0)
        {
            if (option == 1)
            {
                
                if (contracts[(int)suspect.x, (int)suspect.y].wantedLevel == 0) { 
                    if (Random.Range(0, 100) < 50)
                    {
                        contracts[(int)suspect.x, (int)suspect.y].wantedLevel = 1;
                        NewsEvent(1);
                    }
                    else
                    {
                        NewsEvent(3);
                    }

                }
                else if (contracts[(int)suspect.x, (int)suspect.y].wantedLevel == 1)
                {
                    if (Random.Range(0, 100) < 50)
                    {
                        contracts[(int)suspect.x, (int)suspect.y].wantedLevel = 2;
                        Reds++;
                        NewsEvent(2);
                    }
                    else
                    {
                        NewsEvent(3);
                    }

                }
                option = 0;
              
            }
            else if(option == 2)
            {
                
                if (contracts[(int)suspect.x, (int)suspect.y].wantedLevel == 0)
                {
                    if (Random.Range(0, 100) < 35)
                    {
                        contracts[(int)suspect.x, (int)suspect.y].wantedLevel = 2;
                        Reds++;
                        NewsEvent(2);
                    }
                    else
                    {
                        NewsEvent(3);
                    }
                }
                else if(contracts[(int)suspect.x, (int)suspect.y].wantedLevel == 1)
                {
                    contracts[(int)suspect.x, (int)suspect.y].wantedLevel = 2;
                    Reds++;
                    NewsEvent(2);
                }

                option = 0;

            }           
        }
        else if(investigationTimer > 20.0f && option == 0)
        {
            option = 0;
        }

    }

    public void OptionA()
    {
        News.dismiss();
        option = 1;
        investigationTimer = 0.0f;
        GUIopen = false;
    }

    public void OptionB()
    {
        News.dismiss();
        option = 2;
        investigationTimer = 0.0f;
        GUIopen = false;        
    }

    public void NewsEvent(int outcome)
    {
        NewsPaper.show(outcome);
        GUIopen = true;
        showWindow = 1;
        
    }

    public void DismissedNewsEvent()
    {
        investigating = false;
        GUIopen = false;
        investigationTimer = 0.0f;
    }
}
