using System;
using System.IO;
using System.Threading.Tasks;

namespace GestionIO
{
    class Program
    {
       async static Task Main(string[] args)
       {
            //Funzioni.CreazioneDirectiory();

            //Funzioni.CreazioneDirectiory2();

            //Funzioni.EliminareCartelle();

            //Funzioni.EliminareCartella2();

            // Funzioni.CreazSottocartella();
            // Funzioni.SpostareCartella();

            //Funzioni.Spostarecartella2();
            //Funzioni.SpostareCartella();
            //Funzioni.RicercaCartella();

            // per farlo funzioanre devo rendere asincorno anche il main -> async static void Task main
            //await Funzioni.LetturaScritturaFileAsync(); // aggiungo await è usato in copia ad async: asy è una funzionalità per
            //cui si può non aspettare la fine per effettuare altre operazione

            //await Funzioni.LetturaScritturaFileAsync2();

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Employees.csv");
            // Funzioni.ConvertJSON(path);
            //Funzioni.ConverttotoXML(path);
            Funzioni.GetProcessoreName();

       }
    }
}
