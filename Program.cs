namespace ConsoleApp12;

internal static class Program
{
    private static void Main()
    {
        Random random = new Random();
        Console.WriteLine("Создаем коллекцию из 100 дробей");
        var collectionOfFractions = new Fraction[100];
        for (var i = 0; i < collectionOfFractions.Length; i++)
        {
            var fraction = new Fraction(random.Next(0, 100), random.Next(1, 100));
            collectionOfFractions[i] = fraction;
            Console.WriteLine(collectionOfFractions[i].ToString("F3"));
        }

        Console.WriteLine("Ищем целые числа...");
        foreach (var fraction in collectionOfFractions)
        {
            if (fraction._n == fraction._d)
            {
                Console.WriteLine($"{fraction.ToString("f3")} == {fraction._n.ToString()}");
            }
        }

    }

    public readonly struct Fraction : IComparable<Fraction>
    {
        public readonly int _n;
        public readonly int _d;

        public Fraction(int numerator, int denominator)
        {
            _n = numerator;
            _d = denominator;
        }

        public string ToString(string f3)
        {
            return $"{_n}/{_d}";
        }

        public int CompareTo(Fraction other)
        {
            var nComparison = _n.CompareTo(other._n);
            return nComparison != 0 ? nComparison : _d.CompareTo(other._d);
        }
    }
}