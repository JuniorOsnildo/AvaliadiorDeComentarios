using System.Text.RegularExpressions;
using PikachuOFilmeEBomOuNao;

  /*  ● O programa deve apresentar no console a quantidade de
    comentários lidos e o percentual de comentários de cada categoria.
    ● O programa também deve gerar um arquivo com o texto de cada
    comentário lido e a sua categoria.
*/

//lê arquivo 
var data = File.ReadAllText("../../../Comentarios/filme-248825_comentarios.txt");

//separa comentários do texto lido
string[] comentarios = Regex.Split(data, @"\r?\n\r?\n");
List<Comentario> listaComentarios = new List<Comentario>();

//gera lista de comentários e printa na tela
foreach (var comentarioStr in comentarios)
{
    listaComentarios.Add(new Comentario(comentarioStr));
    //Console.WriteLine(listaComentarios[^1].Coment);
    //Console.WriteLine(new string('-', 100));
}

//printa numero de comentários
Console.WriteLine($"numero total de comentarios: {listaComentarios.Count}");

//cria um avaliador dos comentários
Avaliador avaliador = new Avaliador(listaComentarios); 

avaliador.AvaliaComentario();
avaliador.CategorizaComentarios();

List<Comentario> listaBom = avaliador.GeraListaPorCategoria(Categorias.Bom);
List<Comentario> listaRuim = avaliador.GeraListaPorCategoria(Categorias.Ruim);
List<Comentario> listaNeutro = avaliador.GeraListaPorCategoria(Categorias.Neutro);

avaliador.CalculaPorcentagens(listaBom);
avaliador.CalculaPorcentagens(listaRuim);
avaliador.CalculaPorcentagens(listaNeutro);

avaliador.CalculaNotaFilme();

//gera o arquivo txt
string filePath = "../../../Classificados.txt";
string classificados = "POSITIVOS: \n";

foreach (var comentario in listaBom)
{
    classificados += $"\t{comentario.Coment}\n";
    classificados += $"{new string('-', 100)} \n";
}

classificados += "\nNEUTROS: \n";
foreach (var comentario in listaNeutro)
{
    classificados += $"\t{comentario.Coment}\n";
    classificados += $"{new string('-', 100)} \n";
}

classificados += "\nNEGATIVOS: \n";
foreach (var comentario in listaRuim)
{
    classificados += $"\t{comentario.Coment}\n";
    classificados += $"{new string('-', 100)} \n";
}

try
{
    File.WriteAllText(filePath, classificados);
    Console.WriteLine("\nArquivo gerado com sucesso!");
}
catch (Exception e)
{
    Console.WriteLine($"Erro: {e.Message}");
}