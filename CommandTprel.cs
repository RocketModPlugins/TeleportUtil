﻿using System;
using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using UnityEngine;
using SDG.Unturned;

namespace TeleportUtil
{
    public class CommandTprel : IRocketCommand
    {
        public bool AllowFromConsole
        {
            get { return false; }
        }

        public string Name
        {
            get { return "tprel"; }
        }

        public string Help
        {
            get { return "Teleport to x,y,z coords relative to yourself."; }
        }

        public string Syntax
        {
            get { return "<x> <y> <z>"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public List<string> Permissions
        {
            get { return new List<string>() { "TeleportUtil.tprel" }; }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer unturnedCaller = UnturnedPlayer.FromName(caller.Id);
            if (command.Length == 0)
            {
                UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("tprel_help", new object[] { }));
                return;
            }
            if (command.Length != 3)
            {
                UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("invalid_arg", new object[] { }));
                return;
            }
            else
            {
                float? x = command.GetFloatParameter(0);
                float? y = command.GetFloatParameter(1);
                float? z = command.GetFloatParameter(2);

                if (x.HasValue && y.HasValue && z.HasValue)
                {
                    if (unturnedCaller.Stance == EPlayerStance.DRIVING || unturnedCaller.Stance == EPlayerStance.SITTING)
                    {
                        UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("tp_fail", new object[] { }));
                        return;
                    }
                    Vector3 newLocation = new Vector3(unturnedCaller.Position.x + x.Value, unturnedCaller.Position.y + y.Value, unturnedCaller.Position.z + z.Value);
                    unturnedCaller.Teleport(newLocation, unturnedCaller.Rotation);
                    UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("tp_success", new object[] { Math.Round(newLocation.x, 2), Math.Round(newLocation.y, 2), Math.Round(newLocation.z, 2) }));
                }
                else
                {
                    UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("invalid_arg", new object[] { }));
                    return;
                }
            }
        }
    }
}
