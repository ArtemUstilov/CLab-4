using System;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Models
{
    [Serializable]
    class ZodiacChinese
    {
        private readonly string _name;
        private readonly bool _female;

        public ZodiacChinese(string name, bool female)
        {
            _name = name;
            _female = female;
        }

        public string Name()
        {
            return _name;
        }

        public bool Female()
        {
            return _female;
        }
    }
    [Serializable]
    class ZodiacWest
    {
        private readonly string _name;
        private readonly int _dayBeg;

        public ZodiacWest(string name, int day)
        {
            _name = name;
            _dayBeg = day;
        }

        public string Name()
        {
            return _name;
        }

        public int DayBeg()
        {
            return _dayBeg;
        }
    }
    [Serializable]
    class Element
    {
        private readonly string _adjBase;

        public Element(string adjBase)
        {
            _adjBase = adjBase;
        }

        public string FemaleAdj()
        {
            return _adjBase + "а";
        }

        public string MaleAdj()
        {
            return _adjBase + "ий";
        }
    }
    [Serializable]
    static class ZodiacSignsInfo
    {
        private static ZodiacWest[] _sunSigns =
        {
                    new ZodiacWest("Водолій", 20),
                    new ZodiacWest("Риби", 19),
                    new ZodiacWest("Овен", 21),
                    new ZodiacWest("Тілець", 20),
                    new ZodiacWest("Близнюки", 21),
                    new ZodiacWest("Рак", 21),
                    new ZodiacWest("Лев", 23),
                    new ZodiacWest("Діва", 23),
                    new ZodiacWest("Терези", 23),
                    new ZodiacWest("Скорпіон", 23),
                    new ZodiacWest("Стрілець", 22),
                    new ZodiacWest("Козеріг", 22)
                };

        public static ZodiacWest[] SunSigns => _sunSigns;

        private static ZodiacChinese[] _chineseSigns =
        {
                    new ZodiacChinese("Миша", true),
                    new ZodiacChinese("Бик", false),
                    new ZodiacChinese("Тигр", false),
                    new ZodiacChinese("Кіт", false),
                    new ZodiacChinese("Дракон", false),
                    new ZodiacChinese("Змія", true),
                    new ZodiacChinese("Кінь", false),
                    new ZodiacChinese("Коза", true),
                    new ZodiacChinese("Мавпа", true),
                    new ZodiacChinese("Півень", false),
                    new ZodiacChinese("Собака", false),
                    new ZodiacChinese("Свиня", true)
                };

        public static ZodiacChinese[] ChineseSigns => _chineseSigns;
        private static Element[] _elements =
        {
                    new Element("Металев"),
                    new Element("Водян"),
                    new Element("Дерев'ян"),
                    new Element("Вогнян"),
                    new Element("Землян")
                };

        public static Element[] Elements => _elements;
    }
}
