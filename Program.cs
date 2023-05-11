using ANNFinal;

internal class Program
{
    static void PrintMatrix(double[,] matrix, out double value)
    {
        value = 0;
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                Console.Write(string.Format("{0} ", matrix[i, j]));
                value = matrix[i, j];
            }
            Console.Write(Environment.NewLine);
        }
    }

    static void PrintMatrix(int[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                Console.Write(string.Format("{0} ", matrix[i, j]));
            }
            Console.Write(Environment.NewLine);
        }
    }

    static void Main(string[] args)
    {
        var monstersDefeated = 0;
        var playing = true;
        Random R = new Random();

        var playerNeuralNetwork = new NeuralNetWork(1, 6);
        var monsterNeuralNetwork = new NeuralNetWork(1, 6);

        var trainingInputs = new double[,] {
                { 0, 1, 1, 0, 1, 0 },
                { 0, 0, 0, 1, 0, 0 },
                { 1, 1, 1, 0, 0, 1 },
                { 0, 1, 0, 1, 0, 1 }};
        var trainingOutputs = NeuralNetWork.MatrixTranspose(new double[,] { { 1, 1, 1, 1 } });

        playerNeuralNetwork.Train(trainingInputs, trainingOutputs, 100000);

        trainingInputs = new double[,] {
                { 1, 0, 0, 0, 1, 0 },
                { 0, 1, 0, 1, 0, 1 },
                { 0, 0, 1, 0, 1, 0 },
                { 0, 1, 0, 0, 0, 1 }};
        trainingOutputs = NeuralNetWork.MatrixTranspose(new double[,] { { 0, 0, 0, 1 } });

        monsterNeuralNetwork.Train(trainingInputs, trainingOutputs, 100000);

        while (playing)
        {
            var tempList = new List<double>();

            for (int i = 0; i < 6; i++)
            {
                tempList.Add(R.Next(0, 2));
            }

            var newInput = tempList.ToArray();

            var playerOutput = playerNeuralNetwork.Think(new double[,] { { newInput[0], newInput[1], newInput[2], newInput[3], newInput[4], newInput[5] } });
            var monsterOutput = monsterNeuralNetwork.Think(new double[,] { { newInput[0], newInput[1], newInput[2], newInput[3], newInput[4], newInput[5] } });
            
            Console.WriteLine($"\nConsidering new problem [{newInput[0]}, {newInput[1]}, {newInput[2]}, {newInput[3]}, {newInput[4]}, {newInput[5]}] => :");
            
            Console.Write("Player: ");
            PrintMatrix(playerOutput, out double player);

            Console.Write("Monster: ");
            PrintMatrix(monsterOutput, out double monster);

            if (player < monster)
            {
                playing = false;
                Console.WriteLine("The player lost!");
            }
            else
            {
                
                Console.WriteLine($"The player won! They have defeated {++monstersDefeated} monsters so far!");
            }
        }

    }
}