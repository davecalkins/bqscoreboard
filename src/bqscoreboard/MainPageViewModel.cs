using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace bqscoreboard
{
    internal class MainPageViewModel : ViewModelBase
    {
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

        public ICommand ResetScoreboard => new Command(() =>
        {
            QuestionNumber = 1;
            QuestionSuffix = "";
            Team1Score = Team2Score = Team3Score = 0;

            Save();
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
        });

        public ICommand QuestionMinus1 => new Command(() => { QuestionNumber -= 1; Save(); });
        public ICommand QuestionPlus1 => new Command(() => { QuestionNumber += 1; Save(); });

        public ICommand Team1Minus10 => new Command(() => { Team1Score -= 10; Save(); });
        public ICommand Team1Plus10 => new Command(() => { Team1Score += 10; Save(); });

        public ICommand Team2Minus10 => new Command(() => { Team2Score -= 10; Save(); });
        public ICommand Team2Plus10 => new Command(() => { Team2Score += 10; Save(); });

        public ICommand Team3Minus10 => new Command(() => { Team3Score -= 10; Save(); });
        public ICommand Team3Plus10 => new Command(() => { Team3Score += 10; Save(); });

        public void Initialize()
        {
            Load();
            Save();
        }

        private void Load()
        {
            var p = Preferences.Default;
            QuestionNumber = p.Get<int>("QuestionNumber", 1);
            QuestionSuffix = p.Get<string>("QuestionSuffix", "");
            Team1Score = p.Get<int>("Team1Score", 0);
            Team2Score = p.Get<int>("Team2Score", 0);
            Team3Score = p.Get<int>("Team3Score", 0);
        }

        private void Save()
        {
            var p = Preferences.Default;
            p.Set<int>("QuestionNumber", QuestionNumber);
            p.Set<string>("QuestionSuffix", QuestionSuffix);
            p.Set<int>("Team1Score", Team1Score);
            p.Set<int>("Team2Score", Team2Score);
            p.Set<int>("Team3Score", Team3Score);
        }
    }
}
