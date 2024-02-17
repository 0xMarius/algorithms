// TAOCP SECTION 1.1
// Algorithm F (Euclid's algorithm). Given two positive longegers m and n, find their greatest common divisor

try
{
    Console.Write("Enter dividend: ");
    ulong m = ulong.Parse(Console.ReadLine());

    Console.Write("Enter divisor: ");
    ulong n = ulong.Parse(Console.ReadLine());
    
    m = euclidLoop(m, n);

    Console.WriteLine("Algorithm terminated, greatest common divisor: {0}", m);
}
catch (FormatException e)
{
    Console.WriteLine(e);
}
static ulong euclidLoop(ulong m, ulong n)
{
    ulong cycleCounter = 0;

    // F0. [Ensure m >= n.] If m < n, quotient will always be 0, therefore exchange m <-> n
    if (m < n)
    {
        ulong temp = m;
        m = n;
        n = temp;
    }

    while (m != 0 || n != 0)
    {
        // F1. [Remainder m/n.] Divide m by n and let m be the remainder.
        m %= n;
        cycleCounter++;

        // F2. [Is it zero?] If m = 0, the algorithm terminates.
        if (m == 0)
        {
            m = n;
            break;
        }

        Console.WriteLine("Cycle {0}; Remainder: {1}", cycleCounter, m);
        // F3. [Remainder n/m.] Divide n by m and let n be the remainder.
        n %= m;
        cycleCounter++;

        // F4. [Is it zero?] If n = 0, the algorithm terminates.
        if (n == 0)
        {
            break;
        }

        Console.WriteLine("Cycle {0}; Remainder: {1}", cycleCounter, n);
    }

    return m;
}