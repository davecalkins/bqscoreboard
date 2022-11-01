using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bqscoreboard
{
    internal class MainPageViewModel : ViewModelBase
    {
        public string Greeting
        {
            get => model.Greeting;
            set => SetProperty(ref model.Greeting, value);
        }

        private BQScores model = new();

        public void Initialize()
        {
        }
    }
}
