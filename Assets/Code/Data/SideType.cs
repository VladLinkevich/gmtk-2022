using System;

namespace Code.Data
{
  // first 0000 - action type (attack, def, heal)
  // second 0000 - id type
  [Flags]
  public enum SideType
  {
    None =            0b0000_0000,
    Mana =            SideAction.Use | 0b0000_0001,
    Arrow =           SideAction.Attack | 0b0000_0010,
    Life =            SideAction.Def | 0b0000_0011,
    Shield =          SideAction.Def | 0b0000_0100,
    Sword =           SideAction.Attack | 0b0000_0101,
  }

  [Flags]
  public enum SideAction
  {
    Attack = 0b1000_0000,
    Def = 0b0100_0000,
    Use = 0b0010_0000
  }
}