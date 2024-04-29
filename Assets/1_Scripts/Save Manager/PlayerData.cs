[System.Serializable]

public class PlayerData
{
    public bool mute;
    public int dificultad;
    public int tecladoOMovil;

    public PlayerData(EnemysSave enemysSave)
    {
        dificultad = enemysSave.dificultad;
    }
    public PlayerData(MenuPausa mp)
    {
        mute = mp.muteG;
    }
    public PlayerData(Movement movement)
    {
        tecladoOMovil = movement.tecladoOMovil;
    }
}
