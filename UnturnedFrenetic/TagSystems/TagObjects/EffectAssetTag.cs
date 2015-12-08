﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frenetic.TagHandlers;
using Frenetic.TagHandlers.Objects;
using SDG.Unturned;

namespace UnturnedFrenetic.TagSystems.TagObjects
{
    public class EffectAssetTag: TemplateObject
    {
        public static List<EffectAsset> Effects;
        public static Dictionary<string, EffectAsset> EffectsMap;

        public static void Init()
        {
            Effects = new List<EffectAsset>();
            EffectsMap = new Dictionary<string, EffectAsset>();
            Asset[] assets = Assets.find(EAssetType.EFFECT);
            foreach (Asset asset in assets)
            {
                Effects.Add((EffectAsset)asset);
                string namelow = asset.name.ToLower();
                if (EffectsMap.ContainsKey(namelow))
                {
                    SysConsole.Output(OutputType.INIT, "MINOR: multiple effect assets named " + namelow);
                    continue;
                }
                EffectsMap.Add(namelow, (EffectAsset)asset);
            }
            SysConsole.Output(OutputType.INIT, "Loaded " + Effects.Count + " base effects!");
        }

        public static EffectAssetTag For(string nameorid)
        {
            ushort id;
            EffectAsset asset;
            if (ushort.TryParse(nameorid, out id))
            {
                asset = (EffectAsset)Assets.find(EAssetType.EFFECT, id);
            }
            else
            {
                EffectsMap.TryGetValue(nameorid.ToLower(), out asset);
            }
            if (asset == null)
            {
                return null;
            }
            return new EffectAssetTag(asset);
        }

        public EffectAsset Internal;

        public EffectAssetTag(EffectAsset asset)
        {
            Internal = asset;
        }

        public override string Handle(TagData data)
        {
            if (data.Input.Count == 0)
            {
                return ToString();
            }
            switch (data.Input[0])
            {
                // <--[tag]
                // @Name EffectAssetTag.name
                // @Group General Information
                // @ReturnType TextTag
                // @Returns the name of the asset.
                // @Example "Concrete_Dynamic" .name returns "Concrete_Dynamic".
                // -->
                case "name":
                    return new TextTag(Internal.name).Handle(data.Shrink());
                // <--[tag]
                // @Name EffectAssetTag.id
                // @Group General Information
                // @ReturnType TextTag
                // @Returns the ID number of the effect asset.
                // @Example "Concrete_Dynamic" .id returns "38".
                // -->
                case "id":
                    return new TextTag(Internal.id).Handle(data.Shrink());
                default:
                    return new TextTag(ToString()).Handle(data);
            }
        }

        public override string ToString()
        {
            return Internal.name;
        }
    }
}
