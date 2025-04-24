namespace PikachuOFilmeEBomOuNao;

public class Comentario
{
    private readonly string coment;

    public Comentario(string coment) => Coment = coment;

    public string Coment
    {
        get => coment;
        init => coment = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int CountPositivos { get; set; } = 0;

    public int CountNegativos { get; set; } = 0;
    
    public Categorias Categoria { get; set; }
}