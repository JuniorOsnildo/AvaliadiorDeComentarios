using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;

var data = File.ReadAllText("../../../Comentarios/filme-248825_comentarios.txt");

string[] comentarios = data.Split(["\r\n\r\n"], StringSplitOptions.RemoveEmptyEntries);

for (int i = 0; i < comentarios.Length; i++)
{
    Console.WriteLine(i);
    Console.WriteLine(comentarios[i]);
}


/*
bool corte = false;

foreach (var v in data)
{
    if (v == '\n') corte = true;

    if (corte)
    {
        if (v != '\n')
            data.Split("\n").ToList();

        corte = false;
    }

}


Console.WriteLine(list.Count);


int h = 0;

foreach (var variable in list)
{
    Console.WriteLine(h);
    Console.WriteLine(variable);
    h++;
}

*/