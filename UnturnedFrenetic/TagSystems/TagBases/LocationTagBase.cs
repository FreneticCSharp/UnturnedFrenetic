﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frenetic.TagHandlers;
using Frenetic.TagHandlers.Objects;
using UnturnedFrenetic.TagSystems.TagObjects;

namespace UnturnedFrenetic.TagSystems.TagBases
{
    class LocationTagBase : TemplateTags
    {
        // <--[tag]
        // @Base location[<TextTag>]
        // @Group Mathematics
        // @ReturnType LocationTag
        // @Returns the item corresponding to the given name or ID.
        // -->
        public LocationTagBase()
        {
            Name = "location";
        }

        public override string Handle(TagData data)
        {
            string lname = data.GetModifier(0);
            LocationTag ltag = LocationTag.For(lname);
            if (ltag == null)
            {
                return new TextTag("{NULL}").Handle(data.Shrink());
            }
            return ltag.Handle(data.Shrink());
        }
    }
}
