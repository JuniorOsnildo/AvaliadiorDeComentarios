using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;

var data = File.ReadAllText("../../../Comentarios/filme-248825_comentarios.txt");

string[] comentarios = Regex.Split(data, @"\r?\n\r?\n");

for (int i = 0; i < comentarios.Length; i++)
{
    Console.WriteLine(i);
    Console.WriteLine(comentarios[i]);
    Console.WriteLine(new string('-', 100));
}

Console.WriteLine(comentarios.Length);