using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour {


    struct CharacterPortrait
    {
        public Rect character;
        public bool pressed;
        public CharacterPortrait(Rect pos)
        {
            character = pos;
            pressed = false;
        }
    }

    struct Continent
    {
        public Rect window;
        public Rect[] Slots;
        public CharacterPortrait[] Workers;

        public Continent(Rect pos)
        {
            Slots = new Rect[3];
            Workers = new CharacterPortrait[4];

            window = pos;

            Slots[0] = new Rect(pos.width / 3, 0, pos.width * 0.6f, 0);
            Slots[1] = new Rect(pos.width / 3, 0, pos.width * 0.6f, pos.height / 3);
            Slots[2] = new Rect(pos.width / 3, 0, pos.width * 0.6f, (pos.height / 3) * 2);

            Workers[0] = new CharacterPortrait(new Rect(10, 30, 40, 40));
            Workers[1] = new CharacterPortrait(new Rect(60, 30, 40, 40));
            Workers[2] = new CharacterPortrait(new Rect(10, 80, 40, 40));
            Workers[3] = new CharacterPortrait(new Rect(60, 80, 40, 40));
        }

        public void updateWindows()
        {
            for(int i = 0; i < 3; i++)
            {
                Slots[i].width = (window.width/ 3) *2;
                Slots[i].height = (window.height-20) / 3;
                Slots[i].x = window.width /3;
                Slots[i].y = ((window.height-20) /3 * i)+20;
            }
        }

    }

    Rect[] Windows = new Rect[8];
    public Rect Notifications, NorthAmerica, SouthAmerica, Europe, Africa, Asia, Australia;
    Continent[] Continents = new Continent[8];
    public int showWindow;
    CharacterPortrait[] AmeriChar = new CharacterPortrait[4];

    public void Start() {

        Continents[2] = new Continent(new Rect(0,0,0,0));
        Continents[3] = new Continent(new Rect(0, 0, 0, 0));
        Continents[4] = new Continent(new Rect(0, 0, 0, 0));
        Continents[5] = new Continent(new Rect(0, 0, 0, 0));
        Continents[6] = new Continent(new Rect(0, 0, 0, 0));
        Continents[7] = new Continent(new Rect(0, 0, 0, 0));
        Windows[1] = Notifications;
   
        showWindow = 0;


        Screen.SetResolution(1280, 720, true);
    }
    public void Update() 
    {

        for(int i = 0; i < Continents.Length; i++)
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
    }

    void OnGUI()
    {
        if (showWindow != 0)
        {
            switch (showWindow){
                case 1: Windows[showWindow] = GUI.Window(1, Windows[showWindow], Character, "Notifications");
                    break;
                case 2: Windows[showWindow] = GUI.Window(2, Continents[showWindow].window, DoMyWindow, "NorthAmerica");
                    break;
                case 3: Windows[showWindow] = GUI.Window(3, Continents[showWindow].window, DoMyWindow, "SouthAmerica");
                    break;
                case 4: Windows[showWindow] = GUI.Window(4, Continents[showWindow].window, DoMyWindow, "Europe");
                    break;
                case 5: Windows[showWindow] = GUI.Window(5, Continents[showWindow].window, DoMyWindow, "Africa");
                    break;
                case 6: Windows[showWindow] = GUI.Window(6, Continents[showWindow].window, DoMyWindow, "Asia");
                    break;
                case 7: Windows[showWindow] = GUI.Window(7, Continents[showWindow].window, DoMyWindow, "Australia");
                    break;
            }   
        }     
    }

    void DoMyWindow(int windowID)
    {
        if (GUI.Button(new Rect(20, Continents[showWindow].window.height-30, 100, 20), "Hide")) { 
            showWindow = 0;
        }

        for( int i = 0; i < Continents[showWindow].Workers.Length; i++)
        {
            if (Continents[showWindow].Workers[i].character.Contains(Event.current.mousePosition))
            {
                if (Event.current.type == EventType.MouseDown)
                {
                    Continents[showWindow].Workers[i].pressed = true;           
                }
                if (Event.current.type == EventType.MouseUp)
                {
                    Continents[showWindow].Workers[i].pressed = false;
                }
            }

            if (Continents[showWindow].Workers[i].pressed && Event.current.type == EventType.MouseDrag)
            {
                Continents[showWindow].Workers[i].character.x += Event.current.delta.x;
                Continents[showWindow].Workers[i].character.y += Event.current.delta.y;
            }

           
            Continents[showWindow].updateWindows();
            
            GUI.Button(Continents[showWindow].Workers[i].character, "Char");
            GUI.Button(Continents[showWindow].Slots[0], "Easy");
            GUI.Button(Continents[showWindow].Slots[1], "Medium");
            GUI.Button(Continents[showWindow].Slots[2], "Hard");
        }
    }

    void Character(int windowID)
    {
       
    }

    public void chooseWindow(int id)
    {
        showWindow = id;
    }
}
