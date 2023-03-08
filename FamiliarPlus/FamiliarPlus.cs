// Decompiled with JetBrains decompiler
// Type: FamiliarPlus.FamiliarPlus
// Assembly: FamiliarPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 919FB575-AAFD-4EDA-8A87-C0A47BD282BA
// Assembly location: F:\Games\steamapps\common\Pathfinder Second Adventure\Mods\FamiliarPlus\FamiliarPlus.dll

using HarmonyLib;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.EquipmentEnchants;
using Kingmaker.ElementsSystem;
using Kingmaker.Localization;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;

namespace FamiliarPlus
{
  public static class FamiliarPlus
  {
    public static void AddBlueprint(this SimpleBlueprint blueprint)
    {
      BlueprintGuid assetGuid = blueprint.AssetGuid;
      if (ResourcesLibrary.TryGetBlueprint(assetGuid) != null)
        return;
      ResourcesLibrary.BlueprintsCache.AddCachedBlueprint(assetGuid, blueprint);
      blueprint.OnEnable();
    }

    [HarmonyPatch(typeof (BlueprintsCache), "Init")]
    internal static class BlueprintsCache_Init_Patch
    {
      private static LocalizedString emptyString = new LocalizedString();

      private static void Postfix()
      {
        string[] strArray = new string[7]
        {
          "ff0b6d4a4c0142344924711cb47f39d9",
          "53eaa254970205c4bb7d9c9813a25915",
          "9a537b3bc986d7341b30e8acedafeb3c",
          "bcc767d66c7d2c74f9fe70b73c83c286",
          "fedfe06fec617c8429b2db25eb584cd6",
          "6ea6d3b50bfa24e46a710beafbd95cd0",
          "ee799ea9efafbe143a603b801abf0ad8"
        };
        for (int i = 0; i < strArray.Length; ++i)
        {
          BlueprintItemEquipmentUsable blueprint = (BlueprintItemEquipmentUsable) ResourcesLibrary.TryGetBlueprint(new BlueprintGuid(Guid.Parse(strArray[i])));
          if (blueprint != null)
          {
            BlueprintBuff buff1 = blueprint.ActivatableAbility.Buff;
            if (buff1 != null)
            {
              BlueprintComponent[] componentsArrayOfBuff = buff1.ComponentsArray;
              List<Element> allElements = buff1.m_AllElements;
              buff1.m_AllElements = (List<Element>) null;
              if (componentsArrayOfBuff[0] is AddFactContextActions)
              {
                GameAction[] actions = (componentsArrayOfBuff[0] as AddFactContextActions).Activated.Actions;
                for (int index = 0; index < actions.Length; ++index)
                {
                  BlueprintBuff buff2 = ((actions[index] as Conditional).IfTrue.Actions[0] as ContextActionApplyBuff).Buff;
                  BlueprintComponent[] componentsArray = buff2.ComponentsArray;
                  buff2.ComponentsArray = new BlueprintComponent[1]
                  {
                    componentsArray[0]
                  };
                  if (index == 0)
                    componentsArrayOfBuff = componentsArray;
                }
              }
              else
                buff1.ComponentsArray = new BlueprintComponent[1]
                {
                  componentsArrayOfBuff[0]
                };
              string base_guid_local = strArray[i].Substring(0, strArray[i].Length - 2);
              BlueprintFeature newFeature = FamiliarPlus.BlueprintsCache_Init_Patch.GenerateNewFeature(base_guid_local, i, componentsArrayOfBuff, allElements);
              BlueprintFeatureReference featureReference = FamiliarPlus.BlueprintsCache_Init_Patch.GenerateNewFeatureReference(newFeature);
              BlueprintEquipmentEnchantment newEnchantment = FamiliarPlus.BlueprintsCache_Init_Patch.GenerateNewEnchantment(base_guid_local, i, featureReference);
              BlueprintEquipmentEnchantmentReference enchantmentReference = FamiliarPlus.BlueprintsCache_Init_Patch.GenerateNewEnchantmentReference(newEnchantment);
              FamiliarPlus.BlueprintsCache_Init_Patch.AddEnchantmentToUsable(blueprint, enchantmentReference);
              newFeature.AddBlueprint();
              newEnchantment.AddBlueprint();
              blueprint.OnEnable();
            }
          }
        }
      }

      private static void AddEnchantmentToUsable(
        BlueprintItemEquipmentUsable usable,
        BlueprintEquipmentEnchantmentReference enchantmentRef)
      {
        usable.m_Enchantments = new BlueprintEquipmentEnchantmentReference[1]
        {
          enchantmentRef
        };
      }

      private static BlueprintFeature GenerateNewFeature(
        string base_guid_local,
        int i,
        BlueprintComponent[] componentsArrayOfBuff,
        List<Element> elementsArray)
      {
        BlueprintFeature newFeature = new BlueprintFeature();
        newFeature.AssetGuid = BlueprintGuid.Parse(base_guid_local + i.ToString() + "b");
        newFeature.name = "AephiexGeneratedFeature" + i.ToString() + "b";
        newFeature.m_DisplayName = FamiliarPlus.BlueprintsCache_Init_Patch.emptyString;
        newFeature.m_Description = FamiliarPlus.BlueprintsCache_Init_Patch.emptyString;
        newFeature.ComponentsArray = new BlueprintComponent[componentsArrayOfBuff.Length - 1];
        newFeature.m_AllElements = elementsArray;
        newFeature.HideInUI = true;
        newFeature.HideInCharacterSheetAndLevelUp = true;
        for (int index = 1; index < componentsArrayOfBuff.Length; ++index)
        {
          BlueprintComponent blueprintComponent = componentsArrayOfBuff[index];
          blueprintComponent.OwnerBlueprint = (BlueprintScriptableObject) newFeature;
          newFeature.ComponentsArray[index - 1] = blueprintComponent;
        }
        return newFeature;
      }

      private static BlueprintFeatureReference GenerateNewFeatureReference(
        BlueprintFeature blueprintFeature)
      {
        BlueprintFeatureReference featureReference = new BlueprintFeatureReference();
        featureReference.ReadGuidFromGuid(blueprintFeature.AssetGuid);
        return featureReference;
      }

      private static BlueprintEquipmentEnchantment GenerateNewEnchantment(
        string base_guid_local,
        int i,
        BlueprintFeatureReference blueprintFeatureRef)
      {
        BlueprintEquipmentEnchantment newEnchantment = new BlueprintEquipmentEnchantment();
        AddUnitFeatureEquipment featureEquipment = new AddUnitFeatureEquipment();
        newEnchantment.AssetGuid = BlueprintGuid.Parse(base_guid_local + i.ToString() + "a");
        newEnchantment.name = "AephiexGeneratedEnchantment" + i.ToString() + "a";
        newEnchantment.m_EnchantName = FamiliarPlus.BlueprintsCache_Init_Patch.emptyString;
        newEnchantment.m_Description = FamiliarPlus.BlueprintsCache_Init_Patch.emptyString;
        newEnchantment.m_Prefix = FamiliarPlus.BlueprintsCache_Init_Patch.emptyString;
        newEnchantment.m_Suffix = FamiliarPlus.BlueprintsCache_Init_Patch.emptyString;
        newEnchantment.ComponentsArray = new BlueprintComponent[1]
        {
          (BlueprintComponent) featureEquipment
        };
        featureEquipment.m_Feature = blueprintFeatureRef;
        featureEquipment.name = "AephiexGeneratedComp" + i.ToString() + "a";
        return newEnchantment;
      }

      private static BlueprintEquipmentEnchantmentReference GenerateNewEnchantmentReference(
        BlueprintEquipmentEnchantment blueprintEnchantment)
      {
        BlueprintEquipmentEnchantmentReference enchantmentReference = new BlueprintEquipmentEnchantmentReference();
        enchantmentReference.ReadGuidFromGuid(blueprintEnchantment.AssetGuid);
        return enchantmentReference;
      }
    }
  }
}
