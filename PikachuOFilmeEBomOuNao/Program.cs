using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;

var data = File.ReadAllText("C:\\Users\\alunolages\\PycharmProjects\\PythonProject1\\filme-248825_comentarios.txt");  

LinkedList<string> list = new LinkedList<string>();

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

foreach (var VARIABLE in list)
{
    Console.WriteLine(h);
    Console.WriteLine(VARIABLE);
    h++;
}