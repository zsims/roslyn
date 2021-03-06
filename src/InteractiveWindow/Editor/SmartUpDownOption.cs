﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.VisualStudio.InteractiveWindow
{
    [Export(typeof(EditorOptionDefinition))]
    [Name(OptionName)]
    internal sealed class SmartUpDownOption : EditorOptionDefinition<bool>
    {
        internal const string OptionName = "InteractiveSmartUpDown";
        
        public override EditorOptionKey<bool> Key
        {
            get
            {
                return InteractiveWindowOptions.SmartUpDown;
            }
        }
    }
}
