
public class Program
{
    static void Main(string[] args)
    {
        string[] words = { "cccdad", "abcdc", "acbdc", "cdaabc", "dcdcc", "sdfw" };

        foreach (string word in words)
        {
            Console.WriteLine("Введите слово");
            Console.WriteLine($"Слово: {word}");

            if (IsWord(Console.ReadLine()))
            {
                Console.WriteLine("Слово распознано автоматом.");
            }
            else
            {
                Console.WriteLine("Слово не распознано автоматом.");
            }

            Console.WriteLine();
        }
    }
    private enum States
    {
        Start,
        A, 
        B,
        C,
        D, 
        CC,
        CCC,
        DD,
        Final
    }

    private static readonly char[] alphabet = { 'a', 'b', 'c', 'd' };

    private static readonly int[,] matrix = new int[,]
    {
        {1, 1, 2, 3, 4, 3, 3, 4, 8},
        {2, 1, 2, 3, 4, 3, 3, 4, 8},
        {3, 1, 2, 5, 3, 6, 6, 3, 8},
        {4, 1, 2, 4, 7, 4, 4, 7, 8},
    };

    private static States Transition(States currentState, char input)
    {
        int inputIndex = Array.IndexOf(alphabet, input);
        if (inputIndex == -1)
        {
            Console.WriteLine("Буква остуствует в алфавите(только a,b,c,d)");
            return States.Final;
        }

        return (States)matrix[inputIndex, (int)currentState];
    }

    public static bool IsWord(string word)
    {
        States state = States.Start;
        int countC = 0;
        int countD = 0;

        foreach (char symbol in word)
        {
            state = Transition(state, symbol);
            Console.WriteLine($"Буква: {symbol}; Состояние: {state}");

            if (symbol == 'c')
            {
                countC++;
            }
            else if (symbol == 'd')
            {
                countD++;
            }

            if (state == States.Final)
            {
                break;
            }
        }

        return countC == 3 && countD == 2;
    }
}