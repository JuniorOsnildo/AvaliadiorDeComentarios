using System.Text.RegularExpressions;
using PikachuOFilmeEBomOuNao;

  /*  ● O programa deve apresentar no console a quantidade de
    comentários lidos e o percentual de comentários de cada categoria.
    ● O programa também deve gerar um arquivo com o texto de cada
    comentário lido e a sua categoria.
*/

//lê arquivo 
var data = File.ReadAllText("../../../Comentarios/filme-133392_comentarios.txt");

//separa comentários do texto lido
string[] comentarios = Regex.Split(data, @"\r?\n\r?\n");

//cria um avaliador dos comentários
Avaliador avaliador = new Avaliador(); 

//gera lista de comentários e printa na tela
foreach (var comentarioStr in comentarios)
{
    avaliador.AddComentario(new Comentario(comentarioStr));
}

//printa numero de comentários
Console.WriteLine($"Número total de comentários: {avaliador.Comentarios.Count} \n");

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