#region Copyright (c) Pixeval/Pixeval
// GPL v3 License
// 
// Pixeval/Pixeval
// Copyright (c) 2023 Pixeval/LoginPage.xaml.cs
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
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using Pixeval.AppManagement;
using Pixeval.Util.UI;
using Pixeval.Utilities;
using WinUI3Utilities;

namespace Pixeval.Pages.Login;

public sealed partial class LoginPage
{
    private readonly LoginPageViewModel _viewModel;

    public LoginPage()
    {
        _viewModel = new(this);
        InitializeComponent();
    }

    private async void Login_OnTapped(object sender, TappedRoutedEventArgs e)
    {
        try
        {
            await _viewModel.WebView2LoginAsync(this, Navigated);
        }
        catch (Exception exception)
        {
            _ = await this.CreateAcknowledgementAsync(LoginPageResources.ErrorWhileLoggingInTitle,
                    LoginPageResources.ErrorWhileLogginInContentFormatted.Format(exception + "\n" + exception.StackTrace));
            Application.Current.Exit();
        }

        return;
        void Navigated()
        {
            if (App.AppViewModel.MakoClient == null!)
                ThrowHelper.Exception();

            _ = DispatcherQueue.TryEnqueue(() =>
            {
                _viewModel.AdvancePhase(LoginPageViewModel.LoginPhaseEnum.SuccessNavigating);
                NavigateParent<MainPage>(null, new DrillInNavigationTransitionInfo());
                AppInfo.SaveContext();
            });
        }
    }

    private async void LoginPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_viewModel.CheckRefreshAvailable())
            {
                await _viewModel.RefreshAsync();
                NavigateParent<MainPage>(null, new DrillInNavigationTransitionInfo());
            }
            else
            {
                _viewModel.AdvancePhase(LoginPageViewModel.LoginPhaseEnum.WaitingForUserInput);
                _viewModel.IsFinished = _viewModel.IsEnabled = true;
            }
        }
        catch (Exception exception)
        {
            _ = await this.CreateAcknowledgementAsync(LoginPageResources.ErrorWhileLoggingInTitle,
                    LoginPageResources.ErrorWhileLogginInContentFormatted.Format(exception.StackTrace));
            Application.Current.Exit();
        }
    }
}
