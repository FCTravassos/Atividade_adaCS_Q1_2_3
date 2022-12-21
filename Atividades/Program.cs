using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Atividades
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            // Armazenando caminho para o desktop
            string  diretorioDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            
            string  localMatriz  = diretorioDesktop + @"\matriz.txt";   // Endereço da matriz
            string  localCaminho = diretorioDesktop + @"\caminho.txt";  // Endereço do caminho


            using var reader = new StreamReader(localMatriz);
            using var csv = new CsvParser(reader, config);

            if (csv.Read())
            {
                var  numCidades = csv.Record.Length;
                uint[,] tabelaDistancias = new uint[numCidades, numCidades];

                // Percorrendo o vetor de distâncias para preencher a tabela
                for(int i = 0; i < numCidades; i++)
                {
                    var posRelativa = csv.Record;   // vetor com as posições de cada cidade em relação a cidade i
                    for(int j = 0; j < numCidades; j++)
                    {
                        tabelaDistancias[i, j] = uint.Parse(posRelativa[j]);
                    }
                    csv.Read();
                }

                // Lendo a trajetória
                using var readerCaminho = new StreamReader(localCaminho);
                using var csvCaminho = new CsvParser(readerCaminho, config);

                if (csvCaminho.Read())
                {
                    uint[] cidadesPercorridas = csvCaminho.Record.Select(uint.Parse).ToArray();

                    uint distanciaPercorrida = 0;

                    // Calculando a distância
                    for (int i = 1; i < cidadesPercorridas.Length; i++)
                        distanciaPercorrida += tabelaDistancias[cidadesPercorridas[i - 1] - 1, cidadesPercorridas[i] - 1];

                    Console.WriteLine($"A distância total percorrida foi: {distanciaPercorrida} km");
                }
            }
            else // Caso o arquivo não exista, exibir mensagem de erro
            {
                Console.WriteLine("O arquivo não existe.");
            }
        }
    }
}