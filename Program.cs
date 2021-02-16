using System;
using System.Text;
using System.IO;
using System.Collections.Specialized;

namespace ConsoleApp8
{
    class Program
    {


        static void Main(string[] args)
        {
            DirCreator();
            bool end = false;
            string currentFolder = @"\LinuxEmulator";
            currentFolder.Remove(0);
            Console.WriteLine("Enter 'help' to open command list");
            while (!end)
            {
                string fullCommand;
                string currentCommand = @"/";//команда
                string currentCharacter1 = @"/";//первая часть между currentCommand и currentCharacter2
                string currentCharacter2 = @"/";//3 позиция
                Console.Write($"LEconsole : ~{currentFolder}$ ");

                fullCommand = Console.ReadLine();
                bool characterType = true;
                bool firstSymbol = true;
                bool characterType2 = false;
                for (int i = 0; i < fullCommand.Length; i++)//разделение по переменным команду пользователя
                {
                    if (i == 0)
                    {
                        firstSymbol = true;
                    }

                    if (fullCommand[i] == ' ' && characterType == true)
                    {
                        characterType = false;
                        firstSymbol = true;
                    }
                    else if (fullCommand[i] == ' ' && characterType == false)
                    {
                        characterType2 = true;
                        firstSymbol = true;
                    }
                    if (characterType && fullCommand[i] != ' ')//заполнение currentCommand
                    {
                        char what = fullCommand[i];

                        if (firstSymbol)
                        {
                            currentCommand = Convert.ToString(what);
                            firstSymbol = false;
                        }
                        else
                        {
                            currentCommand += Convert.ToString(what);
                        }

                    }
                    else if (!characterType && !characterType2 && fullCommand[i] != ' ')//заполнение со 2 позиции
                    {
                        char what = fullCommand[i];

                        if (firstSymbol)
                        {
                            currentCharacter1 = Convert.ToString(what);
                            firstSymbol = false;
                        }
                        else
                        {
                            currentCharacter1 += Convert.ToString(what);
                        }
                    }
                    else if (!characterType && characterType2 && fullCommand[i] != ' ')//заполнение 3 позиции
                    {
                        char what = fullCommand[i];

                        if (firstSymbol)
                        {
                            currentCharacter2 = Convert.ToString(what);
                            firstSymbol = false;
                        }
                        else
                        {
                            currentCharacter2 += Convert.ToString(what);
                        }
                    }
                }
                if (currentCommand == "help")//список команд
                {
                    Console.WriteLine("ls - вывести список файлов в текущем каталоге.");//+
                    Console.WriteLine("wpd [dir name] - вывести путь к текущей папке в консоли");//+
                    Console.WriteLine("cd [directory name] - переход в выбранную папку текущего каталога, если она существует.");//+
                    Console.WriteLine("cd - возврат к предыдущему каталогу.");//+
                    Console.WriteLine("mkdir [folder name] - создание папки в текущем каталоге");//+
                    Console.WriteLine("rmdir [folder name] - удаление папки в текущем каталоге");//+
                    Console.WriteLine("mk [file name] - создать файл в текущей директории");//+
                    Console.WriteLine("rm [file name] - удалить выбранный файл");//+
                    Console.WriteLine("cat [file name] - вывод содержимого файла в консоль");//+
                    Console.WriteLine("mv [file name] [final file name] - переименование файла в текущей папке");//+
                    Console.WriteLine("cp [file name] [FullFolderWay] - копирование файла");//+
                    Console.WriteLine("more [file name] - постраничный просмотр текстового файла");//+
                    Console.WriteLine("echo [text] [file name] - вывод строки текста в терминал, создание и заполнение текстового файла");//+
                    Console.WriteLine("clear - очистить терминал");//+
                    Console.WriteLine("date - вывод в консоли даты и времени");//+
                    Console.WriteLine("exit - выход из эмулятора");//+

                }
                else if (currentCommand == "ls")//список файлов в каталоге
                {
                    LsShow(currentFolder);
                }
                else if (currentCommand == "pwd")//путь к текущей папке
                {
                    if (currentCharacter1 != "/")
                        Console.WriteLine($@"C:{currentFolder}\{currentCharacter1}");
                    else
                    {
                        Console.WriteLine("THIS FOLDER DOESN'T HAVE EXIST");
                    }
                }
                else if (currentCommand == "cd")
                {

                    if (currentCharacter1 != "/")
                    {
                        currentFolder += @"\" + currentCharacter1;

                    }
                    else
                    {
                        int score = 0;
                        for (int i = currentFolder.Length - 1; i >= 0; i--)
                        {
                            if (Convert.ToString(currentFolder[i]) == @"\")
                            {
                                score = i;

                                break;
                            }
                        }
                        currentFolder = currentFolder.Remove(score, currentFolder.Length - score);

                    }
                    //
                }
                else if (currentCommand == "mkdir")
                {
                    FolderCreator(currentCharacter1, currentFolder);
                }
                else if (currentCommand == "rmdir")
                {
                    FolderDeleter(currentCharacter1, currentFolder);
                }
                else if (currentCommand == "mk")
                {
                    FileCreator(currentCharacter1, currentFolder);
                }
                else if (currentCommand == "rm")
                {
                    FileDeleter(currentCharacter1, currentFolder);
                }
                else if (currentCommand == "cat")
                {
                    FileGetInfo(currentCharacter1, currentFolder);
                }
                else if (currentCommand == "mv")
                {
                    FileRename(currentCharacter1,currentCharacter2, currentFolder);
                }
                else if(currentCommand=="cp")
                {
                    FileCopyFunc(currentCharacter1, currentCharacter2, currentFolder);
                }
                else if(currentCommand=="more")
                {
                    string text = "";

                    using (StreamReader fs = new StreamReader(@$"{currentFolder}\{currentCharacter1}"))
                    {
                        while (true)
                        {
                            string temp = fs.ReadLine();

                            if (temp == null) break;

                            text += temp;
                        }
                    }

                    // Выводим на экран.
                    Console.WriteLine(text);
                }
                else if (currentCommand == "echo")//вывод текста
                {

                    if (currentCharacter1 != "/" && currentCharacter2 != "/")
                    {
                        string writePath = currentFolder + @"\" + currentCharacter2;


                        try
                        {
                            using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                            {
                                sw.WriteLine(currentCharacter1);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    else if (currentCharacter1 != "/" && currentCharacter2 == "/")
                    {
                        Console.WriteLine(currentCharacter1);
                    }
                    else
                    {
                        Console.WriteLine("COMMAND HAS NO TEXT");
                    }

                }

                else if (currentCommand == "clear")
                {
                    Console.Clear();
                }
                else if (currentCommand == "date")
                {
                    ShowDateTime();
                }
                else if (currentCommand == "exit")
                {

                    end = true;
                }
            }
        }
        static void DirCreator()//создание основной папки и внутри нее еще папку и заполненный текстовый файл
        {

            string path = @"C:\LinuxEmulator";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory("subpath");
            FileInfo fileInf1 = new FileInfo(@"C:\LinuxEmulator\subpath\text2.txt");
            if (!fileInf1.Exists)
            {
                fileInf1.Create();
            }
            FileInfo fileInf2 = new FileInfo(@"C:\LinuxEmulator\text1.txt");
            if (!fileInf2.Exists)
            {
                fileInf2.Create();

            }
        }
        static void FolderCreator(string character, string folder)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(folder + @"\" + character);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }
        static void FolderDeleter(string character, string folder)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(folder + @"\" + character);
            if (dirInfo.Exists)
            {
                dirInfo.Delete();
            }
        }
        static void FileCreator(string character, string folder)
        {
            FileInfo fileInf = new FileInfo(@$"{folder}\{character}");

            if (!fileInf.Exists)
            {
                var fileInf1 = File.Create(@$"{folder}\{character}");
                fileInf1.Close();
            }
        }
        static void FileDeleter(string character, string folder)
        {
            FileInfo fileInf = new FileInfo(folder + @"\" + character);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }
        }
        static void FileGetInfo(string character, string folder)
        {
            string path = folder + @"\" + character;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static void FileRename(string character,string character2, string folder)
        {
          
            File.Move(folder+@"\"+character, folder + @"\" + character2);

        }
        static void FileCopyFunc(string character, string character2, string folder)
        {
            File.Copy(folder + @"\" + character,character2);
        }

        static void LsShow(string folder)//вывод содержимого папки
        {
            string[] dirs = Directory.GetDirectories(folder);
            foreach (string subdir in dirs)
            {
                Console.WriteLine(subdir);
            }
            string[] files = Directory.GetFiles(folder);
            foreach (string file1 in files)
            {
                Console.WriteLine(file1);
            }
        }
        static void ShowDateTime()//время в консоли
        {
            Console.WriteLine(DateTime.Now);
        }
    }
}
