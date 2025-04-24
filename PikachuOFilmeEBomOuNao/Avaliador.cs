using System.Security.AccessControl;
using System.Text.RegularExpressions;

// ReSharper disable All

namespace PikachuOFilmeEBomOuNao;

public class Avaliador(List<Comentario> comentarios)

{
    private readonly Regex RegexPositivo = new Regex(
        @"\b( bo(m|ns|a(s)?) | am(o|ei|ável)? | excelent(e|íssimo)? | favorit(o|a|ei)?s? | emocion(ei|a|o|ou|ante)?s? | maravilh(a|os[oa]|ad[oa])?s? | ótim[oa]?s? | ador(ei|o|a|ado?s)? |
                                                  gost(ei|o|a|os[oa])?s? | fof([oa]|inh[oa]|uch[oa])?s? | diver(ção|tid[oa])?s? | perfei(ção|t[oa])?s? | fantástic[oa]?s? | incríve(l|is)? | impression(ante|ei|amos)? |
                                                  lind(íssimo|[oa])?s? | lega(l|is)? | sensaciona(l|is)? | positiv(amente|[oa])?s? | nostalgi(a|co)?s? | surpre(endente|s[oa])?s? | carism(a|átic[oa])?s? | 
                                                  épic([oa]|amente)?s? | inspira(dor(es)?|ção|ções)? | cert[oa]s? | bacan[oa]s? )\b",
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);

    private readonly Regex RegexNegativo = new Regex(
        @"\b ( mau | mal(dade|dos[oa]|vad[oa])?s? | rui(m|ns)? | lix(o|ão)?s? | infeliz(mente)? | insuportáve(l|is)? | irrita(nte)?s? | frac([oa]te)?s? | burr[oa]s? | best(a|eira)?s? | bob(inh[oa]|[oa])?s? | chat(inh[oa]|[oa])?s? |
                                                    mínim[o]s? | fal(had[oa]|id[oa]|h[oa])?s? | problema(tiza(r|d[oa])?)? | decepcion(ante|ou|a|ei)? |  perdid[oa]s? | forçad[oa] | (en)?tedi(o|ante)?s? | 
                                                    paia[s]? | preguiç(a|os[oa])?s? | desanima(dor[ae])?s? | bost(a|il)?s?| merd(a|inha)?s? | imbeci(l|lidade)?s? | vagabund([oa]|agem)?s? | ignoran(te|cia)?s? | 
                                                    pervers(idade|[oa]|amente)?s? | racis(ta|cismo)?s? | xenofob([oa]|ia)?s? | machis(ta|mo)?s? | homofobi(a|co)?s? | péssim[oa]s? | pior(es)? | errad[oa]s? | fei(nh[oa]|[oa])s? )\b",
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);

    private readonly Regex RegexNegacoes = new Regex(
        @"\b( não|nem|nunca|sem|jamais|nenhum[a]|falta? )\b",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);


    public void AvaliaComentario()
    {
        int i = 0;
        foreach (var coment in comentarios)
        {
            //verifica comentários positivos
            foreach (Match match in RegexPositivo.Matches(coment.Coment))
            {
                string palavra = match.Value;
                Console.WriteLine(palavra);
                if (EhNegado(coment.Coment, palavra))
                {
                    coment.CountNegativos++;
                }
                else
                {
                    coment.CountPositivos++;
                }
            }

            //verifica comentários negativos
            foreach (Match match in RegexNegativo.Matches(coment.Coment))
            {
                string palavra = match.Value;
                Console.WriteLine(palavra);
                if (EhNegado(coment.Coment, palavra))
                {
                    coment.CountPositivos++;
                }
                else
                {
                    coment.CountNegativos++;
                }
            }

            //TODO: apagar esses prints depois
            Console.WriteLine($"coment {i+1} positivos*: {coment.CountPositivos}");
            Console.WriteLine($"coment {i+1} negativos*: {coment.CountNegativos}");
            Console.WriteLine();
            i++;
        }
    }

    private bool EhNegado(string comentario, string palavra)
    {
        var palavras = comentario.Split(new[] { ' ', '\r', '\n', '\t', '.', ',', '!', '?', ':', '/', '-' });
        for (int i = 0; i < palavras.Length; i++)
        {
            if (!RegexNegacoes.IsMatch(palavras[i])) continue;
            for (int j = 1; j < 4 && i + j < palavras.Length; j++)
            {
                if (string.Equals(palavras[i + j], palavra, StringComparison.OrdinalIgnoreCase))
                {
                    Match match = RegexNegativo.Match(palavras[i]);
                    
                    //TODO: apagar esses prints depois
                    Console.WriteLine("palavra de negacao: " + palavras[i]);
                    Console.WriteLine("negado: " + palavras[i + j]);
                    
                    return true;
                }
            }
        }
        return false;
    }

    public void CategorizaComentarios()
    {
        foreach (var coment in comentarios)
        {
            int diferenca = coment.CountPositivos - coment.CountNegativos;

            if (diferenca is -1 or 0 or 1)
            {
                coment.Categoria = Categorias.Neutro;
            }
            else if (diferenca > 1)
            {
                coment.Categoria = Categorias.Bom;
            }
            else
            {
                coment.Categoria = Categorias.Ruim;
            }
        }
    }

    public List<Comentario> GeraListaPorCategoria(Categorias categoria)
    {
        List<Comentario> listaGenerica = new List<Comentario>(); 
        foreach (var coment in comentarios)
        {
            if (coment.Categoria == categoria)
            {
                listaGenerica.Add(coment);
            }
        }
        return listaGenerica;
    }

    public void CalculaPorcentagens(List<Comentario> lista)
    {
        Categorias categoria = lista.First().Categoria;
        float porcentagem = (float)lista.Count / comentarios.Count;
        Console.WriteLine($"Porcentagem comentarios {categoria}: " + porcentagem * 100 + "%");
    }


    public void CalculaNotaFilme()
    {
        float soma = 0;

        foreach (var coment in comentarios)
        {
            soma += (float)coment.Categoria;
        }

        float nota = (float)soma / comentarios.Count;
        Console.WriteLine($"Nota final filme: {nota:F2}");
    }
}