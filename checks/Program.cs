using Android_Ofek;
using System.Globalization;
CultureInfo.CurrentCulture = new CultureInfo("de-DE");
void Expect(string a, string op, string b, string expected) {
    if (!CalculatorMath.TryCalculate(a, op, b, out var actual) || actual != expected) throw new Exception($"{a}{op}{b}: {actual}");
}
Expect("0.5", "+", "1.25", "1.75"); Expect("5", "-", "7", "-2"); Expect("3", "*", "4", "12"); Expect("1", "/", "2", "0.5");
foreach (var pair in new[] { ("1", "/", "0"), (".", "+", "1"), ("NaN", "+", "1"), ("1e308", "*", "1e308"), ("1", "?", "2") })
    if (CalculatorMath.TryCalculate(pair.Item1, pair.Item2, pair.Item3, out _)) throw new Exception("Invalid calculation accepted");
Console.WriteLine("PASS: four operations, decimal locale, incomplete decimal, zero division, NaN, overflow, invalid operator.");
