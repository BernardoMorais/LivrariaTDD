using System.ComponentModel;

namespace LivrariaTDD.Infrastructure.Enums
{
    public enum Categories
    {
        [Description("Autoajuda")]
        Autoajuda = 0,

        [Description("Biografias / Autobiografias")]
        BiografiasAutobiografias = 1,

        [Description("Ciências Sociais")]
        CiênciasSociais = 2,

        [Description("Direito")]
        Direito = 3,

        [Description("Ensino de Línguas")]
        EnsinoDeLínguas = 4,
    
        [Description("Filosofia")]
        Filosofia = 5,

        [Description("História do Brasil")]
        HistóriaDoBrasil = 6,

        [Description("História Geral")]
        HistóriaGeral = 7,

        [Description("HQs")]
        HQs = 8,

        [Description("Infanto-Juvenis")]
        InfantoJuvenis = 9,

        [Description("Literatura Brasileira")]
        LiteraturaBrasileira = 10,

        [Description("Literatura Estrangeira")]
        LiteraturaEstrangeira = 11,

        [Description("Pedagogia")]
        Pedagogia = 12,

        [Description("Psicologia")]
        Psicologia = 13,

        [Description("Religiões")]
        Religiões = 14
    }
}
