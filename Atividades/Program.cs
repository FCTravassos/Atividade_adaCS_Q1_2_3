namespace Atividades
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Perguntando ao usuário o número de cidades
            uint numCidades;        // Armazena o número de cidades
            bool numCidadesValido;  // Verifica se a cidade informada é válida

            do
            {
                Console.Write("Digite o número de cidades: ");
                numCidadesValido = uint.TryParse(Console.ReadLine(), out numCidades);
                if (!numCidadesValido || numCidades == 1) Console.WriteLine("Valor inválido.");
            } while (!numCidadesValido || numCidades == 1);

            // Criando o array bidimensional que vai armazenar as distâncias entre as cidades
            uint[,] tabelaDistancias = new uint[numCidades, numCidades];    // Array iniciado em 0, não sendo necessário atribuir valores a sua diagonal principal
            bool distanciaValida;                                           // Variável utilizada para verificar se o valor digitado é um valor válido 

            // Preenchendo a tabela de distâncias no loop
            for (int i = 0; i < numCidades; i++)
            {
                for (int j = i + 1; j < numCidades; j++)
                {
                    Console.Write($"Digite as distâncias entre as cidades {i + 1} e {j + 1}: ");
                    distanciaValida = uint.TryParse(Console.ReadLine(), out tabelaDistancias[i, j]);

                    if (!distanciaValida) // Verificando se o valor digitado não é válido
                    {
                        Console.WriteLine("O valor digitado é inválido, tente novamente.");
                        j--;    // Decrementando j para permanecer no loop caso o valor digitado seja inválido
                    }
                    else        // Caso o valor seja válido, valor simetrico da matriz
                    {
                        tabelaDistancias[j, i] = tabelaDistancias[i, j];
                    }

                }
            }

            // Perguntando o percurso ao usuário
            Console.WriteLine($"{Environment.NewLine}Digite o percurso: ");
            int[] cidadesPercorridas = Console.ReadLine().Split(',').Select(int.Parse).ToArray();

            // Fazendo o cálculo da distância percorrida:
            uint distanciaPercorrida = 0;
            for (int i = 1; i < cidadesPercorridas.Length; i++)
            {
                distanciaPercorrida += tabelaDistancias[cidadesPercorridas[i - 1] - 1, cidadesPercorridas[i] - 1];
            }

            Console.WriteLine($"A distância total percorrida foi: {distanciaPercorrida} km");
        }
    }
}