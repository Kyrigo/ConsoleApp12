using System.Xml;
using System.Xml.Serialization;

namespace ConsoleApp12;

public static class Program
{
    public static void Main()
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

        //сами себе злобные буратины так что конвертируем массив дробей в массив строк
        var collectionOfFractionsString = collectionOfFractions.Select(x => x.ToString("F3")).ToArray();
        
        Console.WriteLine("Пишем коллекцию в файл");
        // File.WriteAllLines("array.txt", collectionOfFractionsString);
        XmlSerializer serializer;
        using (var stream = File.Create("array.xml"))
        {
            serializer = new XmlSerializer(typeof(string[]));
            serializer.Serialize(stream, collectionOfFractionsString);
        }
        
        Console.WriteLine("Воротаем коллекцию из файла");
        var xml = File.ReadAllText("array.xml");
        serializer = new XmlSerializer(typeof(StringFraction));
        var rdr = new StringReader(xml);
        var result = (StringFraction)serializer.Deserialize(rdr)!;
        Console.WriteLine(string.Join(", ", result.Fraction));
        
        Console.WriteLine("Ищем целые числа...");
        foreach (var fraction in collectionOfFractions)
        {
            if (fraction._n == fraction._d)
            {
                Console.WriteLine($"{fraction.ToString("f3")} == {fraction._n.ToString()}");
            }
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

[Serializable, XmlRoot("ArrayOfString")]
public class StringFraction
{
    [XmlElement("string")]
    public string[] Fraction { get; set; }

    public StringFraction()
    {
        Fraction = new string[100];
    }
}