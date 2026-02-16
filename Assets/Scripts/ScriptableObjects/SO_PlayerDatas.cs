using UnityEngine;

[CreateAssetMenu(fileName = "SO_PlayerDatas", menuName = "Scriptable Objects/SO_PlayerDatas")]
public class SO_PlayerDatas : ScriptableObject
{
    public string Name;
    public int Score1;
    public int Score2;
    public int Score3;
    public int Level;


    private SaveController saveSystem;

    public void LoadDatas()
    {
        CheckSaveSystem();
        // utiliser la fonction de savesystem pour load les datas
        // cette fonction renvoie des playersdatas 
        PlayerDatas datas = saveSystem.Load();
        // donc je dois les affecter aux variables de mon scriptable object
        Name = datas.Name;
        Score1 = datas.Score1;
        Score2 = datas.Score2;
        Score3 = datas.Score3;
        Level = datas.Level;
    }

    public void SaveDatas()
    {
        CheckSaveSystem();
        // pour utiliser la fonction save de savesystem j'ai besoin de playerdatas
        // donc je dois créer un playerdatas à partir de mon so
        PlayerDatas datas = new PlayerDatas();
        datas.Name = Name;
        datas.Score1 = Score1;
        datas.Score2 = Score2;
        datas.Score3 = Score3;
        datas.Level = Level;
        // j'envois ça à la fonction save de savesystem
        saveSystem.Save(datas);
    }

    private void CheckSaveSystem()
    {
        // vérifier si savesystem conteint un objet du type savesystem
        if (saveSystem == null) 
        {
            // s'il n'y a rien, j'en crée (instancie) un.
            saveSystem = new SaveController();
        }
    }
}
