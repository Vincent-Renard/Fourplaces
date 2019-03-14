    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Input;
    using Model.Dtos;
    using Storm.Mvvm;
    using Storm.Mvvm.Services;
    using Xamarin.Forms;

    namespace FourplacesApp.View
    {
        public class LoginViewModel : ViewModelBase
        {
            private string _username;
            private string _password;
            private bool _areCredentialsInvalid;

            public LoginViewModel(INavigationService navigationService)
            {
                AuthenticateCommand = new Command(() =>
                {
                    AreCredentialsInvalid = !UserAuthenticated(Username, Password);
                    if (AreCredentialsInvalid) return;

                    //navigationService.GoBack();
                });

                AreCredentialsInvalid = false;
            }

            private bool UserAuthenticated(string username, string password)
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    return false;
                }

                return username.ToLowerInvariant() == "username"
                    && password.ToLowerInvariant() == "password";
            }

            public string Username
            {
                get => _username;
                set
                {
                    if (value == _username) return;
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }

            public string Password
            {
                get => _password;
                set
                {
                    if (value == _password) return;
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }

            public ICommand AuthenticateCommand { get; set; }

            public bool AreCredentialsInvalid
            {
                get => _areCredentialsInvalid;
                set
                {
                    if (value == _areCredentialsInvalid) return;
                    _areCredentialsInvalid = value;
                    OnPropertyChanged(nameof(AreCredentialsInvalid));
                }
            }

            public LoginRequest GetLoginRequest()
            {
                LoginRequest lr = new LoginRequest();
                lr.Email = _username;
                lr.Password = _password;
                return lr;
            }
        }

    }
