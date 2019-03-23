using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Windows;


namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Models
{
    [Serializable]
    public class User
    {
        #region Fields
        private Guid _guid;
        private DateTime _birthday;
        private Element _element;
        private ZodiacWest _zodiac;
        private ZodiacChinese _zodiacChinese;
        private string _name;
        public static readonly string[] Names = {"Aaran", "Aaren", "Aarez", "Aarman", "Aaron", "Aaron-James", "Aarron", "Aaryan", "Aaryn", "Aayan", "Aazaan", "Abaan", "Abbas", "Abdallah", "Abdalroof", "Abdihakim", "Abdirahman", "Abdisalam", "Abdul", "Abdul-Aziz", "Abdulbasir", "Abdulkadir", "Abdulkarem", "Abdulkhader", "Abdullah", "Abdul-Majeed", "Abdulmalik", "Abdul-Rehman", "Abdur", "Abdurraheem", "Abdur-Rahman", "Abdur-Rehmaan", "Abel", "Abhinav", "Abhisumant", "Abid", "Abir", "Abraham", "Abu", "Abubakar", "Ace", "Adain", "Adam", "Adam-James", "Addison", "Addisson", "Adegbola", "Adegbolahan", "Aden", "Adenn", "Adie", "Adil", "Aditya", "Adnan", "Adrian", "Adrien", "Aedan", "Aedin", "Aedyn", "Aeron", "Afonso", "Ahmad", "Ahmed", "Ahmed-Aziz", "Ahoua", "Ahtasham", "Aiadan", "Aidan", "Aiden", "Aiden-Jack", "Aiden-Vee", "Aidian", "Aidy", "Ailin", "Aiman", "Ainsley", "Ainslie", "Airen", "Airidas", "Airlie", "AJ", "Ajay", "A-Jay", "Ajayraj", "Akan", "Akram", "Al", "Ala", "Alan", "Alanas", "Alasdair", "Alastair", "Alber", "Albert", "Albie", "Aldred", "Alec", "Aled", "Aleem", "Aleksandar", "Aleksander", "Aleksandr", "Aleksandrs", "Alekzander", "Alessandro", "Alessio", "Alex", "Alexander", "Alexei", "Alexx", "Alexzander", "Alf", "Alfee", "Alfie", "Alfred", "Alfy", "Alhaji", "Al-Hassan", "Ali", "Aliekber", "Alieu", "Alihaider", "Alisdair", "Alishan", "Alistair", "Alistar", "Alister", "Aliyaan", "Allan", "Allan-Laiton", "Allen", "Allesandro", "Allister", "Ally", "Alphonse", "Altyiab", "Alum", "Alvern", "Alvin", "Alyas", "Amaan", "Aman", "Amani", "Ambanimoh", "Ameer", "Amgad", "Ami", "Amin", "Amir", "Ammaar"};
        public static readonly string[] Lastnames = { "Anderson", "Ashwoon", "Aikin", "Bateman", "Bongard", "Bowers", "Boyd", "Cannon", "Cast", "Deitz", "Dewalt", "Ebner", "Frick", "Hancock", "Haworth", "Hesch", "Hoffman", "Kassing", "Knutson", "Lawless", "Lawicki", "Mccord", "McCormack", "Miller", "Myers", "Nugent", "Ortiz", "Orwig", "Ory", "Paiser", "Pak", "Pettigrew", "Quinn", "Quizoz", "Ramachandran", "Resnick", "Sagar", "Schickowski", "Schiebel", "Sellon", "Severson", "Shaffer", "Solberg", "Soloman", "Sonderling", "Soukup", "Soulis", "Stahl", "Sweeney", "Tandy", "Trebil" };
        public static DateTime RandomDateBirth()
        {
            Random rnd = new Random();
            return new DateTime(
                rnd.Next(1910, 2018),
                rnd.Next(1,12),
                rnd.Next(1,28));
        }
        #endregion

        #region Properties
        public Guid Guid
        {
            get => _guid;
            private set => _guid = value;
        }
        
       
        public DateTime BirthDay
        {
            get => _birthday;
            set
            {
                _birthday = value;
                FindChineseSign();
                FindElement();
                FindSunSign();
            }
        }

        #endregion

        #region Constructor

        public User(string firstName, string lastName, string email, DateTime birthday)
        {
            _guid = Guid.NewGuid();
            Name = firstName;
            Surname = lastName;
            _birthday = birthday;
            Email = email;
            Age = CountAge();
            ValidateAge();
            ValidateName();
            ValidateEmail();
            ValidateSurname();
            FindChineseSign();
            FindElement();
            FindSunSign();
        }
        #endregion

        private int Age { get; set; }

        public string Name
        {
            get { return _name;}
            set
            {
                _name = value;
                ValidateName();
            }
        }

        public string Surname { get; set; }

        public string Email { get; set;  }

        private void ValidateAge()
        {
            if (Age > 135)
            {
                MessageBox.Show("You might be too old");

                throw new TooOldPersonException(Age);
            }

            if (Age < 0)
            {
                MessageBox.Show("You haven`t born yet");
                throw new TooYoungPersonException(Age);
            }
        }

        private void ValidateName()
        {
            Regex reg = new Regex("^[A-Za-z-]{2,15}$");
            if (!reg.IsMatch(Name))
            {
                throw new InvalidNamePersonException(Name);
             }
        }

        private void ValidateEmail()
        {
            if (!String.IsNullOrEmpty(Email) && !new EmailAddressAttribute().IsValid(Email))
            {
                throw new InvalidEmailPersonException(Email);
            }

        }

        private void ValidateSurname()
        {
            var reg = new Regex("^[A-Za-z]{2,18}$");
            if (!reg.IsMatch(Surname)) {
                throw new InvalidSurNamePersonException(Surname);
            }
        }

        public string IsBirthday
        {
            get
            {
                var today = DateTime.Today;
                return (_birthday.Day == today.Day && _birthday.Month == today.Month) ? "Так" : "Ні"; ;
            }
        }

        private int CountAge()
        {
            var today = DateTime.Today;
            Age = today.Year - _birthday.Year;
            if (today.Month < _birthday.Month ||
                (today.Month == _birthday.Month && today.Day < _birthday.Day))
                Age--;
            return Age;
        }

        public string IsAdult => Age >= 18 ? "Повнолітній(я)" : "Дитина";

        private void FindSunSign()
        {
            int zodiacInd = _birthday.Month - 1;
            if (_birthday.Day < ZodiacSignsInfo.SunSigns[zodiacInd].DayBeg())
                zodiacInd = (zodiacInd == 0) ? 11 : zodiacInd - 1;
            _zodiac = ZodiacSignsInfo.SunSigns[zodiacInd];
        }

        private void FindChineseSign()
        {
            int zodiacInd = (_birthday.Year + 8) % 12;
            _zodiacChinese = ZodiacSignsInfo.ChineseSigns[zodiacInd];
        }

        private void FindElement()
        {
            int elementInd = (_birthday.Year) % 10 / 2;
            _element = ZodiacSignsInfo.Elements[elementInd];
        }

        public string ChineseSign
        {
            get
            {
                if (_zodiacChinese == null)
                    return "";
                var adjective = _zodiacChinese.Female() ? _element.FemaleAdj() : _element.MaleAdj();
                return adjective + " " + _zodiacChinese.Name();
            }
        }

        public string SunSign => _zodiac != null ? _zodiac.Name() : "";
        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }
    [Serializable]
    class TooOldPersonException : Exception
    {
        public TooOldPersonException()
        {

        }

        public TooOldPersonException(int age)
            : base($"You must have died, {age} years impossible to live")
        {

        }

    }

    [Serializable]
    class TooYoungPersonException : Exception
    {
        public TooYoungPersonException()
        {

        }

        public TooYoungPersonException(int age)
            : base($"You haven`t born yet: {age} years")
        {

        }

    }

    [Serializable]
    class InvalidNamePersonException : Exception
    {
        public InvalidNamePersonException()
        {

        }

        public InvalidNamePersonException(string name)
            : base($"It can`t be your real name: {name}")
        {

        }

    }

    [Serializable]
    class InvalidSurNamePersonException : Exception
    {
        public InvalidSurNamePersonException()
        {

        }

        public InvalidSurNamePersonException(string name)
            : base($"It can`t be your real surname: {name}")
        {

        }

    }

    [Serializable]
    class InvalidEmailPersonException : Exception
    {
        public InvalidEmailPersonException()
        {

        }

        public InvalidEmailPersonException(string email)
            : base($"Invalid email address: {email}")
        {

        }

    }
}