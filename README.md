# FamiliarPlus
The below is Aephiex's original description from Nexus.
--------------------------------------------------------
 Item based familiars may provide their check bonuses without being summoned.
 ----------------------------------------------------------------------------
 There is a little, little, very little annoyance in this game.

Class feature based familiars do provide their check bonuses regardless of being summoned or not.

But item based familiars only provide their check bonuses when summoned.

Your familiars roam everywhere in battle, even pits. Your familiars get unsummoned when you respec. The longer your familiars stay active, the more laggy the game becomes. All these inconvenience for some damn check bonuses.

Not anymore.

With this mod installed, item based familiars provide their check bonuses for as long as they are equipped to a character, regardless of being summoned or not.

How to Install
Download and install Unity Mod Manager, make sure it is at least version 0.23.0
Run Unity Mod Manger and set it up to find Wrath of the Righteous
Download this mod
Install the mod by dragging the zip file from step 5 into the Unity Mod Manager window under the Mods tab


How to Uninstall
This mod alters save and in order to make your save loadable without this mod, you must follow these instructions.

Before uninstalling, you must permanently delete your familiar items from your save file, either by Toy Box (remove familiar items in Search n' Pick tab) or global map discarding (this way the item is permanently deleted without the need of Toy Box). Do not drop them to the ground or sell them to vendors, this way they will still be present in your save file.

If you are using v1.0.X, there is an additional step. Add familiar items with Toy Box one of each you just deleted (do NOT use the old ones). Make your characters that used to equip them equip them again, then unequip them, and delete them again with Toy Box. Alternatively, you may respec the characters that used to equip any of the familiars.

Then save, quit the game, uninstall this mod, and everything will be fine. You may use Toy Box (Search n' Pick tab) to add familiar items you deleted in the process after you load your save afterwards.

But, why?
Due to how the game handles save file, the whole blueprint is imprinted with each item into the save file, and loading a save file with unknown blueprints causes error.
Due to how the game handles equipable items with enchantments, each new kind of enchantment requires a new blueprint, and adding an invisible enchantment to an equipable item is the only way to add a static effect to it.
That's to say, transfering static effects from familiar item activation abilities to familiar items themselves requires adding new kinds of inivisible enchantments, and in turn adding new blueprints and altering your save file. You must be sure no familiar items present in your save, or your save file cannot be loaded without this mod.
For the additional step, there was a bug in v1.0.X where after a save & load, familiar item invisible enchantments will be permanently attached to the character and the item will have its enchantment permanently dormant (still present). That's to say, deleting all familar items is not enough to erase the new blueprints from your save. Equipping a currently fine familiar item and unequipping it removes the enchantment imprinted with the character and the same is true for respecing. To confirm if your character is still imprinted with the invisible enchantment or not, go to the stats tab and see skill checks if there is something like "+2 Insight [Pipefox]", This issue is not present since v2.0.0.


How to Update from v1.0.2 to v2.0.0+
Follow the instruction of How to Uninstall, then install the newest version of Familiar Plus.
