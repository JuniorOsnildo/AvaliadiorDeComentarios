using System.Text.RegularExpressions;

namespace PikachuOFilmeEBomOuNao;

public class Avaliador(List<Comentario> comentarios)
{
    private readonly Regex _regexPositivo = new Regex(
        @"\b( b[eo]m | am(o|ei|ável)? | excelent(e|íssimo)? | favorit(o|a|ei)?s? | emocion(ei|a|o|ou|ante)?s? | maravilh(a|os[oa]|ad[oa])?s? | ótim[oa]?s? | ador(ei|o|a|ado?s)? |
                                                  gost(ei|o|a|os[oa])?s? | fof([oa]|inh[oa]|uch[oa])?s? | diver(ção|tid[oa])?s? | perfei(ção|t[oa])?s? | fantástic[oa]?s? | incríve(l|is)? | impression(ante|ei|amos)? |
                                                  lind(íssimo|[oa])?s? | lega(l|is)? | sensaciona(l|is)? | positiv(amente|[oa])?s? | nostalgi(a|co)?s? | surpre(endente|s[oa])?s? | carism(a|átic[oa])?s? )\b",
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);

    private readonly Regex _regexNegativo = new Regex(
        @"\b ( ma[ul] | rui(m|ns)? | lix(o|ão)?s? | infeliz(mente)? | irrita(nte)?s? | frac([oa]te)?s? | burr[oa]s? | best(a|eira)?s? | bob(inh[oa]|[oa])?s? | chat(inh[oa]|[oa])?s? |
                                                    mínim[o]s? | fal(had[oa]|id[oa]|h[oa])?s? | problema(tiza(r|d[oa])?)? | decepcion(ante|ou|a|ei)? |  perdid[oa]s? | forçad[oa] | (en)?tedi(o|ante)?s? )\b",
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);

    private readonly Regex _regexNegacoes = new Regex(
        @"\b(não|nem|nunca|sem|jamais|nenhum[a]?)\b",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public void AvaliaComentario()
    {
        int i = 0;
        foreach (var coment in comentarios)
        {
            /*
             realiza match basico, sem verificação
             
            coment.CountPositivos += _regexPositivo.Matches(coment.Coment).Count;
            coment.CountNegativos += _regexNegativo.Matches(coment.Coment).Count;
            
            Console.WriteLine($"coment {i} positivos: {coment.CountPositivos}");
            Console.WriteLine($"coment {i} negativos: {coment.CountNegativos}");
            */
            
            //verifica comentários positivos
            foreach (Match match in _regexPositivo.Matches(coment.Coment))
            {
                string palavra = match.Value;
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
            foreach (Match match in _regexNegativo.Matches(coment.Coment))
            {
                string palavra = match.Value;
                if (EhNegado(coment.Coment, palavra))
                {
                    coment.CountPositivos++;
                }
                else
                {
                    coment.CountNegativos++;
                }
            }

            Console.WriteLine($"coment {i} positivos*: {coment.CountPositivos}");
            Console.WriteLine($"coment {i} negativos*: {coment.CountNegativos}");
            Console.WriteLine();
            i++;
        }
    }

    private bool EhNegado(string comentario, string palavra)
    {
        var palavras = comentario.Split(new[] { ' ', '\r', '\n', '\t', '.', ',', '!', '?', ':', '/', '-' });
        for (int i = 0; i < palavras.Length; i++)
        {
            if (!_regexNegacoes.IsMatch(palavras[i])) continue;
            for (int j = 1; j < 4 && i + j < palavras.Length; j++)
            {
                if (string.Equals(palavras[i + j], palavra, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void VerificaCategoria()
    {
    }
}