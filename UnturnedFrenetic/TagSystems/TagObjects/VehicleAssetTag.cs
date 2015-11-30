﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frenetic.TagHandlers;
using Frenetic.TagHandlers.Objects;
using SDG.Unturned;

namespace UnturnedFrenetic.TagSystems.TagObjects
{
    public class VehicleAssetTag: TemplateObject
    {
        public static List<VehicleAsset> Vehicles;
        public static Dictionary<string, VehicleAsset> VehiclesMap;

        public static void Init()
        {
            Vehicles = new List<VehicleAsset>();
            VehiclesMap = new Dictionary<string, VehicleAsset>();
            Asset[] assets = Assets.find(EAssetType.ITEM);
            foreach (Asset asset in assets)
            {
                Vehicles.Add((VehicleAsset)asset);
                string namelow = asset.name.ToLower();
                if (VehiclesMap.ContainsKey(namelow))
                {
                    SysConsole.Output(OutputType.INIT, "MINOR: multiple vehicle assets named " + namelow);
                    continue;
                }
                VehiclesMap.Add(namelow, (VehicleAsset)asset);
                EntityType.VEHICLES.Add(namelow, new EntityType(asset.name, EntityAssetType.VEHICLE));
            }
            SysConsole.Output(OutputType.INIT, "Loaded " + Vehicles.Count + " base vehicles!");
        }

        public static VehicleAssetTag For(string nameorid)
        {
            ushort id;
            VehicleAsset asset;
            if (ushort.TryParse(nameorid, out id))
            {
                asset = (VehicleAsset)Assets.find(EAssetType.VEHICLE, id);
            }
            else
            {
                VehiclesMap.TryGetValue(nameorid.ToLower(), out asset);
            }
            if (asset == null)
            {
                return null;
            }
            return new VehicleAssetTag(asset);
        }

        public VehicleAssetTag(VehicleAsset asset)
        {
            Internal = asset;
        }

        public VehicleAsset Internal;

        public override string Handle(TagData data)
        {
            if (data.Input.Count == 0)
            {
                return ToString();
            }
            switch (data.Input[0])
            {
                // <--[tag]
                // @Name VehicleTag.name
                // @Group General Information
                // @ReturnType TextTag
                // @Returns the name of the vehicle.
                // @Example "APC_Forest" .name returns "APC_Forest".
                // -->
                case "name":
                    return new TextTag(Internal.name).Handle(data.Shrink());
                // <--[tag]
                // @Name VehicleTag.id
                // @Group General Information
                // @ReturnType TextTag
                // @Returns the internal ID of the item.
                // @Example "APC_Forest" .id returns "53".
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