
public class Program
{
    static void Main(string[] args)
    {
        string[] words = { "cccdad", "abcdc", "acbdc", "cdaabc", "dcdcc", "sdfw" };

        foreach (string word in words)
        {
            
            Console.WriteLine($"Слово: {word}");

            if (IsWord(/*Console.ReadLine()*/word))
            {
                Console.WriteLine("Слово распознано автоматом.");
            }
            else
            {
                Console.WriteLine("Слово не распознано автоматом.");
            }

            Console.WriteLine();
        }
        while(true)
        {
            Console.WriteLine("Введите слово");
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
        C,
        D, 
        DC,
        CC,
        DD,
        DCC,
        DDC,
        CCC,
        DCCC,
        DDCC,
        Final
    }

    private static readonly char[] alphabet = { 'a', 'b', 'c', 'd' };

    private static readonly int[,] matrix = new int[,]
    {

        {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
        {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11},
        {1, 4, 3, 6, 8, 7, 9, 10, 8, 9, 11, 11 },
        {2, 3, 5, 7, 6, 5, 10, 7, 9, 11, 10, 11},
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
        bool  enCountCCC = false;
        bool enCountDD = false;
        States stateCCC = States.CCC;
        States stateDD = States.DD;
        foreach (char symbol in word)
        {
            state = Transition(state, symbol);
            Console.WriteLine($"Буква: {symbol}; Состояние: {state}");

            if (state == stateCCC || state == States.DCCC || state == States.Final)
            {
                enCountCCC = true;
            }
            if (state == stateDD || state == States.DDC || state == States.Final)
            {
                enCountDD = true;
            }

            if (state == States.Final)
            {
                break;
            }
        }

        return enCountCCC  && enCountDD;
    }
}