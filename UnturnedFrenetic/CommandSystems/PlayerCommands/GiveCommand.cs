﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frenetic.CommandSystem;
using UnturnedFrenetic.TagSystems.TagObjects;

namespace UnturnedFrenetic.CommandSystems.PlayerCommands
{
    class GiveCommand: AbstractCommand
    {
        public GiveCommand()
        {
            Name = "give";
            Arguments = "<player> <item> [amount]";
            Description = "Gives a player the specified item.";
        }

        public override void Execute(CommandEntry entry)
        {
            if (entry.Arguments.Count < 2)
            {
                ShowUsage(entry);
                return;
            }
            PlayerTag player = PlayerTag.For(entry.GetArgument(0));
            if (player == null)
            {
                entry.Bad("Invalid player!");
                return;
            }
            ushort item = Utilities.StringToUShort(entry.GetArgument(1));
            byte amount = 1;
            if (entry.Arguments.Count > 2)
            {
                amount = (byte)Utilities.StringToUInt(entry.GetArgument(2));
            }
            if (SDG.Unturned.ItemTool.tryForceGiveItem(player.Internal.player, item, amount))
            {
                entry.Good("Successfully gave item!");
            }
            else
            {
                entry.Bad("Failed to give item (is the inventory full?)!");
            }
        }
    }
}
