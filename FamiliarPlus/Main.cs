// Decompiled with JetBrains decompiler
// Type: FamiliarPlus.Main
// Assembly: FamiliarPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 919FB575-AAFD-4EDA-8A87-C0A47BD282BA
// Assembly location: F:\Games\steamapps\common\Pathfinder Second Adventure\Mods\FamiliarPlus\FamiliarPlus.dll

using HarmonyLib;
using System.Diagnostics;
using UnityModManagerNet;
using Kingmaker;

namespace FamiliarPlus
{
  internal static class Main
  {
    public static bool Enabled = true;
    public static UnityModManager.ModEntry Mod;

    private static bool Load(UnityModManager.ModEntry modEntry)
    {
      Harmony harmony = new Harmony(modEntry.Info.Id);
      Main.Mod = modEntry;
      harmony.PatchAll();
      Main.Log("DEBUG LOGGING ENABLED");
      return true;
    }

    private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
    {
      Main.Enabled = value;
      return true;
    }

    [Conditional("DEBUG")]
    public static void Log(string msg) => Main.Mod.Logger.Log(msg);
  }
}
