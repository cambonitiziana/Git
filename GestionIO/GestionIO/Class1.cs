using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace GestionIO
{
    public class Funzioni
    {
        #region CARTELLE

        //creazione directory 
        // cartellaAcademy
        public static void CreazioneDirectiory() //METODO 1
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CartellaAcademy");// creiamo il path 
                                                                                                                            //sto aggiungendo a questo percorso la cartella 
                                                                                                                            //environment è una classe che ha all'interno dei metodi per rihciamerel'ambiente windows
            DirectoryInfo Directory = new DirectoryInfo(path); //ora creaimao un oggetto di tipo directoryinfo  che ha delle funzioni per cui richiamate può creare un folder nel filesystem
            // gli diamo una stringa

            try
            {
                Directory.Create();
                Console.WriteLine("la cartella è stata creata");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public static void CreazioneDirectiory2() //METODO 2- sfrutto un tipo
        {
            try
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CartellaAcademy2"));
                Console.WriteLine("la cartella è stata creata");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //Eliminare cartelle
        public static void EliminareCartelle()
        {
            //utilizzo tipo directoryinfo
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CartellaAcademy");
            DirectoryInfo directory = new DirectoryInfo(path);

            try
            {
                directory.Delete();
                Console.WriteLine("Cartella Eliminata Correttamente");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public static void EliminareCartella2()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CartellaAcademy2");


            try
            {
                Directory.Delete(path);
                Console.WriteLine("Cartella Eliminata Correttamente");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //creazioen cartella e sottocartella - ed eliminazione
        public static void CreazSottocartella()
        {
            DirectoryInfo directory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Pippo"));

            try
            {
                directory.Create();
                directory.CreateSubdirectory("Pluto");

                Console.WriteLine("Cartell e sottocartella sono state create! premi un tasto per eliminarle");
                Console.ReadLine();

                directory.Delete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //spostare cartella
        public static void SpostareCartella()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CartellaDaprogramma");
            DirectoryInfo directory = new DirectoryInfo(path);

            try
            {
                directory.Create();
                Console.WriteLine("la cartella è stata creata, premi un tasto per spostarla");
                Console.ReadLine();
                directory.MoveTo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures))); //deve sapere come è il path alla fine dello spostamento 
                Console.WriteLine("la cartella è stata creata");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void Spostarecartella2()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CartellaDaProgramma2");
            string imagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "CartellaDaProgramma2");
            DirectoryInfo directory = new DirectoryInfo(path);

            try
            {

                directory.Create();
                Console.WriteLine("Cartela creata");
                Console.ReadLine();

                if (!Directory.Exists(imagePath)) // se la directory che corrisposnte al path imagepath allora faccio lo spostamento
                {
                    directory.MoveTo(imagePath);
                    Console.WriteLine("Cartella spostata");
                }
                else
                {
                    Console.WriteLine("Cartella gia esistente");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public static void CopiaCartella()
        {
            DirectoryInfo[] directories = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)).GetDirectories(); //Array di tipi DirectoryInfo
                                                                                                                                               //Array di cartelle che sono le sotto cartelle della cartella madre MyPicture

            try
            {
                foreach (DirectoryInfo directory in directories)
                {
                    string copypath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), string.Format("{0} copia"), directory.Name); //dove copio
                    //prendi il nome della carte che stai copiando e aggiungi copia
                    Directory.CreateDirectory(copypath);
                    Console.WriteLine("{0} copiata correttamente");

                    //bisogna copiare anche i file
                    FileInfo[] files = directory.GetFiles(); //prendo tutti i file
                    foreach (FileInfo file in files)
                    {
                        file.CopyTo(Path.Combine(copypath, string.Format("{0} copia ", file.Name)));

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //ricerca cartelle
        public static void RicercaCartella()
        {
            DirectoryInfo directory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            try
            {
                DirectoryInfo[] directories = directory.GetDirectories("*", SearchOption.AllDirectories);
                foreach (DirectoryInfo dir in directories)
                {
                    Console.WriteLine("{0} {1}", dir.FullName, dir.LastAccessTime);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region FILE
        // mantiene aperto il file finche non termina lo scopo 
        // Azione sincrona: dobbiamo apsettare la fine di lettura e scrittura prima di poter fare altre operazioni di interfaccia
        //processo asincrono: non dobbiamo apsettare la fine di lettura e scrittura prima di poter fare altre operazioni di interfaccia

        public async static Task LetturaScritturaFileAsync() // se è asincrono bisogna sempre mettere la keyWork TASK che il ritorno
        {
            try
            {

                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Testo.txt");// faccio path
                                                                                                                              //per utilizzare filestrem devo usare la keyword using e dentro indico la validità 
                using (FileStream file = File.Create(filePath)) // ho creato una variabile di processo a cui posso acedere solo dentro lo using
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("ciao!!! sto scrivendo in un file \n sono andata a capo");//abbiamo bisogno di un array di byte; stiamo strasformando una stringa in una sequenza di byte
                                                                                                                            //con UTF8 gli stiamo dicendo che stiamo usando i caratteri "latini(?)
                    await file.WriteAsync(info, 0, info.Length);
                    // file.Write()  metodo sincrono
                    file.Close(); //è buona norma dichiare di chiude la connessione


                    using (StreamReader reader = File.OpenText(filePath))  //possiamo sare using uno dentro l'altro
                    {
                        //ex. per ogni linea possiamo leggere la linea e metterla a console.
                        //Come capiamo quando ha finito? 
                        while (reader.Peek() > -1) // finchè il file non ha fine; il peek serve per segnare a che linea del file si è
                        {
                            Console.WriteLine("il file contiene: {0}", reader.ReadLine());
                        }
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async static Task LetturaScritturaFileAsync2()
        {
            //piu step: 
            //creo cartella + sottocarte con file


            DirectoryInfo directory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "ProvaLetturascrittura"));
            try
            {

                //creazione cartelle
                directory.Create();
                Console.WriteLine(" cartella creata");
                directory.CreateSubdirectory("ProvaScrittura");
                Console.WriteLine("Subdirectory creata correttamente");

                //creazionefile
                string path = Path.Combine(directory.FullName, "ProvaScrittura/SequenzaNumeri.txt"); //parti da directory 
                using (StreamWriter file = File.CreateText(path))//uso stramwriter per scrivere nel file
                {
                    for (int i = 0; i <= 10; i++)
                    {
                        await file.WriteAsync(i.ToString() + "\n");// conversione in stringa
                    }

                }
                using (StreamWriter file = File.AppendText(path))// se avessi lascito create avrebbe sovrascritto, in questo modo aggiungo
                {
                    for (int i = 10; i <= 20; i++)
                    {
                        await file.WriteAsync(i.ToString() + "\n");// conversione in stringa
                    }
                }

                // letturafile
                string line;
                int counter = 0; //mi dice a che riga sono

                using (StreamReader fileReader = File.OpenText(path))
                {
                    while ((line = fileReader.ReadLine()) != null) // mostro a schermo la riga
                    {
                        Console.WriteLine(line);
                        counter++;

                    }
                    line = ""; //resetto la riga
                    fileReader.BaseStream.Position = 0;

                    //lettura totale del test
                    line = fileReader.ReadToEnd();
                    Console.WriteLine(line);  // mi mette tutto in una riga


                    //split
                    string[] linesplit = line.Split("\n"); //divide in base ad una separatore

                    foreach (string el in linesplit)
                    {
                        Console.Write(el + " ");
                    }

                }


            }
            catch (Exception e)
            { Console.WriteLine(e.Message); }
        }
        #endregion

        #region JSON + XML
        public static void ConvertJSON(string path)
        {


            if (File.Exists(path))
            {
                try
                {
                    int totalLines = File.ReadLines(path).Count();
                    string jsonPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Employees.json");
                    using (StreamReader fileReader = File.OpenText(path))
                    {
                        string title = Path.GetFileNameWithoutExtension(path).ToLower();
                        string line;
                        string[] titles = new string[] { };
                        int counter = 0;

                        //using (StreamWriter fileWriter = File.CreateText(jsonPath))
                        using (StreamWriter fileWriter = File.CreateText(jsonPath + "/" + title + ".json"))
                        {

                            //apriamo le parentesi
                            fileWriter.WriteLine("{");
                            fileWriter.WriteLine("\"" + title + "\":[");

                            while ((line = fileReader.ReadLine()) != null)
                            {
                                if (counter == 0)
                                {
                                    line = line.ToLower();
                                    titles = line.Split(",");
                                    counter++;
                                }

                                else
                                {
                                    string[] data = new string[] { };
                                    data = line.Split(",");

                                    fileWriter.Write("{");
                                    for (int i = 0; i < titles.Length; i++)
                                    {
                                        if (i == title.Length - 1) // se è l'ultimo
                                        {
                                            fileWriter.Write("\"" + titles[i] + "\":\"" + data[i] + "\"");
                                        }
                                        else
                                        {
                                            fileWriter.Write("\"" + titles[i] + "\":\"" + data[i] + "\",");
                                        }
                                    }

                                    if (counter != totalLines - 1) // se non è l'ultima riga
                                    {
                                        fileWriter.Write("},\n");
                                    }
                                    else
                                    {
                                        fileWriter.Write("}\n");
                                    }
                                }
                            }

                            fileWriter.WriteLine("]}");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }








            }
        }

        public static void ConverttotoXML(string path)
        {

            string pathXML = Path.GetDirectoryName(path); //sto prendendo il path del file originale e prendo anche il nome
            string title = Path.GetFileNameWithoutExtension(path).ToLower();
            string line;
            string[] titles = new string[] { };
            int counter = 0;

            using (StreamReader file = File.OpenText(path))
            {
                using (StreamWriter filewriter = File.CreateText(pathXML + "/" + title + ".xml"))
                {
                    filewriter.WriteLine("<" + title + ">");

                    while ((line = file.ReadLine()) != null)
                    {
                        if (counter == 0) //caso prima riga, con i titoli
                        {
                            line = line.ToLower();
                            titles = line.Split(",");
                            counter++; //aggiorno il contatore per andare all'altra riga
                        }
                        else //sono in una riga con i dati
                        {
                            string[] data = new string[] { };
                            data = line.Split(",");
                            string titleSingle = title.Substring(0, title.Length - 1); // in titlesingle ho employee (senza la s)
                            filewriter.WriteLine("\t<" + titleSingle + ">");


                            for (int i = 0; i < title.Length-1; i++)
                            {
                                filewriter.WriteLine("\t\t<" + titles[i] + ">" + data[i] + "</" + titles[i] + ">");
                            }
                            filewriter.WriteLine("\t</" + titleSingle + ">");
                            counter++;

                        }

                    }
                    filewriter.WriteLine("</" + title + ">");


                }



            }



         

        }

        #endregion

        #region Registry

        public static void GetProcessoreName()
        {
            RegistryKey registrykey = Registry.LocalMachine;
            registrykey = registrykey.OpenSubKey("HARDWARE\\\\DESCRIPTION\\\\System\\\\CentralProcessor\\\\0");

            object value=  registrykey.GetValue("ProcessorNameString"); //ricavo valore "ProcessoreNameString" è la chiave che gli diamo in ingresso
            // il tipo di ritorno è un object quindi mettiamo davanti un obkect 
           
            Console.WriteLine("il tuo processore è:" + value);
        }


        #endregion



    }
}
