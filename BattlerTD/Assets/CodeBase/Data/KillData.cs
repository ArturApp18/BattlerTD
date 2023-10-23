using System;

namespace CodeBase.Data
{
  [Serializable]
  public class KillData
  {
    public int KilledMobs;
    
    public Action Changed;
    
    public void Add(int lootValue)
    {
      KilledMobs += lootValue;
      Changed?.Invoke();
    } 
    
    public void ResetKillData()
    {
      KilledMobs = 0;
      Changed?.Invoke();
    }
  }
}