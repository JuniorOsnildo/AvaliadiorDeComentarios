using System.Text.RegularExpressions;
using PikachuOFilmeEBomOuNao;

//lê arquivo 
var data = File.ReadAllText("../../../Comentarios/filme-248825_comentarios.txt");

//separa comentários do texto lido
string[] comentarios = Regex.Split(data, @"\r?\n\r?\n");
List<Comentario> listaComentarios = new List<Comentario>();

//gera lista de comentários e printa na tela
for (int i = 0; i < comentarios.Length; i++)
{
    //Console.WriteLine(i);
    Comentario coment =  new Comentario(comentarios[i]);
    listaComentarios.Add(coment);
    //Console.WriteLine(listaComentarios[i].Coment);
    //Console.WriteLine(new string('-', 100));
}

//printa numero de comentários
Console.WriteLine($"numero total de comentarios: {listaComentarios.Count}");

//cria um avaliador dos comentários
Avaliador avaliador = new Avaliador(listaComentarios); 

avaliador.AvaliaComentario();
avaliador.CategorizaComentarios();
avaliador.CalculaNotaFilme();
