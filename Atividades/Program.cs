namespace Atividades
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Armazenando caminho para o desktop
            string  diretorioDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            
            string  localMatriz  = diretorioDesktop + @"\matriz.txt";   // Endereço da matriz
            string  localCaminho = diretorioDesktop + @"\caminho.txt";  // Endereço do caminho

            // Verificando se os arquivos necessários estão no desktop
            bool arquivoExiste = File.Exists(localMatriz) && File.Exists(localCaminho);  

            if (arquivoExiste)
            {
                // Lendo o arquvio de distâncias, subistuindo as quebras de linha por ',' para usar o split para separar elas
                uint[] vetorDistancias = File.ReadAllText(localMatriz).Replace(Environment.NewLine,",").Split(",").Select(uint.Parse).ToArray();
                // Como a matriz sempre tem que ser quadrada, fazendo o cálculo da raiz quadrada para saber o número de cidades
                var  numCidades = (uint)Math.Sqrt(vetorDistancias.Length);

                uint[,] tabelaDistancias = new uint[numCidades, numCidades];

                // Percorrendo o vetor de distâncias para preencher a tabela
                for (int i = 0; i < vetorDistancias.Length; i++)
                    tabelaDistancias[i / numCidades, i % numCidades] = vetorDistancias[i];

                // Lendo a trajetória
                uint[] cidadesPercorridas = File.ReadAllText(localCaminho).Split(",").Select(uint.Parse).ToArray();
                uint distanciaPercorrida = 0;

                // Calculando a distância
                for (int i = 1; i < cidadesPercorridas.Length; i++)
                    distanciaPercorrida += tabelaDistancias[cidadesPercorridas[i - 1] - 1, cidadesPercorridas[i] - 1];

                Console.WriteLine($"A distância total percorrida foi: {distanciaPercorrida} km");
            }
            else // Caso o arquivo não exista, exibir mensagem de erro
            {
                Console.WriteLine("O arquivo não existe.");
            }
        }
    }
}