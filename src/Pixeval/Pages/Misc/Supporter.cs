#region Copyright (c) Pixeval/Pixeval
// GPL v3 License
// 
// Pixeval/Pixeval
// Copyright (c) 2022 Pixeval/Supporter.cs
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

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;

namespace Pixeval.Pages.Misc;

public record Supporter(string Nickname, string Name, ImageSource ProfilePicture, Uri ProfileUri)
{
    // ReSharper disable StringLiteralTypo
    public static readonly IEnumerable<Supporter> Supporters = new List<Supporter>
    {
        new("Sep", "@Guro2", SupportImageOf(22517862), new("https://github.com/Guro2")),
        new("无论时间", "@wulunshijian", SupportImageOf(46415928), new("https://github.com/wulunshijian")),
        new("CN", "@ControlNet", SupportImageOf(12800094), new("https://github.com/ControlNet")),
        new("CY", "@Cyl18", SupportImageOf(14993992), new("https://github.com/cyl18")),
        new("对味", "@duiweiya", SupportImageOf(40987061), new("https://github.com/duiweiya")),
        new("LG", "@LasmGratel", SupportImageOf(6669365), new("https://github.com/LasmGratel")),
        new("鱼鱼", "@sovetskyfish",SupportImageOf(76583116), new("https://github.com/sovetskyfish")),
        new("探姬", "@PerolNotsfsssf", SupportImageOf(96558937), new("https://github.com/Notsfsssf")),
        new("Summpot", "@Summpot", SupportImageOf(29229273), new("https://github.com/Summpot")),
        new("扑克", "@Poker-sang", SupportImageOf(62325494), new("https://github.com/Poker-sang")),
        new("南门二", "@Rigil-Kentaurus", SupportImageOf(49679244), new("https://github.com/Rigil-Kentaurus")),
        new("当妈", "@TheRealKamisama", SupportImageOf(35005476), new("https://github.com/TheRealKamisama")),
        new("茶栗", "@CharlieJiang", SupportImageOf(5109850), new("https://github.com/cqjjjzr"))
    };
    // ReSharper restore StringLiteralTypo

    private static ImageSource SupportImageOf(int supporterId)
    {
        return new BitmapImage(new($"https://avatars.githubusercontent.com/u/{supporterId}?v=4"));
    }
}
