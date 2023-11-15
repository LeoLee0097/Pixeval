#region Copyright (c) Pixeval/Pixeval
// GPL v3 License
// 
// Pixeval/Pixeval
// Copyright (c) 2022 Pixeval/SpotlightTitleMacro.cs
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion

using Pixeval.Download.MacroParser;
using Pixeval.Util.IO;
using Pixeval.Utilities;
using Pixeval.UserControls.IllustrationView;

namespace Pixeval.Download.Macros;

[MetaPathMacro(typeof(IllustrationViewModel))]
public class SpotlightTitleMacro : IMacro<IllustrationViewModel>.ITransducer
{
    public string Name => "spot_title";

    public string Substitute(IllustrationViewModel context)
    {
        return context.Illustrate.SpotlightTitle?.Let(IoHelper.NormalizePathSegment) ?? MacroParserResources.UnknownSpotlightTitle;
    }
}
