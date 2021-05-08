using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class ESPConnector : MonoBehaviour
{

    static SerialPort sp;
    public string message;

    [SerializeField]
    string portName = "COM3";

    private class JoyStickStatus
    {
        public static bool right, left, up, down, press, holding, release;
        public static void refresh()
        {
            press = false;
            release = false;
        }
        public static void handle(string message)
        {

            switch (message)
            {
                case "StickUp":
                    up = true;
                    down = false;
                    break;
                case "StickDown":
                    down = true;
                    up = false;
                    break;
                case "StickLeft":
                    left = true;
                    right = false;
                    break;
                case "StickRight":
                    right = true;
                    left = false;
                    break;
                case "StickX0":
                    right = false;
                    left = false;
                    break;
                case "StickY0":
                    up = false;
                    down = false;
                    break;
                case "StickPress":
                    press = true;
                    holding = true;
                    break;
                case "StickRelease":
                    release = true;
                    holding = false;
                    break;

            }
        }
    }

    private class Btn
    {
        public bool Holding, Down, Up;
    }
    [SerializeField]
    private class BtnStatus
    {

        public static Btn BtnRT = new Btn(), BtnRB = new Btn(),
            BtnLT = new Btn(), BtnLB = new Btn();

        public static void refresh()
        {
            BtnRT.Down = false;
            BtnRB.Down = false;
            BtnLT.Down = false;
            BtnLB.Down = false;
            BtnRT.Up = false;
            BtnRB.Up = false;
            BtnLT.Up = false;
            BtnLB.Up = false;
           
        }

        public static void handle(string message)
        {
            switch (message)
            {
                case "BtnRTDown":
                    BtnStatus.BtnRT.Down = true;
                    BtnStatus.BtnRT.Holding = true;
                    break;
                case "BtnRBDown":
                    BtnStatus.BtnRB.Down = true;
                    BtnStatus.BtnRB.Holding = true;
                    break;
                case "BtnLTDown":
                    BtnStatus.BtnLT.Down = true;
                    BtnStatus.BtnLT.Holding = true;
                    break;
                case "BtnLBDown":
                    BtnStatus.BtnLB.Down = true;
                    BtnStatus.BtnLB.Holding = true;
                    break;
                case "BtnRTUp":
                    BtnStatus.BtnRT.Up = true;
                    BtnStatus.BtnRT.Holding = false;
                    break;
                case "BtnRBUp":
                    BtnStatus.BtnRB.Holding = false;
                    BtnStatus.BtnRB.Up = true;
                    break;
                case "BtnLTUp":
                    BtnStatus.BtnLT.Holding = false;
                    BtnStatus.BtnLT.Up = true;
                    break;
                case "BtnLBUp":
                    BtnStatus.BtnLB.Holding = false;
                    BtnStatus.BtnLB.Up = true;
                    break;
            }
        }
    }

    public class AnalogStick
    {
        private static int x, y;
        public static int getX()
        {
            if (JoyStickStatus.right)
                return 1;
            else if (JoyStickStatus.left)
                return -1;
            return 0;
        }
        public static int getY()
        {
            if (JoyStickStatus.up)
                return 1;
            else if (JoyStickStatus.down)
                return -1;
            return 0;
        }
    }

    private static int swSlot = 0;

    public static int getSwitchSlot()
    {
        return swSlot;
    }
    // Start is called before the first frame update

    public enum Key
    {
        BtnRT,
        BtnRB,
        BtnLT,
        BtnLB,
        JoyStick
    }

    void Start()
    {
        sp = new SerialPort(portName, 115200);
        sp.ReadTimeout = 2;
        try
        {
            sp.Open();
            Debug.Log("ESP連接成功");
        }
        catch
        {
            Debug.Log("ESP連接失敗");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SPWrite("motor:1;");
        }

        try
        {


            BtnStatus.refresh();
            JoyStickStatus.refresh();
            while (true) {
                message = sp.ReadLine();
                if (message.StartsWith("Btn"))
                    BtnStatus.handle(message);
                if (message.StartsWith("Stick"))
                    JoyStickStatus.handle(message);
                if(message.StartsWith("switch"))
                {
                    swSlot = Int32.Parse(message.Substring(6));
                }
                    
                Debug.Log(message);
            }
            
        }
        catch (System.Exception e)
        {
            // Debug.Log(e.Message);
        }
    }

    void OnApplicationQuit()
    {
        sp.Close();
    }

    public static void SPWrite(string message)
    {
        try
        {
            sp.Write(message);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }

    }

    public static bool getKey(Key key)
    {
        switch (key)
        {
            case Key.BtnRT:
                return BtnStatus.BtnRT.Holding;
            case Key.BtnRB:
                return BtnStatus.BtnRB.Holding;
            case Key.BtnLT:
                return BtnStatus.BtnLT.Holding;
            case Key.BtnLB:
                return BtnStatus.BtnLB.Holding;
            case Key.JoyStick:
                return JoyStickStatus.holding;
            default:
                return false;
        }
    }

    public static bool GetKeyDown(Key key)
    {
        switch (key)
        {
            case Key.BtnRT:
                return BtnStatus.BtnRT.Down;
            case Key.BtnRB:
                return BtnStatus.BtnRB.Down;
            case Key.BtnLT:
                return BtnStatus.BtnLT.Down;
            case Key.BtnLB:
                return BtnStatus.BtnLB.Down;
            case Key.JoyStick:
                return JoyStickStatus.press;
            default:
                return false;
        }
    }

    public static bool getKeyUp(Key key)
    {
        switch (key)
        {
            case Key.BtnRT:
                return BtnStatus.BtnRT.Up;
            case Key.BtnRB:
                return BtnStatus.BtnRB.Up;
            case Key.BtnLT:
                return BtnStatus.BtnLT.Up;
            case Key.BtnLB:
                return BtnStatus.BtnLB.Up;
            case Key.JoyStick:
                return JoyStickStatus.release;
            default:
                return false;
        }
    }

    public bool isActive()
    {
        return sp.IsOpen;
    }

}

