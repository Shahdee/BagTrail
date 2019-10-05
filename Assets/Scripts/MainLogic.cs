using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// model 

public class MainLogic : MonoBehaviour
{
    public GUILogic m_GUILogic;
    public EntityManager m_EntityManager;
    public InputManager m_InputManager;

    DataLoader m_DataLoader;
    LevelLogic m_LevelLogic;
    PlayerProfile m_Profile;

    public DataLoader GetDataLoader(){
        return m_DataLoader;
    }

    public EntityManager GetEntityManager(){
        return m_EntityManager;
    }

    public InputManager GetInputManager(){
        return m_InputManager;
    }

    public LevelLogic GetLevelLogic(){
        return m_LevelLogic;
    }

    public PlayerProfile GetPlayerProfile(){
        return m_Profile;
    }

    void Start()
    {
        m_DataLoader = new DataLoader(this);
        m_LevelLogic = new LevelLogic(this);
        m_Profile = new PlayerProfile(this);

        // m_InputManager.Init(this);
        // m_EntityManager.Init(this);
        
        m_GUILogic.Init(this);

    }

    public void StartGame(){

        // TODO future - set level according to players progress 

        m_LevelLogic.StartLevel(0);
        
    }

    public void TryUnlock(string pin){
        m_LevelLogic.TryUnlock(pin); 
    }

    public void MoveNext(){

        m_LevelLogic.MoveNext(); 
    }

    float deltaTime = 0;

     void Update(){
        // m_InputManager.UpdateMe();

        deltaTime = Time.deltaTime;

        m_LevelLogic.UpdateMe(deltaTime);
        m_GUILogic.UpdateMe(deltaTime);
      
    }
}
