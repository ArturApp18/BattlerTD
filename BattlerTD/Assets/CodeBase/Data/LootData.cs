using System;

namespace CodeBase.Data
{
  [Serializable]
  public class LootData
  {
    public int Collected;
    public float RequiredPointForNextLevel = 5000000000000;
    public int Level;
    public LootPieceDataDictionary LootPiecesOnScene = new LootPieceDataDictionary();
    
    public Action Changed;
    public Action LevelUp;

    public void Collect(Loot loot)
    {
      Collected += loot.Value;
      Changed?.Invoke();

      if (Collected <= RequiredPointForNextLevel)
        return;

      RequiredPointForNextLevel *= 1.33f;
      LevelUp?.Invoke();
      Level++;                           
      Collected = 0;
    }

    public void Add(int lootValue)
    {
      Collected += lootValue;
      Changed?.Invoke();
    }
  }
}