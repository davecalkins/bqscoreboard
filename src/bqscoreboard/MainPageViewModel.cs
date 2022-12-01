using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Maui.Audio;

namespace bqscoreboard
{
    internal class MainPageViewModel : ViewModelBase
    {
        private string _team1Name;
        public string Team1Name
        {
            get => _team1Name;
            set => SetProperty(ref _team1Name, value);
        }

        private string _team2Name;
        public string Team2Name
        {
            get => _team2Name;
            set => SetProperty(ref _team2Name, value);
        }

        private string _team3Name;
        public string Team3Name
        {
            get => _team3Name;
            set => SetProperty(ref _team3Name, value);
        }

        private int _questionNumber;
        public int QuestionNumber
        {
            get => _questionNumber;
            set => SetProperty(ref _questionNumber, value);
        }

        private string _questionSuffix;
        public string QuestionSuffix
        {
            get => _questionSuffix;
            set => SetProperty(ref _questionSuffix, value);
        }

        private int _team1Score;
        public int Team1Score
        {
            get => _team1Score;
            set => SetProperty(ref _team1Score, value);
        }

        private int _team2Score;
        public int Team2Score
        {
            get => _team2Score;
            set => SetProperty(ref _team2Score, value);
        }

        private int _team3Score;
        public int Team3Score
        {
            get => _team3Score;
            set => SetProperty(ref _team3Score, value);
        }

        public void OnTeamNameChanged()
        {
            Save();
        }

        public ICommand ResetScoreboard => new Command(() =>
        {
            QuestionNumber = 1;
            QuestionSuffix = "";
            Team1Score = Team2Score = Team3Score = 0;

            Save();

            _resetPlayer.Play();
        });

        public ICommand QuestionSuffixToggle => new Command(() =>
        {
            switch (QuestionSuffix)
            {
                case "":
                    QuestionSuffix = "A";
                    break;
                case "A":
                    QuestionSuffix = "B";
                    break;
                case "B":
                default:
                    QuestionSuffix = "";
                    break;
            }

            Save();
            _buttonPlayer.Play();
        });

        public ICommand QuestionMinus1 => new Command(() =>
        {
            if (QuestionNumber <= 1)
            {
                _errorPlayer.Play();
                return;
            }

            QuestionNumber -= 1;
            Save(); 
            _buttonPlayer.Play();
        });
        public ICommand QuestionPlus1 => new Command(() => { QuestionNumber += 1; Save(); _buttonPlayer.Play(); });

        public ICommand Team1Minus10 => new Command(() => { Team1Score -= 10; Save(); _buttonPlayer.Play(); });
        public ICommand Team1Plus10 => new Command(() => { Team1Score += 10; Save(); _buttonPlayer.Play(); });

        public ICommand Team2Minus10 => new Command(() => { Team2Score -= 10; Save(); _buttonPlayer.Play(); });
        public ICommand Team2Plus10 => new Command(() => { Team2Score += 10; Save(); _buttonPlayer.Play(); });

        public ICommand Team3Minus10 => new Command(() => { Team3Score -= 10; Save(); _buttonPlayer.Play(); });
        public ICommand Team3Plus10 => new Command(() => { Team3Score += 10; Save(); _buttonPlayer.Play(); });

        private IAudioPlayer _resetPlayer = null;
        private IAudioPlayer _buttonPlayer = null;
        private IAudioPlayer _errorPlayer = null;

        public async void Initialize()
        {
            Load();

            _resetPlayer = AudioManager.Current.CreatePlayer(
                await FileSystem.OpenAppPackageFileAsync("dreamstime_111325605_correct.wav"));
            _buttonPlayer = AudioManager.Current.CreatePlayer(
                await FileSystem.OpenAppPackageFileAsync("short-switch.wav"));
            _errorPlayer = AudioManager.Current.CreatePlayer(
                await FileSystem.OpenAppPackageFileAsync("computerbeep_3.wav"));
        }

        private bool _isLoading = false;

        private void Load()
        {
            _isLoading = true;

            var p = Preferences.Default;
            QuestionNumber = p.Get<int>("QuestionNumber", 1);
            QuestionSuffix = p.Get<string>("QuestionSuffix", "");
            Team1Name = p.Get<string>("Team1Name", "Soli");
            Team2Name = p.Get<string>("Team2Name", "Deo");
            Team3Name = p.Get<string>("Team3Name", "Gloria");
            Team1Score = p.Get<int>("Team1Score", 0);
            Team2Score = p.Get<int>("Team2Score", 0);
            Team3Score = p.Get<int>("Team3Score", 0);

            _isLoading = false;
        }

        private void Save()
        {
            if (_isLoading)
                return;

            var p = Preferences.Default;
            p.Set<int>("QuestionNumber", QuestionNumber);
            p.Set<string>("QuestionSuffix", QuestionSuffix);
            p.Set<string>("Team1Name", Team1Name);
            p.Set<string>("Team2Name", Team2Name);
            p.Set<string>("Team3Name", Team3Name);
            p.Set<int>("Team1Score", Team1Score);
            p.Set<int>("Team2Score", Team2Score);
            p.Set<int>("Team3Score", Team3Score);
        }
    }
}
