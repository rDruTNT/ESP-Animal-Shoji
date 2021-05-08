using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{


    [SerializeField]
    ESPConnector esp;
    bool usingEsp;

    Chess selecting;

    [SerializeField]
    private Pointer pointer;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    float moveDelay;
    // Update is called once per frame
    void Update()
    {
        if(esp.isActive())
        {
            usingEsp = true;
            int x = ESPConnector.AnalogStick.getX(), y = ESPConnector.AnalogStick.getY();
            if (x!= 0 || y != 0)
            {
                if(moveDelay<=0.02 && moveDelay < 0.2f)
                {
                    moveDelay += Time.deltaTime;
                    return;
                }
                
                if (Main.isOutSideOfBoard(pointer.x - y, pointer.y + x))
                    return;
                pointer.point(pointer.x - y, pointer.y + x);
                moveDelay -= 0.2f;
            }
            else moveDelay = 0;
                
            if(ESPConnector.GetKeyDown(ESPConnector.Key.JoyStick) {

            }


        } else
        {
            usingEsp = false;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Debug.Log(mousePos.ToString());
            int mouseX = (int)Mathf.Floor((mousePos.y - 5f) / -3.3f);
            int mouseY = (int)Mathf.Floor((mousePos.x + 6.5f) / 3.25f);

            if (selecting == null)
                pointer.point(mouseX, mouseY);
            else
                pointer.point(selecting.getX(), selecting.getY());

            if (Input.GetMouseButtonDown(0))
            {
                Chess c = Main.getChess(mouseX, mouseY);
                

                if (selecting == null)
                {
                    
                    if (c == null) return;
                    if (c.owner == Main.turn)
                    {
                        selecting = c;
                        pointer.clearExtend();
                        pointer.extend(c.getWays());
                    }
                    
                    

                }
                else
                {
                    if (c == null || Main.isOutSideOfBoard(mouseX, mouseY))
                    {
                        pointer.clearExtend();
                        selecting = null;
                    } else
                    {

                    }
                        

                }

                //Chess.CreateChess(chess,mouseX, mouseY, Chess.Type.Falcon, turn);

            }
        }
        

    }
}
