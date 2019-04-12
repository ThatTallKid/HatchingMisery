using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HenCallSystemControls
{
  // Call is Left Mouse/A button
  public static bool AButton()
  {
    return Input.GetButtonDown("A_Button");
  }
  
  // Stop is Right Mouse/B button
  public static bool BButton()
  {
    return Input.GetButtonDown("B_Button");
  }
}
