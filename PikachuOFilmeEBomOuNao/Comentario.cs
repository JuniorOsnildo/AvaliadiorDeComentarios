namespace PikachuOFilmeEBomOuNao;

public class Comentario
{
    private readonly string _coment;

    public Comentario(string coment) => Coment = coment;

    public string Coment
    {
        get => _coment;
        init => _coment = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int CountPositivos { get; set; } = 0;

    public int CountNegativos { get; set; } = 0;
    
    public Categorias Categoria { get; set; }
}