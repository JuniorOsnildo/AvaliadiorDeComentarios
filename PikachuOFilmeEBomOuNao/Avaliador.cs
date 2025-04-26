using System.Security.AccessControl;
using System.Text.RegularExpressions;

// ReSharper disable All

namespace PikachuOFilmeEBomOuNao;

public class Avaliador()
{
    private readonly Regex RegexPositivo = new Regex(
        @"\b( bo(m|ns|a(s)?) | am(o|ei|ável)? | excelent(e|íssimo)? | favorit(o|a|ei)?s? | emocion(ei|a|o|ou|ante)?s? | maravilh(a|os[oa]|ad[oa])?s? | ótim[oa]?s? | ador(ei|o|a|ado?s)? |
                                                  gost(ei|o|a|os[oa])?s? | fof([oa]|inh[oa]|uch[oa])?s? | diver(ção|tid[oa])?s? | perfei(ção|t[oa])?s? | fantástic[oa]?s? | incríve(l|is)? | impression(ante|ei|amos)? |
                                                  lind(íssimo|[oa])?s? | lega(l|is)? | sensaciona(l|is)? | positiv(amente|[oa])?s? | nostalgi(a|co)?s? | surpre(endente|s[oa])?s? | carism(a|átic[oa])?s? | 
                                                  épic([oa]|amente)?s? | inspira(dor(es)?|ção|ções)? | cert[oa]s? | bacan[oa]s? | bel(íssimo|[oa]|eza)?s? | bonit[oa]s? | superior[a]?s? )\b",
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);

    private readonly Regex RegexNegativo = new Regex(
        @"\b ( mau | mal(dade|dos[oa]|vad[oa])?s? | rui(m|ns)? | lix(o|ão)?s? | infeliz(mente)? | insuportáve(l|is)? | irrita(nte)?s? | frac([oa]te)?s? | burr[oa]s? | best(a|eira)?s? | bob(inh[oa]|[oa])?s? | chat(inh[oa]|[oa])?s? |
                                                    mínim[o]s? | fal(had[oa]|id[oa]|h[oa])?s? | problema(tiza(r|d[oa])?)? | decepcion(ante|ou|a|ei)? |  perdid[oa]s? | forçad[oa] | (en)?tedi(o|ante)?s? | 
                                                    paia[s]? | preguiç(a|os[oa])?s? | desanima(dor[ae])?s? | bost(a|il)?s?| merd(a|inha)?s? | imbeci(l|lidade)?s? | vagabund([oa]|agem)?s? | ignoran(te|cia)?s? | 
                                                    pervers(idade|[oa]|amente)?s? | racis(ta|cismo)?s? | xenofob([oa]|ia)?s? | machis(ta|mo)?s? | homofobi(a|co)?s? | péssim[oa]s? |
                                                    pior(es)? | errad[oa]s? | fei(nh[oa]|[oa])s? | lent([oa]|idão)?s? )\b",
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);

    private readonly Regex RegexNegacoes = new Regex(
        @"\b( não | nem | nunca | sem | jamais |nenhum[a]|falta )\b",
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);

    private readonly Regex RegexIntensificadores = new Regex(
        @"\b( muit[oa]s? | bastante(s)? | tão | demais | mais | tant[oa]s? | extremamente | super | mega | demasiad([oa]|amente) )\b",
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);

    public List<Comentario> Comentarios { get; } = new List<Comentario>();

    public void AddComentario(Comentario comentario)
    {
        Comentarios.Add(comentario);
    }

    public void AvaliaComentario()
    {
        foreach (var coment in Comentarios)
        {
            var palavras = coment.Coment.Split(new[] { ' ', '\r', '\n', '\t', '.', ',', '!', '?', ':', '/', '-' });

            for (int i = 0; i < palavras.Length; i++)
            {
                //verifica comentários positivos
                if (RegexPositivo.IsMatch(palavras[i]))
                {
                    //intensificadores
                    if (EhNegadoOuIntensificado(palavras, palavras[i], i, RegexIntensificadores))
                    {
                        coment.CountPositivos += 2;
                        continue;
                    }
                    //negações
                    if (!EhNegadoOuIntensificado(palavras, palavras[i], i, RegexNegacoes))
                    {
                        coment.CountPositivos++;
                        continue;
                    }
                
                    coment.CountNegativos++;
                    continue;
                }

                //verifica comentários negativos
                if (RegexNegativo.IsMatch(palavras[i]))
                {
                    //intensificadores
                    if (EhNegadoOuIntensificado(palavras, palavras[i], i, RegexIntensificadores)) 
                    {
                        coment.CountNegativos += 2;
                        continue;
                    }
                    
                    //negações
                    if (!EhNegadoOuIntensificado(palavras, palavras[i], i, RegexNegacoes))
                    {
                        coment.CountNegativos++;
                        continue;
                    }
                
                    coment.CountPositivos++;
                }
            }
        }
    }

    private bool EhNegadoOuIntensificado(string[] palavras, string palavra, int index, Regex tipoRegex)
    {
        int start = (index <= 3) ? 0 : index - 3;
        int end = index; 

        for (int i = start; i < end; i++)
        {
            if (tipoRegex.IsMatch(palavras[i])) return true;
        }
        return false;
    }
    
    public void CategorizaComentarios()
    {
        foreach (var coment in Comentarios)
        {
            int diferenca = coment.CountPositivos - coment.CountNegativos;

            if (diferenca < -1)
            {
                coment.Categoria = Categorias.Ruim;
                continue;
            }

            if (diferenca > 1)
            {
                coment.Categoria = Categorias.Bom;
                continue;
            }

            coment.Categoria = Categorias.Neutro;
        }
    }

    public List<Comentario> GeraListaPorCategoria(Categorias categoria)
    {
        List<Comentario> listaGenerica = new List<Comentario>();
        foreach (var coment in Comentarios)
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
        float porcentagem = (float)lista.Count / Comentarios.Count;
        Console.WriteLine($"Porcentagem comentarios {categoria}: " + porcentagem * 100 + "%");
    }

    public void CalculaNotaFilme()
    {
        float soma = 0;

        foreach (var coment in Comentarios)
        {
            soma += (float)coment.Categoria;
        }

        float nota = (float)soma / Comentarios.Count;
        Console.WriteLine($"\nNota final filme: {nota:F2}");
    }
}