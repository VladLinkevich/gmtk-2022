using System;

namespace Code.Data
{
  // first 0000 - action type (attack, def, heal)
  // second 0000 - id type
  //[Flags]
  public enum SideType
  {
    None =            0,
    Mana =            SideAction.Use | 1,
    Arrow =           SideAction.Attack | 2,
    Life =            SideAction.Def | 3,
    Shield =          SideAction.Def | 4,
    Sword =           SideAction.Attack | 5,
  }

  [Flags]
  public enum SideAction
  {
    Attack = 1 << 31,
    Def = 1 << 30,
    Use = 1 << 29
  }
}